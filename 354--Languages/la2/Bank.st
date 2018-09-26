"smalltalk port of Bank.java"
"author: Konnor Collins"
Object subclass: Bank [

	"collection (set) of all accounts in the bank"
	|accounts|
	
	Bank class [
		new [
			^super new init
		]
	]
	
	init [
		accounts := Set new.
	]
	
	"add a new account to the bank"
	addAccount: account [
		accounts add: account.
	]
	
	"iterate through all accounts and accrue interest if applicable"
	accrueAccounts: rate [
		accounts do: [:acc | acc accrue: rate].
	]
	
	"iterate through all listed accounts and return their information"
	toString [
		|s|
		s := (Character nl asString).
		accounts do: [:acc | s := s , (acc toString) , (Character nl asString)].
		^s
	]

]

"global space 'main method' "
|bank| bank := Bank new.
|cust| cust := Customer new: 'Ann'.
|acc1| acc1 := CheckingAccount newNumber: '01001' newCustomer: cust newBalance: 100.00.
|acc2| acc2 := SavingsAccount newNumber: '01002' newCustomer: cust newBalance: 200.00.
bank addAccount: acc1.
bank addAccount: acc2.
bank accrueAccounts: 0.02.
(bank toString) printNl.