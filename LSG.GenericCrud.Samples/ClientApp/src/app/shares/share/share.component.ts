import { Component, OnInit } from '@angular/core';
import { HistoricalCrudComponent } from 'src/app/@crud/historical-crud/historical-crud/historical-crud.component';
import { Router } from '@angular/router';
import { ItemService } from 'src/app/@crud/item.service';

@Component({
  selector: 'app-share',
  templateUrl: '../../@crud/historical-crud/historical-crud/historical-crud.component.html'
})
export class ShareComponent extends HistoricalCrudComponent implements OnInit {
  constructor(
    router: Router,
    service: ItemService
  ) {   
    service.entityName = "shares";
    super(router, service);

    super.entityName = "shares";
    super.columnDefs = [
      { headerName: 'Id', field: 'id', sortable: true },
      { headerName: 'Contact', field: 'contactId', sortable: true },
      { headerName: 'Item', field: 'itemId', sortable: true },      
      { headerName: 'Reminder', field: 'sharingReminder', sortable: true },
      { headerName: 'Description', field: 'description', sortable: true }];
  }

  ngOnInit() {
    super.entityName = "items";    
    
    super.ngOnInit();
  }
}
