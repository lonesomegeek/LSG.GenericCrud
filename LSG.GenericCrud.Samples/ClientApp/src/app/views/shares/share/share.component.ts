import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-share',
  templateUrl: './share.component.html',
  styleUrls: ['./share.component.css']
})
export class ShareComponent implements OnInit {
  entityName: string = "shares";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'ContactId', field: 'contactId', sortable: true },
    { headerName: 'ItemId', field: 'itemId', sortable: true },
    { headerName: 'SharingReminder', field: 'sharingReminder', sortable: true },
    { headerName: 'Description', field: 'description', sortable: true }];
  constructor() { }

  ngOnInit() {
  }
}
