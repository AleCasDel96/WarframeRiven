import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrearRivenComponent } from './crear-riven.component';

describe('CrearRivenComponent', () => {
  let component: CrearRivenComponent;
  let fixture: ComponentFixture<CrearRivenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CrearRivenComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrearRivenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
