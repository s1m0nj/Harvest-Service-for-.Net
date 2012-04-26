Feature: ProjectPoco
	In order to avoid silly mistakes
	As a developer
	I want to be hydrate and dehydrate POCO objects with XML

@TestProjectRecord
Scenario: Get All Projects
	When I call HarvestPocoService.GetProjects()
	Then the results should be greater than 0

