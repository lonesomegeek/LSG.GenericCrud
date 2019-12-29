import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from 'src/app/models/item';
import { Observable } from 'rxjs';
import { ItemService } from '../item.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  public rows: Observable<Item[]>;
  columnDefs: any[] = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'Name', field: 'name', sortable: true }];  
  baseRoute: string = "api/items";
  constructor(
    private service : ItemService
  ) { }
  
  ngOnInit() {
    // this.rows = this
    // .http
    // .get<Item[]>(this.baseUrl + this.baseRoute);
    this.rows = this.service.getAll();
  }

}
