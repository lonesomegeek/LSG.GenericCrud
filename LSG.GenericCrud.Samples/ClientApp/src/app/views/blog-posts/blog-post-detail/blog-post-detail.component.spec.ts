import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogPostDetailComponent } from './blog-post-detail.component';

describe('BlobPostDetailComponent', () => {
  let component: BlobPostDetailComponent;
  let fixture: ComponentFixture<BlobPostDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlobPostDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlobPostDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
