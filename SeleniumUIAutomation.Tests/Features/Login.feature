@login
Feature: Login Functionality
    As a user
    I want to be able to login to the application
    So that I can access my account

Background:
    Given I am on the login page

@smoke @positive
Scenario: Successful login with valid credentials
    When I enter username "testuser@example.com"
    And I enter password "Password123"
    And I click the login button
    Then I should be redirected to the home page
    And I should see a welcome message

@negative
Scenario: Login with invalid credentials
    When I enter username "invalid@example.com"
    And I enter password "wrongpassword"
    And I click the login button
    Then I should see an error message "Invalid username or password"

@negative
Scenario: Login with empty credentials
    When I click the login button
    Then I should see an error message "Please enter username and password"

@negative
Scenario Outline: Login with various invalid inputs
    When I enter username "<username>"
    And I enter password "<password>"
    And I click the login button
    Then I should see an error message "<error_message>"

    Examples:
        | username              | password     | error_message                    |
        | invalid@example.com   | wrongpass    | Invalid username or password     |
        |                       | Password123  | Please enter username            |
        | testuser@example.com  |              | Please enter password            |
        | notanemail            | Password123  | Please enter a valid email       |
