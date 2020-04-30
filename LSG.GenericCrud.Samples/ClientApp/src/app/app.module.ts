import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AgGridModule } from 'ag-grid-angular';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { AppComponent } from './app.component';

// Import containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';

const APP_CONTAINERS = [
  DefaultLayoutComponent
];

import {
  AppAsideModule,
  AppBreadcrumbModule,
  AppHeaderModule,
  AppFooterModule,
  AppSidebarModule,
} from '@coreui/angular';

// Import routing module
import { AppRoutingModule } from './app.routing';

// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ChartsModule } from 'ng2-charts';
import { HomeComponent } from './views/home/home.component';
import { ContactComponent } from './views/contacts/contact/contact.component';
import { ContactDetailComponent } from './views/contacts/contact-detail/contact-detail.component';
import { CrudComponent } from './views/@crud/crud/crud.component';
import { CrudDetailComponent } from './views/@crud/crud-detail/crud-detail.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ContributorComponent } from './views/contributors/contributor/contributor.component';
import { ContributorDetailComponent } from './views/contributors/contributor-detail/contributor-detail.component';
import { AboutComponent } from './views/about/about.component';
import { ItemComponent } from './views/items/item/item.component';
import { ItemDetailComponent } from './views/items/item-detail/item-detail.component';
import { ShareComponent } from './views/shares/share/share.component';
import { ShareDetailComponent } from './views/shares/share-detail/share-detail.component';
import { UserComponent } from './views/users/user/user.component';
import { UserDetailComponent } from './views/users/user-detail/user-detail.component';
import { BlogPostComponent } from './views/blog-posts/blog-post/blog-post.component';
import { BlogPostDetailComponent } from './views/blog-posts/blog-post-detail/blog-post-detail.component';

@NgModule({
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AppAsideModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ChartsModule,
    FormsModule,
    CommonModule,
    AgGridModule.withComponents([])
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    P404Component,
    P500Component,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ContactComponent,
    ContactDetailComponent,
    ContributorComponent,
    ContributorDetailComponent,
    AboutComponent,
    ItemComponent,
    ItemDetailComponent,
    ShareComponent,
    ShareDetailComponent,
    UserComponent,
    UserDetailComponent,
    CrudComponent,
    CrudDetailComponent,
    BlogPostComponent,
    BlogPostDetailComponent
  ],
  providers: [{
    provide: LocationStrategy,
    useClass: HashLocationStrategy
  }],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
