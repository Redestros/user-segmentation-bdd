Feature: Manage segments in system

    Background: 
        Given the following segments
            | Name    |
            | Silver  |
            | Gold    |
    
    Scenario: Segments List should not be empty
        When I get the segment list
        Then segment list should not be empty
        
    Scenario: Segment get created successfully
        When I create segments with the following detail
            | Name      |
            | Platinum |
            | VIP       |
        Then segments are created successfully