import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contact } from '../types/Contact';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  private baseUrl = 'https://localhost:7043/api/Contact';

  constructor(private http: HttpClient) {}

  getAllContacts(pageNumber: number, pageSize: number): Observable<Contact[]> {
    return this.http.get<Contact[]>(
      `${this.baseUrl}/GetAllContacts/${pageNumber}/${pageSize}`
    );
  }

  addContact(contact: Contact): Observable<Contact> {
    return this.http.post<Contact>(`${this.baseUrl}/AddNewContact`, contact);
  }

  editContact(contact: Contact): Observable<Contact> {
    return this.http.put<Contact>(`${this.baseUrl}/${contact.id}`, contact);
  }

  deleteContact(contactId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${contactId}`);
  }
}
