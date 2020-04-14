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

  public baseRoute: string;

  constructor(
    private router: Router,
    private service: ItemService
  ) { }

  ngOnInit() {
    this.rows = this.service.getAll();
    this.agGrid.rowDoubleClicked.subscribe(row => this.rowDoubleClicked(row));
  }

  rowDoubleClicked(row: any) {
    this.router.navigate(['/objects/' + row.data.id]);
  }
}
