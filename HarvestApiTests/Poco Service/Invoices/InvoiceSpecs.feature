Feature: InvoicePoco
	In order to avoid silly mistakes
	As a developer
	I want to be hydrate and dehydrate POCO objects with XML


Scenario: Get All Invoices
	When I call HarvestPocoService.GetInvoices()
	Then the results should be greater than 0

