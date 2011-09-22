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
	Then the client should contain "//client[id='[TESTCLIENTID]']"

#This is a test records dependency used in tear down
@TestClientRecord
Scenario: Delete a client
	When I call "DeleteClient(clientID)"
	And I call "GetClient(clientID)"
	Then the result should not contain "/clients"

@TestClientRecord
Scenario: Get All Clients Updated Since ID
	When I call "GetClients(updatedSinceUTC)" 
	Then the result should contain "//client/name[contains(.,'Test')]"
	Then  the result should be equal
	| Xpath								| 
	| //client/name[contains(.,'Test')] |
	| //client							|

@TestClientRecord
Scenario: Get specific client
	When I call "GetClient(clientID)"
	Then the result should contain "/client[id='[TESTCLIENTID]']"
	
@TestClientRecord	
Scenario: Update a client
	When I call "UpdateClient(clientID,xml)" 
	| xml |
	| <client><name>Delete Me, Automated Test, Updated</name></client>|
	And  I call "GetClient(clientID)"
	Then the result should contain "/client[name='Delete Me, Automated Test, Updated']"



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