import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-share-detail',
  templateUrl: './share-detail.component.html',
  styleUrls: ['./share-detail.component.css']
})
export class ShareDetailComponent implements OnInit {
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
