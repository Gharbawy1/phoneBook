import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddContactDialogComponent } from './add-contact-dialog.component';

describe('AddContactDialogComponent', () => {
  let component: AddContactDialogComponent;
  let fixture: ComponentFixture<AddContactDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddContactDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddContactDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
