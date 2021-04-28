
define .next
	POUT RIN
	INC_AM ADD
	ROUT ADDR_IN PIN
	NOP
	INST_READ DONE
end 
define .jmp_aint
	POUT RIN
	OUT1 ADD
	ROUT ADDR_IN
	NOP
	READ ADDR_IN PIN
	NOP
	INST_READ DONE
end
define .jmp_mem
	AOUT ADDR_IN
	NOP
	READ ADDR_IN PIN
	NOP
	INST_READ DONE
	NOP
	NOP
end
define .jmp_amem
	POUT RIN
	OUT1 ADD
	ROUT ADDR_IN
	NOP
	READ ADDR_IN PIN
	NOP
	INST_READ DONE
end
define .jmp_reg
	AOUT ADDR_IN PIN
	NOP
	INST_READ DONE
	NOP
	NOP
	NOP
	NOP
end

define $NOP
	NOP
	.next
end

define $MOV REG REG
	AOUT BIN
	.next
end

define $MOV REG MEM
	BOUT ADDR_IN
	AOUT WRITE
	.next
end
define $MOV REG AMEM
	POUT RIN
	OUT1 ADD
	ROUT ADDR_IN
	NOP
	READ ADDR_IN
	WRITE AOUT
	.next
end

define $MOV AINT REG
	POUT RIN
	OUT1 ADD
	ROUT ADDR_IN
	NOP
	READ BIN
	.next
end
define $MOV MEM REG
	AOUT ADDR_IN
	NOP
	READ BIN
	.next

end
define $MOV AMEM REG
	POUT RIN
	OUT1 ADD
	ROUT ADDR_IN
	NOP
	READ ADDR_IN
	NOP
	READ BIN
	.next
end

define $JMP MEM
	.jmp_mem
end

define $JMP AMEM
	.jmp_amem
end

define $JMP REG
	.jmp_reg
end
define $JMP AINT
	.jmp_aint
end

define $JL MEM
	EROUT GREATER EQUALS
	.jmp_mem
	.next
end
define $JL AMEM
	EROUT GREATER EQUALS
	.jmp_amem
	.next
end
define $JLE	MEM
	EROUT GREATER
	.jmp_mem
	.next
end
define $JLE	AMEM
	EROUT GREATER
	.jmp_amem
	.next
end
define $JE MEM
	EROUT EQUALS
	.next
	.jmp_mem
end
define $JE AMEM
	EROUT EQUALS
	.next
	.jmp_amem
end
define $JGE MEM
	EROUT EQUALS GREATER
	.next
	.jmp_mem
end
define $JGE AMEM
	EROUT EQUALS GREATER
	.next
	.jmp_amem
end
define $JG MEM
	EROUT GREATER
	.next
	.jmp_mem
end
define $JG AMEM
	EROUT GREATER
	.next
	.jmp_amem
end
define $JL REG
	EROUT GREATER EQUALS
	.jmp_reg
	.next
end
define $JLE	REG
	EROUT GREATER
	.jmp_reg
	.next
end
define $JE REG
	EROUT EQUALS
	.next
	.jmp_reg
end
define $JGE REG
	EROUT EQUALS GREATER
	.next
	.jmp_reg
end
define $JL AINT
	EROUT GREATER EQUALS
	.jmp_aint
	.next
end
define $JLE	AINT
	EROUT GREATER
	.jmp_aint
	.next
end
define $JE AINT
	EROUT EQUALS
	.next
	.jmp_aint
end
define $JGE AINT
	EROUT EQUALS GREATER
	.next
	.jmp_aint
end

define $ADD REG REG
	OUT0 ERIN
	AOUT RIN
	ADD BOUT
	ROUT AIN
	.next
end
define $ADD REG MEM
	OUT0 ERIN
	BOUT ADDR_IN
	AOUT RIN
	ADD READ
	AIN ROUT
	.next
end
define $ADD REG AINT
	OUT0 ERIN
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	ADD READ
	ROUT AIN
	.next
end
define $ADD REG AMEM
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	READ ADDR_IN
	OUT0 ERIN
	READ ADD
	ROUT AIN
	.next
end
define $SUB REG REG
	OUT0 ERIN
	AOUT RIN
	SUB BOUT
	ROUT AIN
	.next
end
define $SUB REG MEM
	BOUT ADDR_IN
	AOUT RIN
	OUT0 ERIN
	SUB READ
	AIN ROUT
	.next
end
define $SUB REG AINT
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	OUT0 ERIN
	SUB READ
	ROUT AIN
	.next
end
define $SUB REG AMEM
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	READ ADDR_IN
	OUT0 ERIN
	READ SUB
	ROUT AIN
	.next
end
define $MUL REG REG
	OUT0 ERIN
	AOUT RIN
	MUL BOUT
	ROUT AIN
	.next
end
define $MUL REG MEM
	OUT0 ERIN
	BOUT ADDR_IN
	AOUT RIN
	MUL READ
	AIN ROUT
	.next
end
define $MUL REG AINT
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	OUT0 ERIN
	MUL READ
	ROUT AIN
	.next
end
define $MUL REG AMEM
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	READ ADDR_IN
	OUT0 ERIN
	READ MUL
	ROUT AIN
	.next
end
define $DIV REG REG
	OUT0 ERIN
	AOUT RIN
	DIV BOUT
	ROUT AIN
	.next
end
define $DIV REG MEM
	OUT0 ERIN
	BOUT ADDR_IN
	AOUT RIN
	DIV READ
	AIN ROUT
	.next
end
define $DIV REG AINT
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	OUT0 ERIN
	DIV READ
	ROUT AIN
	.next
end
define $DIV REG AMEM
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	READ ADDR_IN
	OUT0 ERIN
	READ DIV
	ROUT AIN
	.next
end

define $MOD REG REG
	OUT0 ERIN
	AOUT RIN
	MOD BOUT
	ROUT AIN
	.next
end
define $MOD REG MEM
	OUT0 ERIN
	BOUT ADDR_IN
	AOUT RIN
	MOD READ
	AIN ROUT
	.next
end
define $MOD REG AINT
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	OUT0 ERIN
	MOD READ
	ROUT AIN
	.next
end

define $MOD REG AMEM
	POUT RIN
	ADD OUT1
	ROUT ADDR_IN
	RIN AOUT
	READ ADDR_IN
	OUT0 ERIN
	READ MOD
	ROUT AIN
	.next
end

define $COM REG
	COM AOUT
	.next
end

define $COM AINT
	POUT RIN
	OUT1 ADD
	ROUT ADDR_IN
	NOP
	READ COM
	.next
end

define $COM MEM
	AOUT ADDR_IN
	NOP
	READ COM
	.next
end

define $COM AMEM
	RIN POUT
	OUT1 ADD
	ROUT ADDR_IN
	NOP
	ADDR_IN READ
	NOP
	READ COM
	.next
end

	



