import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfertasPublicasComponent } from './ofertas-publicas.component';

describe('OfertasPublicasComponent', () => {
  let component: OfertasPublicasComponent;
  let fixture: ComponentFixture<OfertasPublicasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OfertasPublicasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfertasPublicasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
