import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditJobApplicationComponent } from './edit-job-application.component';

describe('EditJobApplicationComponent', () => {
  let component: EditJobApplicationComponent;
  let fixture: ComponentFixture<EditJobApplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditJobApplicationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditJobApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
