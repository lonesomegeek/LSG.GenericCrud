import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Item } from './accounts.component';

@Component({
  selector: 'app-accounts-edit',
  templateUrl: './accounts-edit.component.html'
})
export class AccountEditDataComponent {
  public item: Item;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute) {
    let url: string = baseUrl + 'api/accounts/' + route.snapshot.params["id"];
    http.get<Item>(url).subscribe(result => {
      this.item = result;
    }, error => console.error(error));
    http.post(url + "/read", {}).subscribe();
  }
}
