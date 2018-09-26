"port of Account.java into smalltalk"
"author: Konnor Collins"
Object subclass: Account [
	"the accoutn number"
	|accNumber|
	
	"the customer owning the account"
	|customer|
	
	"the current balance of the account"
	|accBalance|

	Account class [
		new [
			^super new
		]
		newNumber: number newCustomer: customer newBalance: balance [
			^self subclassResponsibility
		]
	]
	
	initNumber: number initCustomer: aCustomer initBalance: balance [
		accNumber := number.
		customer := aCustomer.
		accBalance := balance.
	]
	
	accrue: rate [
		^self subclassResponsibility
	]
	
	deposit: amount [
		accBalance := accBalance + amount.
	]
	
	withdraw: amount [
		accBalance := accBalance - amount.
	]
	
	toString [
		"temp string variable for tostring method"
		|s|
		
		s := accNumber , ':' , (customer toString) , ':' , (accBalance asString).
		
		^s
	]

]