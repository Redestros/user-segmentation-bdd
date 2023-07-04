Feature: Manage segments in system

    Scenario: Initial segments should exist
        When I get the segment list
        Then segment list should not be empty

    Scenario: Segment get created successfully
        When I create segments with the following detail
          | Name |
          | VIP  |
        Then segments are created successfully