import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CrudDetailComponent } from './crud-detail.component';

describe('CrudDetailComponent', () => {
  let component: CrudDetailComponent;
  let fixture: ComponentFixture<CrudDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CrudDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CrudDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
