1.  **Add Individual**

    `POST /reports/create`

    - **Description:** Add a new individual to the database.
    - **Request Body:**
      ```json
      {
        "firstname": "John",
        "lastname": "Doe",
        "age": 30,
        "gender": "male",
        "isAlive": true,
        "location": {
          "latitude": 42.13215,
          "longitude": 32.432
        }
      }
      ```
    - **Response (200 OK):**

      ```json
      {
        "id": "78fea950-7e31-4be5-8c86-dbbe35bed423",
        "firstname": "John",
        "lastname": "Doe",
        "age": 30,
        "gender": "male",
        "isAlive": true,
        "location": {
          "latitude": 133.3451,
          "longitude": 12.325,
          "hemisphere": "N"
        }
      }
      ```

2.  **Update Survivor Location**

    `PUT /reports/update/78fea950-7e31-4be5-8c86-dbbe35bed423`

    - **Description:** Update the location of a survivor identified by their first- and last name
    - **Request Body:**
      ```json
      {
        "latitude": 40.7128,
        "longitude": -74.006
      }
      ```
    - **Response (200 OK):**
      ```json
      {
        "latitude": 40.7128,
        "longitude": -74.006,
        "hemisphere": "N"
      }
      ```

3.  **Get Percentage of Survivors**

    `GET /reports/percentage`

    - **Description:** Get the percentage of survivors compared to the total number of individuals.
    - **Response (200 OK):**
      ```json
      {
        "percentage": 75
      }
      ```

4.  **Find Survivors**

    `GET /reports/survivors`

    - **Description:** List all survivors
    - **Response (200 OK):**
      ```json
      [
        {
          "id": "78fea950-7e31-4be5-8c86-dbbe35bed423",
          "firstName": "Jane",
          "lastName": "Smith",
          "isAlive": true,
          "location": {
            "latitude": 55.676098,
            "longitude": 12.568337,
            "hemisphere": "N"
          }
        },
        {
          "id": "78fea950-7e31-4be5-8c86-dbbe35bed423",
          "firstname": "Alex",
          "lastname": "Johnson",
          "isalive": true,
          "location": {
            "latitude": 51.5074,
            "longitude": -0.1278,
            "hemisphere": "N"
          }
        }
      ]
      ```

5.  **Search by Lastname**

    `GET /reports/search?lastName=Smith`

    - **Description:** Search for individuals by their lastname and check if they're deceased.
    - **Response (200 OK):**

      ```json
      [
        {
          "id": 1,
          "firstname": "Jane",
          "lastname": "Smith",
          "isAlive": true,
          "location": {
            "latitude": 55.676098,
            "longitude": 12.568337,
            "hemisphere": "N"
          }
        },
        {
          "id": 8,
          "firstname": "Michael",
          "lastname": "Smith",
          "isAlive": true,
          "location": {
            "latitude": 133.3451,
            "longitude": 12.325,
            "hemisphere": "N"
          }
        }
      ]
      ```
