using System;
using System.Collections.Generic;
using System.Text;

namespace CPUPMicroAssemble
{
    class Microcode
    {
        public string name = "null";
        public string[] data = new string[2048];

        public Microcode()
        {
            for(int i = 0; i < 2048; i++)
            {
                data[i] = "";
            }
        }
        public void Insert(Instruction inst)
        {

        }

        public void Insert(string addr,string[] code)
        {
            int i = 0;
            foreach(var a in addr)
            {
                i = i * 2;
                if(a == '1')
                {
                    i += 1;
                }
            }

            for(int j = 0; j < code.Length; j++)
            {
                data[i + j] = code[j];
            }
        }

        public void Insert(string addr, string code)
        {
            string[] a = { code };
            Insert(addr, a);
        }


    }
}
