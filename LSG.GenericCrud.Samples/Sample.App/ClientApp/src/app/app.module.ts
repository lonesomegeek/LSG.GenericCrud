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
import { CrudItemComponent } from './items/items.component';
import { MostRecentlyUsedComponent } from './most-recently-used/most-recently-used.component';
import { ItemEditDataComponent } from './items/items-edit.component';
import { AccountDataComponent } from './accounts/accounts.component';
import { AccountEditDataComponent } from './accounts/accounts-edit.component';
import { AgGridModule } from "ag-grid-angular";
import { CrudBaseComponent } from './_generics/crud.component';

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
    CrudItemComponent
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
      { path: 'items', component: CrudItemComponent },
      { path: 'items/:id', component: ItemEditDataComponent },
      { path: 'accounts', component: AccountDataComponent },
      { path: 'accounts/:id', component: AccountEditDataComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
