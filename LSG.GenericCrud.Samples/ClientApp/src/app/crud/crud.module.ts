import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CrudComponent } from './crud/crud.component';
import { CrudDetailComponent } from './crud-detail/crud-detail.component';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [CrudComponent, CrudDetailComponent],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    AgGridModule.withComponents([])
  ],
  exports: [
    CrudComponent, CrudDetailComponent
  ]
})

export class CrudModule { }
