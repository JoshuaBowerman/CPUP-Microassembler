# CPUP Microassembler

The CPUP Microassembler turns a microassembly file into a MIF file used to initialize the microcode rom.
The microassembler supports functions and types. 

## Functions
All function names must start with a `.`. A normal function might look like this

    define .name
	    NOP
	    NOP
	end

## Data Types
there are 4 data types supported by this microassembler.
Keyword | Meaning
------- | -------
REG | This denotes a standard register
MEM | A memory location contains in a register
AINT | A number stored in the word following the instruction.
AMEM | An address stored in the word following the instruction.

## Supported Signals
The following list is all the supported signals. for details on what each signal does see [Execution Module](https://github.com/JoshuaBowerman/CPUP/blob/main/execution.md)
### Signals
- RIN
- ROUT
- ADD
- SUB
- MUL
- DIV
- EROUT
- ERIN
- MOD
- GREATER
- EQUALS
- COM
- ADDR_IN
- WRITE
- READ
- INST_READ
- PIN
- POUT
- BIN
- AIN
- BOUT
- AOUT
- DONE
- OUT0
- OUT1
- INC_AM
- NOP

## Defining an instruction
When defining an instruction you start with `$` in the same way you start with `.` for functions.

An intstruction opening line takes on the following format

`define $Instruction Type1 Type2`

Instruction being whichever instruction you are implementing, Type1 and Type2 being data types.
Note: Type1 and Type2 are optional

**Example:** COM where the data is in a register.

`define $COM REG`

After the opening line you would begin defining your instruction using Signals
each line will be one microinstruction. each signal is seperated by a space.

**Example:**

`RIN READ AIN'

You can use functions simply by writing their name, note that they must be on thier own line.

**Example:**

`.next`

Once done you end the instruction with the `end` keyword on it's own line.

**Example:**
All together an instruction may look like this.

    define $JMP MEM
	    AOUT PIN ADDR_IN
	    NOP
	    INST_READ DONE
	end

