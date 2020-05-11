import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  entityName: string = "users";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true, hide: true },
    { headerName: 'Name', field: 'name', sortable: true }];
  constructor() { }

  ngOnInit() {
  }

}
