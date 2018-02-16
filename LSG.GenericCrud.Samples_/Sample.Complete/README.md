# How to launch this sample

## With Visual Studio
1. Open this sample in Visual Studio
2. Press F5 to get up'n'running

## With docker
1. Be sure to have Docker installed on your machine: [Link](https://www.docker.com/community-edition#/download)
2. In a terminal/console, run this command: 
```bash
docker run -p 5000:80 -it lonesomegeek/genericcrud.sample.complete:stable
```

# Routes available in this sample

## Accounts: Generic CRUD with entity
Note: There is a custom route to get contacts related to accounts

| VERB   | URL                        | Description                         |
|--------|----------------------------|-------------------------------------|
| GET    | /api/accounts              | Retreive all accounts               |
| GET    | /api/accounts/:id          | Retreive one account                |
| GET    | /api/accounts/:id/contacts | Retreive all contacts of an account |
| POST   | /api/accounts              | Create one account                  |
| PUT    | /api/accounts/:id          | Update one account                  |
| DELETE | /api/accounts/:id          | Delete one account                  |

## Accounts (async): Generic CRUD with entity
Note: There is a custom route to get contacts related to accounts

| VERB   | URL                             | Description                         |
|--------|---------------------------------|-------------------------------------|
| GET    | /api/accountsasync              | Retreive all accounts               |
| GET    | /api/accountsasync/:id          | Retreive one account                |
| GET    | /api/accountsasync/:id/contacts | Retreive all contacts of an account |
| POST   | /api/accountsasync              | Create one account                  |
| PUT    | /api/accountsasync/:id          | Update one account                  |
| DELETE | /api/accountsasync/:id          | Delete one account                  |

## Accounts: Generic CRUD with DTO & entity
| VERB   | URL                        | Description                         |
|--------|----------------------------|-------------------------------------|
| GET    | /api/accountsDto           | Retreive all accounts dto           |
| GET    | /api/accountsDto/:id       | Retreive one account dto            |
| POST   | /api/accountsDto           | Create one account with dto         |
| PUT    | /api/accountsDto/:id       | Update one account with dto         |
| DELETE | /api/accountsDto/:id       | Delete one account                  |

## Accounts (async): Generic CRUD with DTO & entity
| VERB   | URL                             | Description                         |
|--------|---------------------------------|-------------------------------------|
| GET    | /api/accountsDtoasync           | Retreive all accounts dto           |
| GET    | /api/accountsDtoasync/:id       | Retreive one account dto            |
| POST   | /api/accountsDtoasync           | Create one account with dto         |
| PUT    | /api/accountsDtoasync/:id       | Update one account with dto         |
| DELETE | /api/accountsDtoasync/:id       | Delete one account                  |

## Contacts: Generic CRUD with entity
| VERB   | URL                        | Description                         |
|--------|----------------------------|-------------------------------------|
| GET    | /api/contacts              | Retreive all contacts               |
| GET    | /api/contacts/:id          | Retreive one contact                |
| POST   | /api/contacts              | Create one contact                  |
| PUT    | /api/contacts/:id          | Update one contact                  |
| DELETE | /api/contacts/:id          | Delete one contact                  |

## Accounts: Historical CRUD with entity
| VERB   | URL                                  | Description                            |
|--------|--------------------------------------|----------------------------------------|
| GET    | /api/historicalaccounts              | Retreive all accounts                  |
| GET    | /api/historicalaccounts/:id          | Retreive one account                   |
| GET    | /api/historicalaccounts/:id/contacts | Retreive all contacts of an account    |
| POST   | /api/historicalaccounts              | Create one account                     |
| PUT    | /api/historicalaccounts/:id          | Update one account                     |
| DELETE | /api/historicalaccounts/:id          | Delete one account                     |
| GET    | /api/historicalaccounts/:id/history  | Retreive and account history           |
| POST   | /api/historicalaccounts/:id/restore  | Restore a deleted account              |

## Accounts (async): Historical CRUD with entity
| VERB   | URL                                       | Description                            |
|--------|-------------------------------------------|----------------------------------------|
| GET    | /api/historicalaccountsasync              | Retreive all accounts                  |
| GET    | /api/historicalaccountsasync/:id          | Retreive one account                   |
| GET    | /api/historicalaccountsasync/:id/contacts | Retreive all contacts of an account    |
| POST   | /api/historicalaccountsasync              | Create one account                     |
| PUT    | /api/historicalaccountsasync/:id          | Update one account                     |
| DELETE | /api/historicalaccountsasync/:id          | Delete one account                     |
| GET    | /api/historicalaccountsasync/:id/history  | Retreive and account history           |
| POST   | /api/historicalaccountsasync/:id/restore  | Restore a deleted account              |

## Accounts: Historical CRUD with DTO & entity
| VERB   | URL                                     | Description                         |
|--------|-----------------------------------------|-------------------------------------|
| GET    | /api/historicalaccountsdto              | Retreive all accounts               |
| GET    | /api/historicalaccountsdto/:id          | Retreive one account                |
| GET    | /api/historicalaccountsdto/:id/contacts | Retreive all contacts of an account |
| POST   | /api/historicalaccountsdto              | Create one account                  |
| PUT    | /api/historicalaccountsdto/:id          | Update one account                  |
| DELETE | /api/historicalaccountsdto/:id          | Delete one account                  |
| GET    | /api/historicalaccountsdto/:id/history  | Retreive and account history        |
| POST   | /api/historicalaccountsdto/:id/restore  | Restore a deleted account           |

## Accounts (async): Historical CRUD with DTO & entity
| VERB   | URL                                          | Description                         |
|--------|----------------------------------------------|-------------------------------------|
| GET    | /api/historicalaccountsdtoasync              | Retreive all accounts               |
| GET    | /api/historicalaccountsdtoasync/:id          | Retreive one account                |
| GET    | /api/historicalaccountsdtoasync/:id/contacts | Retreive all contacts of an account |
| POST   | /api/historicalaccountsdtoasync              | Create one account                  |
| PUT    | /api/historicalaccountsdtoasync/:id          | Update one account                  |
| DELETE | /api/historicalaccountsdtoasync/:id          | Delete one account                  |
| GET    | /api/historicalaccountsdtoasync/:id/history  | Retreive and account history        |
| POST   | /api/historicalaccountsdtoasync/:id/restore  | Restore a deleted account           |

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/9a3fb545124c6f36cbd8)