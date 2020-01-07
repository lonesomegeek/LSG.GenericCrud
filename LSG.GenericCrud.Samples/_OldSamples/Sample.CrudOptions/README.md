# How to launch this sample
1. Open this sample solution in Visual Studio
2. Press F5 to get up'n'running

# Routes available in this sample

1. Retreive an access token: in postman, execute step _1. Retreive access token with required scopes_
2. Copy the access_token in the response
3. For each call you want to make with the following endpoints, you will need to change the authorization header to: Authorization: Bearer 'access token copied in step #2'

| VERB   | URL               | Description           |
|--------|-------------------|-----------------------|
| GET    | /api/accounts     | Retreive all accounts |
| GET    | /api/accounts/:id | Retreive one account  |
| POST   | /api/accounts     | Create one account    |
| PUT    | /api/accounts/:id | Update one account    |
| DELETE | /api/accounts/:id | Delete one account    |

Note: For the purpose of this demonstration, PUT and DELETE routes are protected and they request a scope that you will never have. In that case, you will not be able to update and delete in this sample.

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/1074bb27ba9ab15b3445)