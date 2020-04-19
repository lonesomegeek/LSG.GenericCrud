import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContributorDetailComponent } from './contributor-detail.component';

describe('ContributorDetailComponent', () => {
  let component: ContributorDetailComponent;
  let fixture: ComponentFixture<ContributorDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContributorDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContributorDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
