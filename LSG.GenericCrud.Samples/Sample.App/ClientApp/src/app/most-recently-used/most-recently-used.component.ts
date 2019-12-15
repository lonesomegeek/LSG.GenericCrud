import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from '../items/items.component';

@Component({
  selector: 'app-most-recently-used',
  templateUrl: './most-recently-used.component.html'
})
export class MostRecentlyUsedComponent {
  public objects: EntityEvent[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<EntityEvent[]>(baseUrl + 'api/items/most-recently-used').subscribe(result => {
      this.objects = result;
      this.objects.map(_ => {
        http.get<Item[]>(baseUrl + 'api/items/' + _.entityId).subscribe(result2 => {
          let item: Item = result2;
          _.entityName = item.name;
        });
      });
      
      
    }, error => console.error(error));
  }
}


interface EntityEvent {
  entityId: string,
  entityName: string,
  events: HistoricalEvent[]
}

interface HistoricalEvent {
  id: string,
  entityId: string,
  entityName: string,
  action: string,
  createdBy: string,
  createdDate: Date
}