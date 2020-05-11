import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  entityName: string = "items";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true, hide: true },
    { headerName: 'Name', field: 'name', sortable: true },
    { headerName: 'Price', field: 'price', sortable: true },
    { headerName: 'Color', field: 'color', sortable: true }];
  constructor() { }

  ngOnInit() {
  }
}
