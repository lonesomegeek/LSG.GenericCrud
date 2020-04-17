import { Component, OnInit } from '@angular/core';
import { HistoricalDetailComponent } from 'src/app/@crud/historical-crud/historical-detail/historical-detail.component';

@Component({
  selector: 'app-item-detail',
  templateUrl: '../../@crud/historical-crud/historical-detail/historical-detail.component.html'
})
export class ItemDetailOldComponent extends HistoricalDetailComponent implements OnInit {

  ngOnInit() {
    super.entityName = "items";
    super.ngOnInit();
  }
}
