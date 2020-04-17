import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  entityName: string = "items";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'Name', field: 'name', sortable: true },
    { headerName: 'Description', field: 'description', sortable: true }];
  constructor() { }

  ngOnInit() {
  }
}
