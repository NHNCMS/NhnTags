# NHN Tags Microservice

## Introduction
The NHN Tags Microservice is a component of the NHN project that exposes a group of endpoints to manage tags. This microservice provides the ability to create, update, edit, and delete tags within the NHN system.

## Features
- Create new tags
- Update existing tags
- Edit tag details
- Delete tags

## Endpoints
The following endpoints are available for managing tags:
- POST / - create a new tag
- PUT /{id} - update an existing tag
- GET /{id} - retrieve details for a single tag
- DELETE /{id} - delete a tag
- GET / - retrieve a list of all tags

## Requirements
- NHN project
- .NET 7 or later
- Docker
- Kubernetes

## Installation
To install and run the microservice, follow these steps:
1. Clone the repository:
    git clone https://github.com/nhn/tags-microservice.git
2. Start the Docker container:
    docker run -p 5000:5000 -d nhn/tags-microservice:latest

## Contributing
Contributions to the microservice are welcome. 🤗 

## License
This microservice is released under the MIT license.

## Troubleshooting
If you encounter any issues with the deployment or usage of the microservice, consult the troubleshooting guide for possible solutions.

## Credits
This microservice has been created by the NHN team as part of the NHN project.
