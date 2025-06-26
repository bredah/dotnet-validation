Feature: User Pagination

  Scenario: The response should contain the expected user data
    When I request users from page 1
    Then the user list should contain:
      | first_name | last_name | email                    | avatar                                         |
      | Janet      | Weaver    | janet.weaver@reqres.in   | https://reqres.in/img/faces/2-image.jpg        |
    And the response page number should match the requested page

  Scenario: The response should contain the expected user data from page 2
    When I request users from page 2
    Then the user list should contain:
      | first_name | last_name | email                    | avatar                                         |
      | Byron      | Fields    | byron.fields@reqres.in   | https://reqres.in/img/faces/10-image.jpg       |
    And the response page number should match the requested page
    
  Scenario: The response should be empty when requesting a non-existent page
    When I request users from page 99
    Then the response should contain an empty user list

  Scenario: The response should match the user list schema
    When I request users from page 1
    Then the response should match the "users-list-response.schema" schema