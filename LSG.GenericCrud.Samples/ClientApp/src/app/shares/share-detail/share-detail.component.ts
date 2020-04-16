import { Component, OnInit } from '@angular/core';
import { HistoricalDetailComponent } from 'src/app/@crud/historical-crud/historical-detail/historical-detail.component';

@Component({
  selector: 'app-share-detail',
  templateUrl: '../../@crud/historical-crud/historical-detail/historical-detail.component.html'
})
export class ShareDetailComponent extends HistoricalDetailComponent implements OnInit {
  ngOnInit() {
    super.entityName = "shares";
    super.ngOnInit();
  }
}
