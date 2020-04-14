import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AgGridModule } from 'ag-grid-angular';
import { CrudComponent } from './@crud/crud/crud.component';
import { HistoricalDetailComponent } from './@crud/historical-crud/historical-detail/historical-detail.component';
import { HistoricalCrudComponent } from './@crud/historical-crud/historical-crud/historical-crud.component';
import { ShareComponent } from './shares/share/share.component';
import { ShareDetailComponent } from './shares/share-detail/share-detail.component';
import { ItemComponent } from './items/item/item.component';
import { ItemDetailComponent } from './items/item-detail/item-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CrudComponent,
    HistoricalCrudComponent,
    HistoricalDetailComponent,
    ShareComponent,
    ShareDetailComponent,
    ItemComponent,
    ItemDetailComponent
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

      { path: 'items', component: ItemComponent,  },
      { path: 'items/:id', component: ItemDetailComponent,  },
      { path: 'items/create', component: ItemDetailComponent,  },

      { path: 'shares',         component: ShareComponent,  },
      { path: 'shares/:id',     component: ShareDetailComponent,  },
      { path: 'shares/create',  component: ShareDetailComponent,  },

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
