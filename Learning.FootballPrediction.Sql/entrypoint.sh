#!/bin/bash
echo "$0: Starting SQL Server"
./setup.sh $REBUILD_DATA & /opt/mssql/bin/sqlservr