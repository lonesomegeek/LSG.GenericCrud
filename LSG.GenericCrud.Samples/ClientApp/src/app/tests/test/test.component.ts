import { Component, OnInit } from '@angular/core';
import { Test } from '../../models/test';
import { Crud } from '../../models/crud';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {
  model: Crud = new Test();
  
  constructor() { }

  ngOnInit(): void {
  }

}
