### Build the container
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
RUN mkdir -p /src/app && \
    mkdir -p /src/unit-tests

### copy the source code
COPY app /src/app
COPY unit-tests /src/unit-tests

## Run unit tests
WORKDIR /src/unit-tests
RUN dotnet test --logger:trx

### Build the app
WORKDIR /src/app
RUN dotnet publish -c Release -o /app


###########################################################

### Build the runtime container
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
EXPOSE 4120
WORKDIR /app

### create a user
RUN groupadd -g 4120 mikv && \
    useradd -r  -u 4120 -g mikv mikv && \
### dotnet needs a home directory
    mkdir -p /home/mikv && \
    chown -R mikv:mikv /home/mikv

### run as mikv user
USER mikv

### copy the app
COPY --from=build /app .

ENTRYPOINT [ "dotnet",  "mikv.dll" ]
