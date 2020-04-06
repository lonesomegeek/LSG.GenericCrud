import { Component, OnInit } from '@angular/core';
import { HistoricalCrudComponent } from 'src/app/@crud/historical-crud/historical-crud/historical-crud.component';

@Component({
  selector: 'app-share',
  templateUrl: '../../@crud/historical-crud/historical-crud/historical-crud.component.html'
})
export class ShareComponent extends HistoricalCrudComponent implements OnInit {

  ngOnInit() {
    super.baseRoute = "api/shares";
    super.columnDefs = [
      { headerName: 'Id', field: 'id', sortable: true },
      { headerName: 'Description', field: 'description', sortable: true }];
    super.ngOnInit();
  }
}
