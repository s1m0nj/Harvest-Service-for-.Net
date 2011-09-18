Feature: Client Service Requests
	In order to simplify interactions with the Harvest API
	As a developer
	I want to be to call client service calls without knowing how to authenticate.
	I want to be to call client service calls without knowing the endpoint URL's required.


#This is a test records dependency used in setup
Scenario: Get All Clients
	When I call "GetClients()"
	Then the result should contain "/clients"
	
#This is a test records dependency used in setup
@TestClientRecord
Scenario: Create a client
	Then the client should contain clientID

#This is a test records dependency used in tear down
@TestClientRecord
Scenario: Delete a client
	When I call "DeleteClient(clientID)"
	And I call "GetClient(clientID)"
	Then the result should not contain "/clients"

@TestClientRecord	
Scenario: Toggle a client’s state
	When I call "ToggleClientState(clientID)"
	And  I call "GetClient(clientID)"
	Then the result should contain "/client[active='false']"

@TestClientRecord	
Scenario: Toggle a clients state to inactive and back again
	When I call "ToggleClientState(clientID)"
	And  I call "ToggleClientState(clientID)"
	And  I call "GetClient(clientID)"
	Then the result should contain "/client[active='true']"