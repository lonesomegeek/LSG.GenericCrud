import { Component, OnInit } from '@angular/core';
import { Crud } from '../../models/crud';
import { Test } from '../../models/test';

@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent implements OnInit {
  model: Crud = new Test();

  constructor() { }

  ngOnInit(): void {
  }

}
