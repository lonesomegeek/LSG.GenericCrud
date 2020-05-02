import { Component, OnInit, Input } from '@angular/core';
import { Crud } from '../../models/crud';

@Component({
  selector: 'app-crud',
  templateUrl: './crud.component.html',
  styleUrls: ['./crud.component.css']
})
export class CrudComponent implements OnInit {
  @Input()
  model: Crud;

  constructor() { }

  ngOnInit(): void {
  }

}
