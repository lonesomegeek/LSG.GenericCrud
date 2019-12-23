import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html'
})

export class ItemDataComponent implements OnInit {
  public elements: any;
  
  columnDefs = [
    { headerName: 'Name', field: 'name', sortable: true }
  ];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  ngOnInit() {
    this.elements = this
      .http
      .get<Item[]>(this.baseUrl + 'api/items');
      
  }
}

export interface Item {
  name: string;
}
