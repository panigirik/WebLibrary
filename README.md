
Additionally, make sure you have the following installed:

.NET SDK (version 6 or higher)

Docker (optional, for running services like PostgreSQL, Redis, and ClamAV in containers)

1: Clone the Repository
First, clone the project repository to your local machine.

sitory-url>
cd <project-directory>

2: Configure the Database and Redis
PostgreSQL:
Ensure that you have PostgreSQL running locally, and the database Library is available. Use the following connection string to connect to PostgreSQL:
Host=host.docker.internal;Port=5432;Database=<your_db_name>;Username=<your_username>;Password=<your_password>

You can set this up in your local PostgreSQL instance or use Docker to run PostgreSQL locally:
docker run --name postgres-container -e POSTGRES_PASSWORD=Volvos80 -p 5432:5432 -d postgres

Redis:
Redis should be running locally on port 6379. If Redis is not installed, you can run it using Docker:
docker run --name redis-container -p 6379:6379 -d redis

ClamAV:
Make sure ClamAV is installed and running locally on port 3310. If ClamAV is not installed, you can run it using Docker:


3: Configure appsettings.json

4: docker run --name clamav-container -p 3310:3310 -d mkodockx/docker-clamav:latest
