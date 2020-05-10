import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  entityName: string = "contacts";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'Name', field: 'name', sortable: true },
    { headerName: 'Phone', field: 'phone', sortable: true }];
  constructor() { }

  ngOnInit() {
  }

}
