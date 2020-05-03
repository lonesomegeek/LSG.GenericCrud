import { Component, OnInit, Input, Inject } from '@angular/core';
import { CrudBase } from '../../models/crud';
import { BaseService } from '../../views/@crud/base.service';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-crud-detail',
  templateUrl: './crud-detail.component.html',
  styleUrls: ['./crud-detail.component.css']
})
export class CrudDetailComponent implements OnInit {
  row: any;
  @Input()
  model: CrudBase;
  @Input()
  showHistory: boolean = false;
  history: Observable<History[]>;
  service: BaseService;
  historyColumnDefs: any[] = [
    { headerName: 'Action', field: 'action', sortable: true },
    { headerName: 'By', field: 'createdBy', sortable: true },
    { headerName: 'At', field: 'createdDate', sortable: true }];
  mode: string = "read";
  selectedId: string;

  constructor(    
    private router: Router,
    private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    route: ActivatedRoute
  ) {
    this.selectedId = route.snapshot.params["id"];
    this.service = new BaseService(this.httpClient, this.baseUrl)
    if (!this.selectedId) this.mode = "create";
  }

  ngOnInit(): void {
    this.service.baseRoute = "api/" + this.model.entityName;
    if (this.mode != "create") {      
        this.service.getOne(this.selectedId).subscribe(e => { this.row = e; });
        if (this.showHistory) {
          this.service.makeRead(this.selectedId).subscribe();
          this.history = this.service.getOneHistory(this.selectedId);
        }      
    }
  }

}
