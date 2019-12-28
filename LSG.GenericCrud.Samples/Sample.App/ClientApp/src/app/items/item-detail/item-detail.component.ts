import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Item } from 'src/app/models/item';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  row: Item;

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    route: ActivatedRoute) {
    let url: string = baseUrl + 'api/items/' + route.snapshot.params["id"];
    http.get<Item>(url).subscribe(result => {
      this.row = result;
    }, error => console.error(error));
    http.post(url + "/read", {}).subscribe();
  }

  ngOnInit() {
  }

}

