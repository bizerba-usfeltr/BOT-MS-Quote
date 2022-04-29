Here are some common Docker commands for settings up the containers used in the application.

Section 1: Postgres

Part A: Building the container

docker build -t bot-services-quote-postgres:1.0.0 -f postgres.quotedb.dockerfile .

-t 	The name of the image to create
-f	The docker filed used to compose the image

Notes:
	- The assumption is that the build command will be run from the Docker directory (the parent directory for this file)
	- Do not forget the '.' at the end of the command. That tells Docker build to build from the current directory
	
Part B: Running the container

docker run --name BotQuoteTestDb -p 5454:5432/tcp -d bot-services-quote-postgres:1.0.0

--name	The name that will display for the container in docker
-p	Maps the exposed port (5432) from the container to the local machine port (5454)
-d	Runs the container in detached mode

Notes: 
	- The development environment should be set to use 5454 as the port to connect to the container database
	- The image will execute any scripts specified in the 'scripts' directory in the Docker directory on the Postgres database
	- You have to delete the container and re-run the docker run command to reset the Postgres migration executions