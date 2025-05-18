import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarAlertasComponent } from './sidebar-alertas.component';

describe('SidebarAlertasComponent', () => {
  let component: SidebarAlertasComponent;
  let fixture: ComponentFixture<SidebarAlertasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SidebarAlertasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SidebarAlertasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
