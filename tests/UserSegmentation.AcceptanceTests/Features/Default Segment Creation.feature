Feature: Default_Segment_Creation


Scenario: Default segment exist in system
	When I retrieve the segment list
	Then I should a list having one item and is the default