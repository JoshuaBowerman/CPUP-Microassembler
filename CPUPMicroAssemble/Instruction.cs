using System;
using System.Collections.Generic;
using System.Text;

namespace CPUPMicroAssemble
{
    class Instruction
    {
        public string name;
        public string arg1;
        public string arg2;
        public string[] code;

        public static Dictionary<string, string> instdict = new Dictionary<string, string>()
        {
            {"NOP", "0000" },
            {"MOV", "0001" },
            {"JMP", "0010" },
            {"JE",  "0011" },
            {"JL",  "0100" },
            {"JG",  "0101" },
            {"JLE", "0110" },
            {"JGE", "0111" },
            {"ADD", "1000" },
            {"SUB", "1001" },
            {"MUL", "1010" },
            {"DIV", "1011" },
            {"COM", "1100" },
            {"JER", "1101" },
            {"MOD", "1110" }
        };
        public static Dictionary<string, string> typedict = new Dictionary<string, string>()
        {
            {"REG","00" },
            {"MEM","10" },
            {"AINT","01" },
            {"AMEM","11" },
            {"","00" }
        };


        public Instruction(string title, string type1, string type2, string[] micro)
        {
            name = title;
            arg1 = type1;
            arg2 = type2;
            code = micro;
        }

        public string getInstructionCode()
        {
            return instdict[name];
        }

        public string getStartAddr()
        {
            return "0" + instdict[name] + typedict[arg1][0] + typedict[arg2][0] + (((typedict[arg1][1] == '1') || (typedict[arg2][1] == '1')) ? "1" : "0") + "0000";
        }

        public int getStartAddrInt()
        {
            int v = 0;
            for(int i = 0; i < getStartAddr().Length; i++)
            {
                v = v * 2;
                if (getStartAddr()[i] == '1')
                {
                    v++;
                }
            }
            return v;
        }
    }
}
