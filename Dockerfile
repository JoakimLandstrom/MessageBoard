## Build app
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder

COPY MessageBoard/ MessageBoard/

WORKDIR /MessageBoard/App

RUN dotnet publish -r linux-x64 -o ../../Release

## Run app
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime

COPY --from=builder /Release /Release

WORKDIR /Release

ENTRYPOINT [ "./App" ]
