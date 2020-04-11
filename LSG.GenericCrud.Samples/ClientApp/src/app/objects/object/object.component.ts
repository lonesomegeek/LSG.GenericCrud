import { Component, OnInit } from '@angular/core';
import { HistoricalCrudComponent } from '../../@crud/historical-crud/historical-crud/historical-crud.component';

@Component({
  selector: 'app-objects',
  templateUrl: '../../@crud/historical-crud/historical-crud/historical-crud.component.html'
})
export class ObjectComponent extends HistoricalCrudComponent implements OnInit {

  ngOnInit() {
    super.baseRoute = "api/objects";
    super.columnDefs = [
      { headerName: 'Id', field: 'id', sortable: true },
      { headerName: 'Name', field: 'name', sortable: true },
      { headerName: 'Description', field: 'description', sortable: true }];
    super.ngOnInit();
  }

}
