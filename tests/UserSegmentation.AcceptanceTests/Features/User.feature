Feature: Manage users in the system

    Background:
        Given the following users
          | Username | Email             |
          | draven   | alfredo@gmail.com |
          | jhin     | jhin@gmail.com    |
          | varus    | varus@gmail.com   |

    Scenario: Users list should not be empty
        When I get the users list
        Then users list should not be empty

    Scenario: Users get created successfully
        When I create users with the following details
          | Username | Email           | Firstname | Lastname | Birthdate  | PhoneNumber |
          | user1    | user1@gmail.com | user      | one      | 01/01/1998 | 25994801    |
          | user2    | user2@gmail.com | user      | two      | 02/03/1998 | 25994801    |
        Then users are created successfully
        And users are assigned to default segment

    Scenario: Users update their personal info successfully
        Given an existing user
          | Username |
          | draven   |
        When I update his personal info with
          | FirstName | LastName        | PhoneNumber |
          | Draven    | The executioner | 28870101    |
        Then personal infos are updated successfully