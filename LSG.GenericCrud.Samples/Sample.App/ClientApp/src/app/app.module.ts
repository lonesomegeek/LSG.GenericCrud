import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CrudItemComponent } from './_old/items_/items.component';
import { MostRecentlyUsedComponent } from './_old/most-recently-used_/most-recently-used.component';
import { ItemEditDataComponent } from './_old/items_/items-edit.component';
import { AccountDataComponent } from './_old/accounts_/accounts.component';
import { AccountEditDataComponent } from './_old/accounts_/accounts-edit.component';
import { AgGridModule } from "ag-grid-angular";
import { CrudBaseComponent } from './_old/_generics_/crud.component';
import { ItemComponent } from './items/item/item.component';
import { ItemDetailComponent } from './items/item-detail/item-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ItemEditDataComponent,
    AccountDataComponent,
    AccountEditDataComponent,
    MostRecentlyUsedComponent,
    CrudBaseComponent,
    CrudItemComponent,
    ItemComponent,
    ItemDetailComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AgGridModule.withComponents([]),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'items', component: ItemComponent },
      { path: 'items/:id', component: ItemDetailComponent },
      { path: 'items/create', component: ItemDetailComponent },
      { path: 'accounts', component: AccountDataComponent },
      { path: 'accounts/:id', component: AccountEditDataComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
