import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from 'src/app/models/item';
import { Observable } from 'rxjs';
import { ItemService } from '../item.service';
import { AgGridAngular } from 'ag-grid-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  @ViewChild('agGrid', undefined) agGrid: AgGridAngular;

  public rows: Observable<Item[]>;
  columnDefs: any[] = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'Name', field: 'name', sortable: true }];
    
  baseRoute: string = "api/objects";
  constructor(
    private service: ItemService,
    private router: Router
  ) { }

  ngOnInit() {
    this.rows = this.service.getAll();
    this.agGrid.rowDoubleClicked.subscribe(row => this.rowDoubleClicked(row));
  }

  rowDoubleClicked(row: any) {
    this.router.navigate(['/objects/' + row.data.id]);
  }
}
