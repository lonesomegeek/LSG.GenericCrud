import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contributor',
  templateUrl: './contributor.component.html',
  styleUrls: ['./contributor.component.css']
})
export class ContributorComponent implements OnInit {
  entityName: string = "contributors";
  columnDefs = [
    { headerName: 'Id', field: 'id', sortable: true },
    { headerName: 'Name', field: 'name', sortable: true },
    { headerName: 'GitHubRepository', field: 'gitHubRepository', sortable: true }];
  constructor() { }

  ngOnInit() {
  }

}
