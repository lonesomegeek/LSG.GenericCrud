import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlobPostComponent } from './blob-post.component';

describe('BlobPostComponent', () => {
  let component: BlobPostComponent;
  let fixture: ComponentFixture<BlobPostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlobPostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlobPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
