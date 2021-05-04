import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { BaseService } from '../base.service';

@Component({
  selector: 'app-crud-detail',
  templateUrl: './crud-detail.component.html',
  styleUrls: ['./crud-detail.component.css']
})
export class CrudDetailComponent implements OnInit {
  row: any;
  @Input()
  entityName: string;
  @Input()
  columnDefs: any[];
  selectedId: string;
  mode: string = "read";
  isEditing: boolean = false;
  history: Observable<History[]>;
  @Input()
  showHistory: boolean = false;
  historyColumnDefs: any[] = [
    { headerName: 'Action', field: 'action', sortable: true },
    { headerName: 'By', field: 'createdBy', sortable: true },
    { headerName: 'At', field: 'createdDate', sortable: true }];

  constructor(
    private service: BaseService,
    private router: Router,
    route: ActivatedRoute
  ) {
    this.selectedId = route.snapshot.params["id"];
    if (this.selectedId == "create") this.mode = "create";
  }

  ngOnInit() {
    this.service.entityName = this.entityName;
    this.service.baseRoute = "api/" + this.service.entityName;

    if (this.mode != "create") {
      this.service.getOne(this.selectedId).subscribe(e => { this.row = e; });
      if (this.showHistory) {
        this.service.makeRead(this.selectedId).subscribe();
        this.history = this.service.getOneHistory(this.selectedId);
      }
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
      this.router.navigate(['/' + this.entityName]);
    });
  }
}