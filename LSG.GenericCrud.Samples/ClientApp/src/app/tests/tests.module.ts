import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestComponent } from './test/test.component';
import { TestDetailComponent } from './test-detail/test-detail.component';
import { TestsRoutingModule } from './tests-routing.module';
import { CrudModule } from '../crud/crud.module';

@NgModule({
  declarations: [TestComponent, TestDetailComponent],
  exports: [],
  imports: [
    CommonModule,
    TestsRoutingModule,
    CrudModule
  ]
})
export class TestsModule { }
