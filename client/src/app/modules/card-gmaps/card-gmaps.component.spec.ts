import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardGmapsComponent } from './card-gmaps.component';

describe('CardGmapsComponent', () => {
  let component: CardGmapsComponent;
  let fixture: ComponentFixture<CardGmapsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardGmapsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardGmapsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
