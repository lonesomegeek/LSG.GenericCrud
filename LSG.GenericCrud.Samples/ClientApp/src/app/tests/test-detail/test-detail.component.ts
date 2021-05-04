import { Component, OnInit } from '@angular/core';
import { CrudBase } from '../../models/crud';
import { Test } from '../../models/test';

@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent implements OnInit {
  model: CrudBase = new Test();

  constructor() { }

  ngOnInit(): void {
  }

}
