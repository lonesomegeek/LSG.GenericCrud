import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-blob-post-detail',
  templateUrl: './blob-post-detail.component.html',
  styleUrls: ['./blob-post-detail.component.css']
})
export class BlobPostDetailComponent implements OnInit {
  entityName: string = "blogposts";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'Title', field: 'title', sortable: true },
    { headerName: 'Text', field: 'text', sortable: true }];  
  constructor() { }

  ngOnInit() {
  }

}
