"port of SavingsAccount.java"
"author: Konnor Collins"
Account subclass: SavingsAccount [
	"interest on the account"
	|savingsInterest|
	
	SavingsAccount class [
		newNumber: number newCustomer: customer newBalance: balance [
			^super new initNumber: number initCustomer: customer initBalance: balance
		]
	]
	
	initNumber: number initCustomer: customera initBalance: balance [
		super initNumber: number initCustomer: customera initBalance: balance.
		savingsInterest := 0.
	]
	
	accrue: rate [
		 accBalance := accBalance + (accBalance * rate).
		 savingsInterest := savingsInterest + (accBalance * rate).
	]
]