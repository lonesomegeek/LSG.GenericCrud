import { Component, OnInit } from '@angular/core';
import { HistoricalCrudComponent } from '../../@crud/historical-crud/historical-crud/historical-crud.component';
import { Router } from '@angular/router';
import { ItemService } from 'src/app/@crud/item.service';

@Component({
  selector: 'app-item',
  templateUrl: '../../@crud/historical-crud/historical-crud/historical-crud.component.html'
})
export class ItemOldComponent extends HistoricalCrudComponent implements OnInit {
  constructor(
    router: Router,
    service: ItemService
  ) {   
    service.entityName = "items";
    super(router, service);

    super.entityName = "items";
    super.columnDefs = [
      { headerName: 'Id', field: 'id', sortable: true },
      { headerName: 'Name', field: 'name', sortable: true },
      { headerName: 'Description', field: 'description', sortable: true }];
  }
  ngOnInit() {
    super.entityName = "items";    
    
    super.ngOnInit();
  }

}
