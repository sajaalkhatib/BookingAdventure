import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmainComponent } from './admain.component';

describe('AdmainComponent', () => {
  let component: AdmainComponent;
  let fixture: ComponentFixture<AdmainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdmainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
