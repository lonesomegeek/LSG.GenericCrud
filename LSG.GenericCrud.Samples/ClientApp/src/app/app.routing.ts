import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { HomeComponent } from './views/home/home.component';
import { ContactComponent } from './views/contacts/contact/contact.component';
import { ContactDetailComponent } from './views/contacts/contact-detail/contact-detail.component';
import { AboutComponent } from './views/about/about.component';
import { ItemComponent } from './views/items/item/item.component';
import { ItemDetailComponent } from './views/items/item-detail/item-detail.component';
import { ShareComponent } from './views/shares/share/share.component';
import { ShareDetailComponent } from './views/shares/share-detail/share-detail.component';
import { ContributorComponent } from './views/contributors/contributor/contributor.component';
import { ContributorDetailComponent } from './views/contributors/contributor-detail/contributor-detail.component';
import { UserComponent } from './views/users/user/user.component';
import { UserDetailComponent } from './views/users/user-detail/user-detail.component';
import { BlogPostComponent } from './views/blog-posts/blog-post/blog-post.component';
import { BlogPostDetailComponent } from './views/blog-posts/blog-post-detail/blog-post-detail.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: '404',
    component: P404Component,
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    component: P500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'base',
        loadChildren: () => import('./views/base/base.module').then(m => m.BaseModule)
      },
      {
        path: 'buttons',
        loadChildren: () => import('./views/buttons/buttons.module').then(m => m.ButtonsModule)
      },
      {
        path: 'charts',
        loadChildren: () => import('./views/chartjs/chartjs.module').then(m => m.ChartJSModule)
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./views/dashboard/dashboard.module').then(m => m.DashboardModule)
      },
      { path: 'home',             component: HomeComponent },
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

      { path: 'blogposts',         component: BlogPostComponent,  },
      { path: 'blogposts/:id',     component: BlogPostDetailComponent,  },
      { path: 'blogposts/create',  component: BlogPostDetailComponent,  },
      
      {
        path: 'icons',
        loadChildren: () => import('./views/icons/icons.module').then(m => m.IconsModule)
      },
      {
        path: 'notifications',
        loadChildren: () => import('./views/notifications/notifications.module').then(m => m.NotificationsModule)
      },
      {
        path: 'theme',
        loadChildren: () => import('./views/theme/theme.module').then(m => m.ThemeModule)
      },
      {
        path: 'widgets',
        loadChildren: () => import('./views/widgets/widgets.module').then(m => m.WidgetsModule)
      }
    ]
  },
  { path: '**', component: P404Component }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
