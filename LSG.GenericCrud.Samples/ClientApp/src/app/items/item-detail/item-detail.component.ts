import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Item } from 'src/app/models/item';
import { ItemService } from '../item.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  row: Item;
  selectedId: string;
  mode : string = "read";
  isEditing: boolean = false;
  history: Observable<History[]>;
  historyColumnDefs: any[] = [
    { headerName: 'Action', field: 'action', sortable: true },
    { headerName: 'By', field: 'createdBy', sortable: true },
    { headerName: 'At', field: 'createdDate', sortable: true }];

  constructor(
    private service: ItemService,
    private router: Router,
    route: ActivatedRoute
  ) {
    this.selectedId = route.snapshot.params["id"];
    if (this.selectedId == "create") this.mode = "create";
  }

  ngOnInit() {
    if (this.mode != "create") {
      this.service.getOne(this.selectedId).subscribe(e => { this.row = e; });
      this.service.makeRead(this.selectedId).subscribe();
      this.history = this.service.getOneHistory(this.selectedId);
    } else {
      this.row = new Item();
    }
  }

  editActivate() {
    this.mode = "edit";
  }

  editDeactivate() {
    this.mode = "read";
  }

  create() {
    this.service.postOne(this.row).subscribe(result => {      
      this.router.navigate(['/items/' + (result as Item).id]);      
    })
  }

  delete() {
    this.service.deleteOne(this.selectedId).subscribe(result => {
      this.router.navigate(['/items']);
      
    });
  }

  save() {
    this.service.putOne(this.selectedId, this.row).subscribe(result => {
      this.editDeactivate();
    });
  }


}

