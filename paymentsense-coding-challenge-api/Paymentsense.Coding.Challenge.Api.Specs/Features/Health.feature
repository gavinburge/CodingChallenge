Feature: Health
	Test behaviour of health endpoint

Scenario: Health check returns OK
	Given i call the helth check endpoint
	Then i should get back a 200 status
	And a healthy response is returned
