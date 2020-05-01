import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CrudComponent } from './crud/crud.component';
import { CrudDetailComponent } from './crud-detail/crud-detail.component';



@NgModule({
  declarations: [CrudComponent, CrudDetailComponent],
  imports: [
    CommonModule
  ]
})
export class CrudModule { }
