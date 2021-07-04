Feature: Countries
	Tests to ensure the behaviour of the country endpoints work correctly

Scenario: Get all countries
	Given a request to get all countries
	Then i should get back a 200 status
	And i should get back 250 countries

Scenario: When data from external service cannot be found error is returned
	Given the external data cannot be found 
	When a request to get all countries
	Then i should get back a 500 status

Scenario: Multiple requests made but one call made to external service
	Given 3 requests received to get all countries
	Then i should get back 3 200 status
	And the external service should be called once

Scenario: Get page 1 of countries
	Given a request to get page 1 of countries with a page size of 10
	Then i should get back a 200 status
	And i should get back 10 countries
	And the first country name should be 'Afghanistan'
	And the first flag should be 'https://restcountries.eu/data/afg.svg'
	And the last country name should be 'Antigua and Barbuda'
	And the last flag should be 'https://restcountries.eu/data/atg.svg'
	And the total items should be 250

Scenario: Get page 25 of countries
	Given a request to get page 25 of countries with a page size of 10
	Then i should get back a 200 status
	And i should get back 10 countries
	And the first country name should be 'Vanuatu'
	And the last country name should be 'Zimbabwe'
	And the total items should be 250

#issue identified with caching lock where the semiphoreslim lock is not released on exception
Scenario: When an error is returned subsequent call should be handled
	Given the external data cannot be found 
	When a request to get all countries
	Then i should get back a 500 status
	When a subsequent request is made for existing data
	Then i should get back a 200 status
	And i should get back 250 countries

Scenario: Bad request returned when invalid characters are used in country detail query
	Given a request to get country details with invalid characters 
	Then i should get back a 400 status

Scenario: When a 5xx error is returned from external service then it should be retried 2 times
	Given external call returns a 500 status code 
	When a request to get all countries
	Then i should get back a 500 status
	And 3 attempts to call the service should have been made