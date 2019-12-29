import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Item } from 'src/app/models/item';
import { ItemService } from '../item.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  row: Item;
  selectedId : string;
  editing : boolean = true;
  history : Observable<History[]>;
  historyColumnDefs: any[] = [
    { headerName: 'Action', field: 'action', sortable: true },
    { headerName: 'By', field: 'createdBy', sortable: true },
    { headerName: 'At', field: 'createdDate', sortable: true }];  

  constructor(
    private service : ItemService,
    route : ActivatedRoute
  ) {
    this.selectedId = route.snapshot.params["id"];
  }

  ngOnInit() {
    this.service.getOne(this.selectedId).subscribe(e => { this.row = e; });
    this.service.makeRead(this.selectedId).subscribe(result => { console.log(result); });
    this.history = this.service.getOneHistory(this.selectedId);
  }

}

