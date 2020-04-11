import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricalCrudComponent } from './historical-crud.component';

describe('HistoricalCrudComponent', () => {
  let component: HistoricalCrudComponent;
  let fixture: ComponentFixture<HistoricalCrudComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoricalCrudComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoricalCrudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
