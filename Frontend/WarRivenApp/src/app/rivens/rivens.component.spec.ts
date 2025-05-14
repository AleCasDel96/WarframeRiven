import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RivensComponent } from './rivens.component';

describe('RivensComponent', () => {
  let component: RivensComponent;
  let fixture: ComponentFixture<RivensComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RivensComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RivensComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
