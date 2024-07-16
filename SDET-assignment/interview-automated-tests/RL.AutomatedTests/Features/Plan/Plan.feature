Feature: Plan feature
    Test creating and adding procedures to a plan

    # example
    Scenario: Create Plan
        Given I'm on the start page
        When I click on start
        Then I'm on the plan page

# Expected test
Scenario: Test Adding user to a plan procedure
     Given Select 2 procedures on the plan
     When Select a user from the user list
     Then Refresh the page and Validate Users