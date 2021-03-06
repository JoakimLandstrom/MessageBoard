# Message Board

A simple message board

## Getting Started

These instructions will get you up and running on your local machine for development and testing purposes.

## Features

To create/read/update a message to the board you need to have authorization.
A user will be created for you if you are not an existing user already, credentials are provided via basic auth flow. 

Endpoints can be found at localhost:5000/swagger

### Prerequisites

```
.net core 3.1
docker
```
## Building the app

To build the app, execute the command below in the App folder:

```
dotnet build
```

## Running the app

To run the app, execute the command below in the App folder:

```
dotnet run
```
The app will run on port: 5000

## Running the app in docker

Build the docker image by running this command in the root folder: 

```
docker build -t  app .
```

Run the image: 

```
docker run -p 5000:5000 app:latest
```

The app should be available at port: 5000

## Running the tests

To run the tests, execute the command below in the Tests folder:

```
dotnet test
```
