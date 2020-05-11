import { Component, OnInit, Input, Inject, AfterViewInit, ViewChild } from '@angular/core';
import { CrudBase } from '../../models/crud';
import { BaseService } from '../../views/@crud/base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AgGridAngular } from 'ag-grid-angular';
import { Router } from '@angular/router';
import { ColumnDef } from '../../models/column-def';

@Component({
  selector: 'app-crud',
  templateUrl: './crud.component.html',
  styleUrls: ['./crud.component.css']
})
export class CrudComponent implements OnInit, AfterViewInit {
  @Input()
  model: CrudBase;
  service: BaseService;
  rows: Observable<any[]>;
  @ViewChild('agGrid') agGrid: AgGridAngular;

  constructor(
    private router: Router,
    httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) { 
    this.service = new BaseService(httpClient, baseUrl);
  }
  ngAfterViewInit(): void {
    this.agGrid.rowDoubleClicked.subscribe(row => this.rowDoubleClicked(row));
  }

  ngOnInit(): void {  
    this.service.baseRoute = "api/" + this.model.entityName;
    this.rows = this.service.getAll();
  }

  rowDoubleClicked(row: any) {
    this.router.navigate(['/' + this.model.routeName + '/' + row.data.id]);
  }
}
