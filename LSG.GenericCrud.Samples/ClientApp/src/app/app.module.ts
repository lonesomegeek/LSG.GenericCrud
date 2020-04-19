import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { AgGridModule } from 'ag-grid-angular';
import { ItemComponent } from './components/items/item/item.component';
import { ItemDetailComponent } from './components/items/item-detail/item-detail.component';
import { ShareComponent } from './components/shares/share/share.component';
import { ShareDetailComponent } from './components/shares/share-detail/share-detail.component';
import { CommonModule } from '@angular/common';
import { ContactComponent } from './components/contacts/contact/contact.component';
import { ContactDetailComponent } from './components/contacts/contact-detail/contact-detail.component';
import { ContributorComponent } from './components/contributors/contributor/contributor.component';
import { ContributorDetailComponent } from './components/contributors/contributor-detail/contributor-detail.component';
import { UserComponent } from './components/users/user/user.component';
import { UserDetailComponent } from './components/users/user-detail/user-detail.component';
import { BlobPostComponent } from './components/blog-posts/blob-post/blob-post.component';
import { BlobPostDetailComponent } from './components/blog-posts/blob-post-detail/blob-post-detail.component';
import { CrudComponent } from './components/@crud/crud/crud.component';
import { CrudDetailComponent } from './components/@crud/crud-detail/crud-detail.component';
import { AboutComponent } from './components/about/about.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
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
    BlobPostDetailComponent,
    AboutComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    AgGridModule.withComponents([]),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'about', component: AboutComponent },
      
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
