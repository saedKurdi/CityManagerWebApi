# City Manager API

Welcome to the City Manager API! This API allows users to register, log in, and request information about cities based on their user ID.

## Features

- **User Registration**: Register a new user to get access to the API.
- **User Login**: Log in to obtain an API key.
- **City Requests**: Retrieve city information based on your user ID.

## Getting Started

Follow these steps to get started with the City Manager API:

### 1. Registration

To use the API, you first need to register a new user. Send a `POST` request to the `/register` endpoint with the following JSON payload:

```json
{
  "username": "your_username",
  "password": "your_password"
}
