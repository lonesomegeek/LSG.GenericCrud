import { CrudDataComponent } from "../_generics/crud.component";
import { Component } from "@angular/core";

@Component({
  selector: "crud-item-component",
  templateUrl: "../_generics/crud.component.html"
})
export class CrudItemComponent extends CrudDataComponent<Item> {
  columnDefs: any[] = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'Name', field: 'name', sortable: true }];  
  baseRoute: string = "api/items";
}

export interface Item {
  name: string;
}
