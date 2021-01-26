using System;
using System.Collections.Generic;
using System.Text;

namespace CPUPMicroAssemble
{
    class Instruction
    {
        string name;
        string arg1;
        string arg2;
        string[] code;

        public Instruction(string title, string type1, string type2, string[] micro)
        {
            name = title;
            arg1 = type1;
            arg2 = type2;
            code = micro;
        }
    }
}
