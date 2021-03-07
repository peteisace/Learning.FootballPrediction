#!/bin/bash

# input parameters
password=$1
name=$2
hostname=$3

CID=$(docker container run -e 'ACCEPT_EULA=Y' -e "SA_PASSWORD=$password" -p 1433:1433 --name $name -h $hostname -d mcr.microsoft.com/mssql/server:2017-latest -v /opt/docker-storage/mssql/fp-db/data:/var/opt/mssql/data  \
-v /opt/docker-storage/mssql/fp-db/log:/var/opt/mssql/log -v /opt/docker-storage/mssql/fp-db/secrets:/var/opt/mssql/secrets)

# Get a sqlcommand 
docker exec -it $name /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $password 