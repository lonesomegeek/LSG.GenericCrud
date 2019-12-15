import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html'
})
export class ItemDataComponent {
  public items: Item[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Item[]>(baseUrl + 'api/items').subscribe(result => {
      this.items = result;
    }, error => console.error(error));
  }
}

export interface Item {
  name: string;
}
