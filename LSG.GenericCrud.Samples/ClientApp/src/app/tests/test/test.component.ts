import { Component, OnInit } from '@angular/core';
import { Test } from '../../models/test';
import { CrudBase } from '../../models/crud';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {
  model: CrudBase = new Test();

  constructor() { }

  ngOnInit(): void {
  }

}
