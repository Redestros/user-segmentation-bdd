Feature: Manage users in the system    


Scenario: Users get created successfully
	When I create users with the following details
	     | Username | Email           |
         | user1    | user1@gmail.com |
         | user2    | user2@gmail.com |   
	Then users are created successfully