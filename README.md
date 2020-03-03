# Message Board

A simple message board

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them

```
net core 3.1
docker
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
docker run -p 80:5000 app:latest
```

The app should be available at port: 5000

## Running the tests

To run the tests, execute the command below in the Tests folder:

```
dotnet test
```