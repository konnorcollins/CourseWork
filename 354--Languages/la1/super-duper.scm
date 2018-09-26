(define (super-duper source count)
	
	; actual duper
	(define (super-duper source count)
		
		(if (null? source)
			; list terminated
			'()
			
			; list has an element
			
			(if (list? (car source))
				; element is a list
				(cons (make-list count (super-duper (car source) count)) (super-duper (cdr source) count))
				; element is an atom
				(cons (make-list count (car source)) (super-duper (cdr source) count))
			)
		)
	)


	; check for atoms just in case
	(if (list? source)
		; is a list
		(super-duper source count)
		; is an atom
		source
	)
)