### build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Copy the source
COPY src /src

### Build the release app
WORKDIR /src
RUN dotnet publish -c Release -o /app

    
###########################################################


### build the runtime container
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime

### create a user
### dotnet needs a home directory
RUN addgroup -S mikv && \
    adduser -S mikv -G mikv && \
    mkdir -p /home/mikv && \
    chown -R mikv:mikv /home/mikv

WORKDIR /app
COPY --from=build /app .
RUN chown -R mikv:mikv /app

# run as the mikv user
USER mikv

EXPOSE 8080

ENTRYPOINT [ "dotnet",  "mikv.dll" ]
