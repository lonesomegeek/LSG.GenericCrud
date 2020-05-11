import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from '../../@crud/base.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
  entityName: string = "blogposts";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true, hide: true },
    { headerName: 'Title', field: 'title', sortable: true },
    { headerName: 'Text', field: 'text', sortable: true }];  
  rows: Observable<any[]>;

  constructor(private service: BaseService) { 
    this.service.entityName = this.entityName;
    this.service.baseRoute = "api/" + this.entityName;
  }

  ngOnInit() {
    this.rows = this.service.getAll();
  }

}
