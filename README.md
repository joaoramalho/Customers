# Customers Application

This is a project App for take home assignment requested by Pinewood.

# Backend

Solution contains 2 projects (Customers.Data) where DB (EF Core - code first) is used to declare the objects and create the DB based on the migrations.
If you have EF tools, run: `dotnet ef database update --context ApplicationDBContext --project Customers.Data --startup-project Customers.Web`.

# Frontend

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 18.2.8.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Tests
There are no tests on this project due to the time restriction, but in a real world situation, tests like unit tests, e2e tests, performance, etc... are mandatory for a stable and performant system.

Joao Ramalho (joaodramalho@gmail.com)