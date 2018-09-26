"port of Customer.java into smalltalk"
"Author: Konnor Collins"
Object subclass: Customer [
	|customerName|
	
	Customer class [
		new: name [
			^super new init: name
		]
	]
	
	init: name [
		customerName := name.
	]
	
	toString [
		^customerName
	]
]