import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarRivenComponent } from './editar-riven.component';

describe('EditarRivenComponent', () => {
  let component: EditarRivenComponent;
  let fixture: ComponentFixture<EditarRivenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditarRivenComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditarRivenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
