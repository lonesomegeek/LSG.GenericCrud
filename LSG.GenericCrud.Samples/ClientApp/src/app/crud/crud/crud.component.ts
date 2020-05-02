import { Component, OnInit, Input, Inject } from '@angular/core';
import { CrudBase } from '../../models/crud';
import { BaseService } from '../../views/@crud/base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-crud',
  templateUrl: './crud.component.html',
  styleUrls: ['./crud.component.css']
})
export class CrudComponent implements OnInit {
  @Input()
  model: CrudBase;
  service: BaseService;
  rows: Observable<any[]>;

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { 
    this.service = new BaseService(httpClient, baseUrl);
  }

  ngOnInit(): void {  
    this.service.baseRoute = "api/" + this.model.entityName;
    this.rows = this.service.getAll();
  }

}
