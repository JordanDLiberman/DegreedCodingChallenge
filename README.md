# DegreedCodingChallenge

# API
The API is written in .net core.
## Project structure:
* API Controllers
* Services
* Clients

### Services and Clients share an Interfaces project
### Entities project is used to deserialize client responses to native types

## Unit tests are created around controller methods and service methods.

## Other features used:
* Dependency Injection
* CORS enabled (for local execution of UI and API)
* Swagger UI

# UI
The UI is written in Vue.js using Vueitfy components and axios for API calls.

# Running
The execution is a little complex, due to having to work around CORS issues for localhost on various ports.
*Open API project in Visual Studio and run.
*Open a command line, navigate into /DegreedCodingChallenge/VueUI/joke-ui
*Run command:  npm run serve
*Open a browser and navigate to http://localhost:44332/Jokes