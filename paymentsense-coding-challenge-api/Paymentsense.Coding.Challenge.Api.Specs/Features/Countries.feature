﻿Feature: Countries
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