import { Component } from '@angular/core';
import { CrudBase } from '../../models/crud';
import { Test } from '../../models/test';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})

export class TestComponent { 
  model: CrudBase = new Test();
}