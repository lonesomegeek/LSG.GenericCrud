import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contributor-detail',
  templateUrl: './contributor-detail.component.html',
  styleUrls: ['./contributor-detail.component.css']
})
export class ContributorDetailComponent implements OnInit {
  entityName: string = "contributors";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true, hide: true },
    { headerName: 'Name', field: 'name', sortable: true },
    { headerName: 'GitHubRepository', field: 'gitHubRepository', sortable: true }];
  constructor() { }

  ngOnInit() {
  }

}
