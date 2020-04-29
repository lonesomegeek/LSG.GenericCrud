import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  entityName: string = "items";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true, hideInForm: true },
    { headerName: 'Name', field: 'name', sortable: true },
    { headerName: 'Price', field: 'price', sortable: true },
    { headerName: 'Color', field: 'color', sortable: true }];
  constructor() { }

  ngOnInit() {
  }
}
