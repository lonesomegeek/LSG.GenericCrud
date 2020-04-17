import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  entityName: string = "items";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'Name', field: 'name', sortable: true },
    { headerName: 'Description', field: 'description', sortable: true }];
  constructor() { }

  ngOnInit() {
  }
}
