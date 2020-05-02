import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TestComponent } from './test/test.component';
import { TestDetailComponent } from './test-detail/test-detail.component';

const routes: Routes = [
  { path: '',       component: TestComponent,       data: { title: 'Tests' } },
  { path: 'create', component: TestDetailComponent, data: { title: 'Create'} }]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class TestsRoutingModule { }
