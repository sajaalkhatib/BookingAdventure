import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DestinationdetailsComponent } from './destinationdetails.component';

describe('DestinationdetailsComponent', () => {
  let component: DestinationdetailsComponent;
  let fixture: ComponentFixture<DestinationdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DestinationdetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DestinationdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
