import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html'
})

export class AccountDataComponent implements OnInit {
  public elements: any;
  
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'Name', field: 'name', sortable: true }
  ];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  ngOnInit() {
    this.elements = this
      .http
      .get<Item[]>(this.baseUrl + 'api/accounts');      
  }
}

export interface Item {
  name: string;
}
