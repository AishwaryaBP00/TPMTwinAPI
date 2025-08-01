import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BackLogManagerComponent } from './back-log-manager.component';

describe('BackLogManagerComponent', () => {
  let component: BackLogManagerComponent;
  let fixture: ComponentFixture<BackLogManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BackLogManagerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BackLogManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
