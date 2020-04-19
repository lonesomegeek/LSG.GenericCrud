import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AgGridAngular } from 'ag-grid-angular';
import { BaseService } from '../base.service';

@Component({
  selector: 'app-crud',
  templateUrl: './crud.component.html',
  styleUrls: ['./crud.component.css']
})
export class CrudComponent implements OnInit {
  @ViewChild('agGrid', undefined) agGrid: AgGridAngular;

  public rows: Observable<any[]>;
  
  @Input()
  public columnDefs: any[];

  @Input()
  entityName: string;
  public baseRoute: string;

  constructor(
    private router: Router,
    private service: BaseService
  ) {}

  ngOnInit() {

    this.service.entityName = this.entityName;    
    this.baseRoute = "api/" + this.service.entityName;
    this.service.baseRoute = this.baseRoute;
    this.service.baseRoute = "api/" + this.service.entityName; 
    
    this.rows = this.service.getAll();
    this.agGrid.rowDoubleClicked.subscribe(row => this.rowDoubleClicked(row));
  }  

  rowDoubleClicked(row: any) {
    this.router.navigate(['/' + this.entityName + '/' + row.data.id]);
  }
}
