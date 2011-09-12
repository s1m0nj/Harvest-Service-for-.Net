Feature: Client Service Requests
	In order to simplify interactions with the Harvest API
	As a developer
	I want to be to call client service calls without knowing how to authenticate.
	I want to be to call client service calls without knowing the endpoint urls required.


#This is a test records dependecy used in setup
Scenario: Get All Clients
	When I call "GetClients()"
	Then the result should contain "/clients"
	
#This is a test records dependecy used in setup
@TestClientRecord
Scenario: Create a client
	Then the client should contain clientID

#This is a test records dependecy used in ter down
@TestClientRecord
Scenario: Delete a client
	When I call "DeleteClient(clientID)"
	And I call "GetClient(clientID)"
	Then the result should not contain "/clients"