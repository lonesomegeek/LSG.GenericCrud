import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { ItemService } from '../../item.service';

@Component({
  selector: 'app-historical-detail',
  templateUrl: './historical-detail.component.html',
  styleUrls: ['./historical-detail.component.css']
})
export class HistoricalDetailComponent implements OnInit {
  row: any;
  entityName: string;
  selectedId: string;
  mode: string = "read";
  isEditing: boolean = false;
  history: Observable<History[]>;
  historyColumnDefs: any[] = [
    { headerName: 'Action', field: 'action', sortable: true },
    { headerName: 'By', field: 'createdBy', sortable: true },
    { headerName: 'At', field: 'createdDate', sortable: true }];

  constructor(
    private service: ItemService,
    private router: Router,
    route: ActivatedRoute
  ) {
    this.selectedId = route.snapshot.params["id"];
    if (this.selectedId == "create") this.mode = "create";
  }

  ngOnInit() {
    if (this.mode != "create") {
      this.service.getOne(this.selectedId).subscribe(e => { this.row = e; });
      this.service.makeRead(this.selectedId).subscribe();
      this.history = this.service.getOneHistory(this.selectedId);
    } else {
      this.row = {};
    }
  }

  editActivate() {
    this.mode = "edit";
  }

  editDeactivate() {
    this.mode = "read";
  }

  create() {
    this.service.postOne(this.row).subscribe(result => {
      this.router.navigate(['/' + this.entityName]);
    })
  }

  delete() {
    this.service.deleteOne(this.selectedId).subscribe(result => {
      this.router.navigate(['/' + this.entityName]);

    });
  }

  save() {
    this.service.putOne(this.selectedId, this.row).subscribe(result => {
      this.editDeactivate();
    });
  }
}