import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AgGridAngular } from 'ag-grid-angular';
import { ItemService } from '../../item.service';

@Component({
  selector: 'app-historical-crud',
  templateUrl: './historical-crud.component.html',
  styleUrls: ['./historical-crud.component.css']
})
export class HistoricalCrudComponent implements OnInit {
  @ViewChild('agGrid', undefined) agGrid: AgGridAngular;

  public rows: Observable<any[]>;
  public columnDefs: any[];

  public entityName: string;
  public baseRoute: string;

  constructor(
    private router: Router,
    private service: ItemService
  ) {    
    this.entityName = this.service.entityName;
    this.baseRoute = "api/" + this.service.entityName;
    // this.service.entityName = this.entityName;
    console.log("crud entity name: " + this.entityName);
  }

  ngOnInit() {
    this.service.baseRoute = "api/" + this.service.entityName; 

    this.rows = this.service.getAll();
    this.agGrid.rowDoubleClicked.subscribe(row => this.rowDoubleClicked(row));
  }

  rowDoubleClicked(row: any) {
    this.router.navigate(['/' + this.entityName + '/' + row.data.id]);
  }
}
