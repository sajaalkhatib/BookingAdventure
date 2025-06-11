import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddadventureComponent } from './addadventure.component';

describe('AddadventureComponent', () => {
  let component: AddadventureComponent;
  let fixture: ComponentFixture<AddadventureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddadventureComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddadventureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
