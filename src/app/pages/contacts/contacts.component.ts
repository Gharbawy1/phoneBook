import { Component, OnInit, ViewChild, viewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { Contact } from '../../types/Contact';
import { ContactService } from '../../services/contact.service';
import { CommonModule, NgClass } from '@angular/common';
import { MatTable, MatTableModule } from '@angular/material/table';
import { MatCard, MatCardModule } from '@angular/material/card';
import { FormControl, NgModel, ReactiveFormsModule } from '@angular/forms';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatTableDataSource } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { LoginComponent } from '../login/login.component';
import { AuthService } from '../../services/auth.service';
import { Route, Router } from '@angular/router';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { AddContactDialogComponent } from '../add-contact-dialog/add-contact-dialog.component';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { DeleteContactDialogComponent } from '../delete-contact-dialog/delete-contact-dialog.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-contacts',
  imports: [
    CommonModule,
    MatTableModule,
    MatCardModule,
    ReactiveFormsModule,
    MatIconModule,
    MatPaginatorModule,
    MatToolbarModule,
    MatButtonModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
  ],
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.scss',
})
export class ContactsComponent implements OnInit {
  role: string = '';
  dataSource = new MatTableDataSource<Contact>([]); // link with navigator
  displayedColumns: string[] = ['name', 'email', 'phoneNumber'];
  totalCount = 0;
  pageSize = 10;
  pageNumber = 1;
  userName = localStorage.getItem('userName');
  searchQuery: string = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  constructor(
    private contactsService: ContactService,
    private authService: AuthService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.role = localStorage.getItem('role') ?? '';
    this.loadContacts();
    // For Admin
    if (this.role === 'Admin') {
      this.displayedColumns = ['id', 'name', 'email', 'phoneNumber', 'actions'];
    }
  }

  loadContacts() {
    this.contactsService
      .getAllContacts(this.pageNumber, this.pageSize)
      .subscribe({
        next: (res: any) => {
          // res is response from API so i must fetch "data"
          // this.dataSource.data = res.data.data;
          // this.totalCount = res.data.totalCount;
          this.dataSource.data = res.data.data; // استخدم MatTableDataSource
          console.log(res);
          this.dataSource.paginator = this.paginator;
          this.totalCount = res.data.totalCount;
        },
        error: (err) => {
          console.error('Error loading contacts:', err);
        },
      });
  }
  onPageChange(event: any) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadContacts();
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  OpenAddContactPopUp() {
    this.dialog.open(AddContactDialogComponent, {
      width: '30%',
      height: '400px',
      data: {
        title: 'New Contact',
      },
    });
  }

  editContact(contact: Contact) {
    this.dialog.open(AddContactDialogComponent, {
      width: '30%',
      height: '400px',
      data: {
        title: 'Edit Contact',
        contact: contact,
      },
    });
  }

  deleteContact(contact: Contact) {
    const dialogRef = this.dialog.open(DeleteContactDialogComponent, {
      width: '30%',
      height: '185px',
      data: { name: contact.name },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.contactsService.deleteContact(contact.id).subscribe(
          () => {
            this.snackBar.open('Contact deleted successfully!', 'Close', {
              duration: 3000,
              verticalPosition: 'top',
              horizontalPosition: 'center',
              panelClass: ['success-snackbar'],
              
            });
            window.location.reload();
          },
          (error) => {
            console.error('Error deleting contact:', error);
            this.snackBar.open('Failed to delete contact!', 'Close', {
              duration: 3000,
              verticalPosition: 'top',
              horizontalPosition: 'center',
              panelClass: ['error-snackbar'],
            });
          }
        );
      }
    });
  }

  logOut() {
    this.authService.logOut();
    this.router.navigate(['/login']); 
  }
}
