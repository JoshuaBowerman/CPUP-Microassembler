﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace CPUPMicroAssemble
{
    class MIFWriter
    {
        //This will turn an int into a 12 bit binary string
        public static string IntToAddr(int addr)
        {
            int a = addr;
            string result = "";
            for(int i = 0; i < 12; i++)
            {
                if ((a % 2) == 1)
                {
                    result = "1" + result;
                    a--;
                }
                else
                {
                    result = "0" + result;
                }
                a = a / 2;
            }
            return result;

        }

        public static void Write(List<Instruction> instructions,string outLocation)
        {

            Console.WriteLine("Generating MIF File");
            string file = "--Generated By CPUP Microassembler\n" +
                            "--Generated On:" + DateTime.Today.ToString("dd/MM/yyyy h:mm tt") +"\n";

            file += "WIDTH=26;\n";
            file += "DEPTH=2048;\n\n";
            file += "ADDRESS_RADIX=BIN;\n";
            file += "DATA_RADIX=BIN;\n\n";
            file += "CONTENT BEGIN\n";


            //Sort the instruction list so it's in order by the starting address
            instructions.Sort((x, y) => x.getStartAddrInt().CompareTo(y.getStartAddrInt()));

            int curAddr = 0; //This should always refers to the entry after the one you just wrote. NOT THE ONE YOU JUST WROTE

            for(int i = 0; i < instructions.Count; i++)
            {
                //Handle the memory between the last instruction and this one
                if(instructions[i].getStartAddrInt() - curAddr > 0)
                {
                    //if it's only off by one we dont want to write a range
                    if(instructions[i].getStartAddrInt() - curAddr == 1)
                    {
                        file += "   " + IntToAddr(curAddr) + " : " + Program.sigdict["NOP"] + ";\n";
                    }
                    else
                    {
                        file += "   [" + IntToAddr(curAddr) + ".." + IntToAddr(instructions[i].getStartAddrInt() - 1) + "]  :  " + Program.sigdict["NOP"] + ";\n";
                    }
                    curAddr = instructions[i].getStartAddrInt();
                }
                for(int j = 0; j < instructions[i].code.Length; j++)
                {
                    file += "   " + IntToAddr(curAddr++) + " : " + instructions[i].code[j] + ";\n";
                }
            }
            //Handle the trailing whitespace if any
            if(curAddr != 2047)
            {
                //if it's just one entry dont do a range
                if (2047 - curAddr == 1)
                {
                    file += "   " + IntToAddr(curAddr) + " : " + Program.sigdict["NOP"] + ";\n";
                }
                else
                {
                    file += "   [" + IntToAddr(curAddr) + ".." + IntToAddr(2047) + "]  :  " + Program.sigdict["NOP"] + ";\n";
                }
            }


            //Write the file

            Console.WriteLine("Writing MIF");
            File.WriteAllText(outLocation, file);

            Console.WriteLine("File Written");
          

        }
    }
}
