Feature: SegmentAssignment
When a user's financials are updated, he will be automatically assigned to a segment

    Scenario Outline: User will be assigned to segment after he updates his financials
        Given an existing user born on <Birthdate>
        When he update his financials including <GrossAnnualRevenue> and <SocialScore>
        Then he should be assigned to a <Segment>

    Examples:     
      | Birthdate  | GrossAnnualRevenue | SocialScore | Segment |
      | 1980-01-01 | 29000              | 80          | Default |
      | 1980-01-01 | 34000              | 80          | Default |
      | 2006-01-01 | 34000              | 80          | Default |
      | 1980-01-01 | 34000              | 90          | Silver  |
      | 1980-01-01 | 64000              | 100         | Gold    |