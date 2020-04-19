import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/@crud/base.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-blob-post',
  templateUrl: './blob-post.component.html',
  styleUrls: ['./blob-post.component.css']
})
export class BlobPostComponent implements OnInit {
  entityName: string = "blogposts";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true },
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
