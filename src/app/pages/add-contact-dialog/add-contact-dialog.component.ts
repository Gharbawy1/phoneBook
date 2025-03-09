import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ContactService } from '../../services/contact.service';
import { Contact } from '../../types/Contact';
import { MatFormFieldModule } from '@angular/material/form-field';
import { DialogModule } from '@angular/cdk/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { NgxIntlTelInputModule } from 'ngx-intl-tel-input';

@Component({
  selector: 'app-add-contact-dialog',
  templateUrl: './add-contact-dialog.component.html',
  styleUrl: './add-contact-dialog.component.scss',
  imports: [
    MatFormFieldModule,
    DialogModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    NgxIntlTelInputModule
  ],
})
export class AddContactDialogComponent implements OnInit {
  newContactForm!: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private ref: MatDialogRef<AddContactDialogComponent>,
    private fb: FormBuilder,
    private contactService: ContactService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.newContactForm = this.fb.group({
      name: [this.data?.contact?.name || '', Validators.required],
      email: [
        this.data?.contact?.email || '',
        [Validators.required, Validators.email],
      ],
      phoneNumber: [
        this.data?.contact?.phoneNumber || '',
        [Validators.required, Validators.pattern(/^\d{11}$/)],
      ],
    });
  }

  closepopup() {
    this.ref.close();
  }

  addContact() {
    if (this.newContactForm.valid) {
      const newContact: Contact = {
        id: 0,
        ...this.newContactForm.value,
      };

      this.contactService.addContact(newContact).subscribe(
        (response: any) => {
          this.snackBar.open('Contact added successfully!', 'Close', {
            duration: 3000, // المدة بالمللي ثانية (3 ثواني)
            verticalPosition: 'top',
            horizontalPosition: 'center',
            panelClass: ['success-snackbar'], // كلاس مخصص
          });
          this.ref.close(response);
        },
        (error: any) => {
          console.error('Error adding contact:', error);
          this.snackBar.open('Failed to add contact!', 'Close', {
            duration: 3000,
            verticalPosition: 'top',
            horizontalPosition: 'center',
            panelClass: ['error-snackbar'],
          });
        }
      );
    }
  }

  updateContact() {
    if (this.newContactForm.valid) {
      const newContact: Contact = {
        id: this.data?.contact.id,
        ...this.newContactForm.value,
      };
      console.log(newContact);

      this.contactService.editContact(newContact).subscribe(
        (response: any) => {
          this.snackBar.open('Contact Updated successfully!', 'Close', {
            duration: 3000,
            verticalPosition: 'top',
            horizontalPosition: 'center',
            panelClass: ['success-snackbar'],
          });
          this.ref.close(response);
          window.location.reload();
        },
        (error: any) => {
          console.error('Error adding contact:', error);
          this.snackBar.open('Failed to add contact!', 'Close', {
            duration: 3000,
            verticalPosition: 'top',
            horizontalPosition: 'center',
            panelClass: ['error-snackbar'],
          });
        }
      );
    }
  }

  saveContact() {
    if (this.data?.contact) {
      this.updateContact();
    } else {
      this.addContact();
    }
  }
}
