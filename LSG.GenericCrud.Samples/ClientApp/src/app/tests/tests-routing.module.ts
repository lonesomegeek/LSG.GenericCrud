import { NgModule, ComponentFactoryResolver } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TestComponent } from './test/test.component';
import { TestDetailComponent } from './test-detail/test-detail.component';

const routes: Routes = [
  { path: '', data: { title: 'Tests' }, children: [
    { path: '',       component: TestComponent,       data: { title: 'List' } },
    { path: 'create', component: TestDetailComponent, data: { title: 'Create'} },
    { path: ':id',    component: TestDetailComponent, data: { title: 'Edit'} } ] } ];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class TestsRoutingModule { }
