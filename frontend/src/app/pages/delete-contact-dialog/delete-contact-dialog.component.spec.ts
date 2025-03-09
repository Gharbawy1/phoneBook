import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteContactDialogComponent } from './delete-contact-dialog.component';

describe('DeleteContactDialogComponent', () => {
  let component: DeleteContactDialogComponent;
  let fixture: ComponentFixture<DeleteContactDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteContactDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteContactDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
