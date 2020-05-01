import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contact-detail',
  templateUrl: './contact-detail.component.html',
  styleUrls: ['./contact-detail.component.css']
})
export class ContactDetailComponent implements OnInit {
  entityName: string = "contacts";
  columnDefs = [
    { headerName: 'Id', field: 'id', hideInForm: true, sortable: true },
    { headerName: 'Name', field: 'name', sortable: true },
    { headerName: 'Phone', field: 'phone', sortable: true }];
  constructor() { }

  ngOnInit() {
  }

}
