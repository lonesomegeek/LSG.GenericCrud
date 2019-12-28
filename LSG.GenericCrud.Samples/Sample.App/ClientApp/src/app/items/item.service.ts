import { Injectable } from '@angular/core';
import { Item } from '../models/item';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor() { }

  getAll() : Observable<Item[]> {
    
  }
}
