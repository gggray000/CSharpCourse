import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginPassengerComponent } from './login-passenger.component';

describe('LoginPassengerComponent', () => {
  let component: LoginPassengerComponent;
  let fixture: ComponentFixture<LoginPassengerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginPassengerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginPassengerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
