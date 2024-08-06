# Tibber Cleaner Robot

Welcome to the **Tibber Cleaner Robot** project! This microservice simulates a cleaning robot navigating an office space, executing movement commands, and reporting the number of unique locations cleaned.

## Overview

The Tibber Cleaner Robot microservice listens for HTTP POST requests on port 5000. It processes movement commands to simulate the robot's path and cleans each vertex it visits. After completing the cleaning process, the robot reports the number of unique locations cleaned and stores the results in a database. The result is returned in JSON format.

## Features

- **Movement Commands**: Process commands to move the robot in different directions.
- **Unique Cleaning Locations**: Calculate and report the number of unique vertices cleaned.
- **Database Storage**: Save execution details including timestamp, command count, result, and duration.
- **Configurable Database Connection**: Set database connection details via environment variables.
- **Docker Support**: Easily run the service using Docker and Docker Compose.

## API Endpoint

### POST /tibber-developer-test/enter-path

**Request Body:**

```json
{
  "start": {
    "x": 10,
    "y": 22
  },
  "commands": [
    {
      "direction": "east",
      "steps": 2
    },
    {
      "direction": "north",
      "steps": 1
    }
  ]
}
```

**Response Example:**
```json
{
  "id": 1234,
  "timestamp": "2018-05-12T12:45:10.851596Z",
  "commands": 2,
  "result": 4,
  "duration": 0.000123
}
```
## Fields

- id: Unique identifier for the execution record.
- timestamp: Time when the record was created.
- commands: Number of command elements processed.
- result: Number of unique locations cleaned.
- duration: Time taken for the calculation in seconds.

## Running the Service
1. Build and Run with Docker Compose:
```bash
docker-compose up
```
This command will build the Docker image (if not already built) and start the container with the necessary services.

2. Access the service swagger:
   Once the service is running, you can access the swagger at http://localhost:5000/swagger.

3. Stop the servive:

```bash
docker-compose down
```

For further configuration or troubleshooting, refer to the Docker and Docker Compose documentation or check the service logs for any errors or messages.

## Contact
For questions or support, please contact `k.amirhosseinn@gmail.com`.