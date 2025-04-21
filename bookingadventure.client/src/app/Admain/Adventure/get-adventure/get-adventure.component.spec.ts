import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetAdventureComponent } from './get-adventure.component';

describe('GetAdventureComponent', () => {
  let component: GetAdventureComponent;
  let fixture: ComponentFixture<GetAdventureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetAdventureComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetAdventureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
