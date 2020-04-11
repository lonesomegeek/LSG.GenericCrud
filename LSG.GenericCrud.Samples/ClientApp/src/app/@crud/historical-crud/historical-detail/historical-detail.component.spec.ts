import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricalDetailComponent } from './historical-detail.component';

describe('HistoricalDetailComponent', () => {
  let component: HistoricalDetailComponent;
  let fixture: ComponentFixture<HistoricalDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoricalDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoricalDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
