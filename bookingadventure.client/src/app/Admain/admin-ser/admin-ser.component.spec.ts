import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSerComponent } from './admin-ser.component';

describe('AdminSerComponent', () => {
  let component: AdminSerComponent;
  let fixture: ComponentFixture<AdminSerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminSerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminSerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
