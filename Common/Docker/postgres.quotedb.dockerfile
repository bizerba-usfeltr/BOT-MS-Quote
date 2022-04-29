FROM 		postgres:14.2-bullseye

LABEL       author="Keith Lester"
LABEL       creationDate="8 April 2022"
LABEL       description="This creates the sample database for the Quote data microservice and populates it with sample starter data."

ENV			POSTGRES_PASSWORD 'Bizerba1'

COPY        ./config/postgresql.conf      /tmp/postgresql.conf
COPY        ./scripts ./docker-entrypoint-initdb.d
