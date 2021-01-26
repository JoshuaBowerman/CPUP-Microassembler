using System;
using System.Collections.Generic;
using System.Text;

namespace CPUPMicroAssemble
{
    class Function
    {

        public string[] microcode;
        public string name;

        public Function(string Title, string[] code)
        {
            name = Title;
            microcode = code;
        }
    }
}
