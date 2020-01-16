import { OnInit, Inject, Component, Input } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

export abstract class CrudDataComponent<T> implements OnInit {
    public rows: Observable<T[]>;
  
    abstract columnDefs = [];
  
    abstract baseRoute: string;
    constructor(
      private http: HttpClient,
      @Inject('BASE_URL') private baseUrl: string,
    ) { }
  
    ngOnInit() {
      this.rows = this
        .http
        .get<T[]>(this.baseUrl + this.baseRoute);
    }
    
  }
  
  @Component({
    selector: "crud-component",
    templateUrl: './crud.component.html'
  })
  export class CrudBaseComponent extends CrudDataComponent<any> {
    @Input() columnDefs: any;
    @Input() baseRoute: string;
  }
  