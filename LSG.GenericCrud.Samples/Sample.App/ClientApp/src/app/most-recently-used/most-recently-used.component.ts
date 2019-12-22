import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from '../items/items.component';

@Component({
  selector: 'app-most-recently-used',
  templateUrl: './most-recently-used.component.html'
})
export class MostRecentlyUsedComponent {
  public objects: EntityEvent[];

  getRouteFromEntityFullname(entityName: string): string {
    switch (entityName) {
      case "Sample.App.Models.Item":
        return "items";
      case "Sample.App.Models.Account":
        return "accounts";
      default:
        return "";
    }
  }
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<EntityEvent[]>(baseUrl + '/most-recently-used').subscribe(result => {
      this.objects = result;
      this.objects.map(_ => {
        let route = this.getRouteFromEntityFullname(_.events[0].entityName);
        http.get<Item>(baseUrl + 'api/' + route + '/' + _.entityId).subscribe(result2 => {
          let item: Item = result2;
          _.routeUrl = route + '/' + _.entityId;
          _.entityDisplayName = item.name;
        });
      });


    }, error => console.error(error));
  }
}


interface EntityEvent {
  entityId: string,
  entityDisplayName: string,
  routeUrl: string;
  events: HistoricalEvent[];
}

interface HistoricalEvent {
  id: string,
  entityId: string,
  entityName: string,
  action: string,
  createdBy: string,
  createdDate: Date
}