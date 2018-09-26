"port of CheckingAccount"
"author: Konnor Collins"
Account subclass: CheckingAccount [

	CheckingAccount class [
		newNumber: number newCustomer: customer newBalance: balance [
			^super new initNumber: number initCustomer: customer initBalance: balance
		]
	]
	
	initNumber: number initCustomer: customera initBalance: balance [
		super initNumber: number initCustomer: customera initBalance: balance
	]
	
	accrue: rate [
		"this space left intentionally blank"
	]
]