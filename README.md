# BankBer Application

## Welcome to BankBer

Hello, and welcome to BankBer, from the same company that brought you GrillBer!

BankBer allows users to select their name, select a specific account that they own, and then see all the transactions associated with that account! It's just like any bank app, but so much better!

We need you, the developer, to implement the following feature requirements to make BankBer even better!

## BankBer Requirements

1. Transactions should have a description **_Top Priority!_ Do this first!**
    - Examples:
        - A paycheck description might say "MORTGAGE RESEAR PAYROLL"
        - A gas bill might say "Speedpay AmerenMO" 
    - Any transaction with a blank or null description should just say "No description"
    - New transactions should have an input box for description
1. When inserting a transaction, the user picked date should be the one used
    - The back end doesn't appear to support this for some reason
1. When displaying the list of transactions, the amounts should have the following formating
    - All amounts should have a dollar sign in front of them
    - All amounts should show two decimal places
    - Debit amounts should be red and have a negative sign in front of the dollar sign
1. After adding a new transaction:
    - The input row should slide back up and hide itself
    - The new transaction should be retrieved from the back-end and displayed in the list
1. Transactions should appear in reverse date order (i.e. the last transaction should be the first transaction)
    - This could be done in either the front-end or the back-end
    - This includes new transactions (i.e. if the user adds a transaction with a date in the middle of the current list of transactions, it should appear in the correct spot without reloading the page)
1. Users need a way to back out so they can:
    - Select a different account to look at
    - Select a different user to look at

## Notes

- We are not concerned with security or authentication. We believe people can be trusted, and don't need to log in
- The back end is not hosted anywhere. You'll see that all the back-end references use localhost. To use this app, you'll need to start the BankBer.BackEnd project.
- The database file is expected to exist at "`C:\BankBer\BankBer.db`". If one doesn't exist there, an empty one will be created there. Feel free to put the sample database file in that location to use sample data!
