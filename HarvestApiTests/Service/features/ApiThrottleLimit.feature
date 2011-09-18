Feature: Addition
	In order to avoid my code form dealing with the API throttle limit
	As a developer
	I want to make my service request and expect the service wrapper to deal with the throttle

# The following test may fail if your internet connection is slow
@TestClientRecord
Scenario: Exceed the throttle limit
	When I call "ToggleClientState(clientID)" 122 times
	Then an exception should not be received
	And a wait for the throttle to clear should of happened
