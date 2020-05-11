import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-blog-post-detail',
  templateUrl: './blog-post-detail.component.html',
  styleUrls: ['./blog-post-detail.component.css']
})
export class BlogPostDetailComponent implements OnInit {
  entityName: string = "blogposts";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true, hide: true },
    { headerName: 'Title', field: 'title', sortable: true },
    { headerName: 'Text', field: 'text', sortable: true }];  
  constructor() { }

  ngOnInit() {
  }

}
