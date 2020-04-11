import { Component, OnInit } from '@angular/core';
import { HistoricalDetailComponent } from 'src/app/@crud/historical-crud/historical-detail/historical-detail.component';

@Component({
  selector: 'app-object-detail',
  templateUrl: '../../@crud/historical-crud/historical-detail/historical-detail.component.html'
})
export class ObjectDetailComponent extends HistoricalDetailComponent implements OnInit {

  ngOnInit() {
    super.ngOnInit();
  }

}
