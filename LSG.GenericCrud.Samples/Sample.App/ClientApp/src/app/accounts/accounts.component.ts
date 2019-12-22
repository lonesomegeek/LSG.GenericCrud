import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html'
})
export class AccountDataComponent {
  public items: Item[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Item[]>(baseUrl + 'api/accounts').subscribe(result => {
      this.items = result;
    }, error => console.error(error));
  }
}

export interface Item {
  name: string;
}
