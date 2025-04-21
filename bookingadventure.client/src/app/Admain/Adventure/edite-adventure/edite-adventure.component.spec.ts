import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditeAdventureComponent } from './edite-adventure.component';

describe('EditeAdventureComponent', () => {
  let component: EditeAdventureComponent;
  let fixture: ComponentFixture<EditeAdventureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditeAdventureComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditeAdventureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
