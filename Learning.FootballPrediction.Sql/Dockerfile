FROM mcr.microsoft.com/mssql/server:2017-latest

COPY sql-scripts/setup.sql setup.sql
COPY entrypoint.sh entrypoint.sh
COPY setup.sh setup.sh
RUN chmod +x entrypoint.sh
RUN chmod +x setup.sh

CMD /bin/bash entrypoint.sh