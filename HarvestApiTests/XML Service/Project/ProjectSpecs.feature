Feature: Project Service Requests
	In order to simplify interactions with the Harvest API
	As a developer
	I want to be to call project service calls without knowing how to authenticate.
	I want to be to call project service calls without knowing the endpoint URL's required.


#This is a test records dependency used in setup
Scenario: Get All Projects
	When I call "GetProjects()"
	Then the xml result should contain "/projects"

#This is a test records dependency used in setup
@TestProjectRecord 
Scenario: Create a project
	Then the project should contain "//project[id='[TESTPROJECTID]']"		

#This is a test records dependency used in tear down
@TestProjectRecord
Scenario: Delete a project
	When I call "DeleteProject(projectID)"
	And I call "GetProject(projectID)"
	Then the xml result should not contain "/projects"



#This may fail if your systems clock is wrong
@TestProjectRecord
Scenario: Get All Projects Updated Since ID
	When I call "GetProjects(updatedSinceUTC)" 
	Then the xml result should contain "//project/name[contains(.,'Test')]"
	And  the xml result should be equal
	| Xpath								| 
	| //project/name[contains(.,'Test')] |
	| //project							|

@TestProjectRecord
Scenario: Get All Projects By Client ID
	When I call "GetProjects(clientID)"
	Then the xml result should contain "//project[client-id='[TESTCLIENTID]']"

@TestProjectRecord
Scenario: Get specific project
	When I call "GetProject(projectID)"
	Then the xml result should contain "/project[id='[TESTPROJECTID]']"	

@TestProjectRecord	
Scenario: Update a project
	When I call "UpdateProject(projectID,xml)" 
	| xml |
	| <project><name>Delete Me, Automated Test, Updated</name><client-id>[TESTCLIENTID]</client-id></project>|
	And  I call "GetProject(projectID)"
	Then the xml result should contain "/project[name='Delete Me, Automated Test, Updated']"

@TestProjectRecord	
Scenario: Toggle a projects state
	When I call "ToggleProjectState(projectID)"
	And  I call "GetProject(projectID)"
	Then the xml result should contain "/project[active='false']"

@TestProjectRecord	
Scenario: Toggle a projects state to inactive and back again
	When I call "ToggleProjectState(projectID)"
	And  I call "ToggleProjectState(projectID)"
	And  I call "GetProject(projectID)"
	Then the xml result should contain "/project[active='true']"
