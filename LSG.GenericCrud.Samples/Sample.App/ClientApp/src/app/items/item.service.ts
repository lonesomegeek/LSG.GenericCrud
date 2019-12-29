import { Injectable, Inject } from '@angular/core';
import { Item } from '../models/item';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  baseRoute: string = "api/items";

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  getAll() : Observable<Item[]> { return this.http.get<Item[]>(this.baseUrl + this.baseRoute); }
  getOne(id : string) : Observable<Item> { return this.http.get<Item>(this.baseUrl + this.baseRoute + "/" + id); }
  getOneHistory(id : string) : Observable<History[]> { return this.http.get<History[]>(this.baseUrl + this.baseRoute + "/" + id + "/history"); }
  makeRead(id : string) : Observable<any> { return this.http.post(this.baseUrl + this.baseRoute + "/" + id + "/read", {}); }
  putOne(id : string, data : Item) { return this.http.put(this.baseUrl + this.baseRoute + "/" + id, data)};
  postOne(data : Item) { return this.http.post(this.baseUrl + this.baseRoute, data)};
}
