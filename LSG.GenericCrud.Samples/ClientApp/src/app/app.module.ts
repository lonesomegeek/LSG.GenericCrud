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
import { ItemComponent } from './items/item/item.component';
import { ItemDetailComponent } from './items/item-detail/item-detail.component';
import { ShareComponent } from './shares/share/share.component';
import { ShareDetailComponent } from './shares/share-detail/share-detail.component';
import { CommonModule } from '@angular/common';
import { ContactComponent } from './contacts/contact/contact.component';
import { ContactDetailComponent } from './contacts/contact-detail/contact-detail.component';
import { ContributorComponent } from './contributors/contributor/contributor.component';
import { ContributorDetailComponent } from './contributors/contributor-detail/contributor-detail.component';
import { UserComponent } from './users/user/user.component';
import { UserDetailComponent } from './users/user-detail/user-detail.component';
import { BlobPostComponent } from './blog-posts/blob-post/blob-post.component';
import { BlobPostDetailComponent } from './blog-posts/blob-post-detail/blob-post-detail.component';
import { CrudComponent } from './@crud/crud/crud.component';
import { CrudDetailComponent } from './@crud/crud-detail/crud-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CrudComponent,
    CrudDetailComponent,
    ShareComponent,
    ShareDetailComponent,
    ItemComponent,
    ItemDetailComponent,
    ContactComponent,
    ContactDetailComponent,
    ContributorComponent,
    ContributorDetailComponent,
    UserComponent,
    UserDetailComponent,
    BlobPostComponent,
    BlobPostDetailComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
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

      { path: 'contacts',         component: ContactComponent,  },
      { path: 'contacts/:id',     component: ContactDetailComponent,  },
      { path: 'contacts/create',  component: ContactDetailComponent,  },

      { path: 'contributors',         component: ContributorComponent,  },
      { path: 'contributors/:id',     component: ContributorDetailComponent,  },
      { path: 'contributors/create',  component: ContributorDetailComponent,  },

      { path: 'users',         component: UserComponent,  },
      { path: 'users/:id',     component: UserDetailComponent,  },
      { path: 'users/create',  component: UserDetailComponent,  },

      { path: 'blogposts',         component: BlobPostComponent,  },
      { path: 'blogposts/:id',     component: BlobPostDetailComponent,  },
      { path: 'blogposts/create',  component: BlobPostDetailComponent,  },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
