import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Home',
    url: '/home',
    icon: 'icon-speedometer',
    badge: {
      variant: 'info',
      text: 'NEW'
    }
  },
  {
    title: true,
    name: 'Components'
  },
  {
    name: 'Contacts',
    url: '/contacts',
    icon: 'icon-puzzle'
  },
  {
    name: 'Items',
    url: '/items',
    icon: 'icon-puzzle'
  },
  {
    name: 'Shares',
    url: '/shares',
    icon: 'icon-puzzle'
  },
  {
    name: 'Contributors',
    url: '/contributors',
    icon: 'icon-puzzle'
  },
  {
    name: 'Users',
    url: '/users',
    icon: 'icon-puzzle'
  },
  {
    title: true,
    name: 'Blog'
  },
  {
    name: 'Blog',
    url: '/blog',
    icon: 'icon-puzzle'
  },
  {
    name: 'Blog posts',
    url: '/blogposts',
    icon: 'icon-puzzle'
  }
  // ,
  // {
  //   name: 'Tests',
  //   url: '/tests',
  //   icon: 'icon-puzzle'
  // }
];
