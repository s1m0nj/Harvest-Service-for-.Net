Feature: Harvest API Throttle Limit
	Copied from the Harvest API documentation[http://www.getharvest.com/api#api-throttle-limit] (as of 2011/09/12):
		We have an API throttle that blocks accounts emitting more than 100 calls per 15 seconds. We reserve the right to tune the limitations, but they are always set high enough to allow a well-behaving interactive program to do its job.
		For batch processes and API developers who still need to perfect their code, this throttle may be an inadvertent blocker. Just wait and make no API calls (the throttle is reset with each call). The throttle will lift itself in few minutes and API calls may resume.
		When the rate limit is exceeded Harvest will send an HTTP 503 status code. The number of seconds until the throttle is lifted is sent via the "Retry-After" HTTP header, as specified in RFC 2616. You can use GET /account/rate_limit_status to programmatically query your current throttle status.
	In order to avoid my code form dealing with the API throttle limit
	As a developer
	I want to make my service request and expect the service wrapper to deal with the throttle.

# The following test assumes that it can execute 100 requests in less than 15 seconds, if your harware or internet connection are too slow this test may fail.
@TestClientRecord
Scenario: Exceed the throttle limit
	When I call "ToggleClientState(clientID)" 202 times
	Then an exception should not be received
	And a wait for the throttle to clear should of happened
