using System;
using System.Collections.Generic;

namespace CPUPMicroAssemble
{
    class Program
    {
        public static Dictionary<string, string> sigdict = new Dictionary<string, string>()
        {
            { "RIN",        "00000000000000000000000001" },
            { "ROUT",       "00000000000000000000000010" },
            { "ADD",        "00000000000000000000000100" },
            { "SUB",        "00000000000000000000001000" },
            { "MUL",        "00000000000000000000010000" },
            { "DIV",        "00000000000000000000100000" },
            { "EROUT",      "00000000000000000001000000" },
            { "ERIN",       "00000000000000000010000000" },
            { "MOD",        "00000000000000000100000000" },
            { "GREATER",    "00000000000000001000000000" },
            { "EQUALS",     "00000000000000010000000000" },
            { "COM",        "00000000000000100000000000" },
            { "ADDR_IN",    "00000000000001000000000000" },
            { "WRITE",      "00000000000010000000000000" },
            { "READ",       "00000000000100000000000000" },
            { "INST_READ",  "00000000001000000000000000" },
            { "PIN",        "00000000010000000000000000" },
            { "POUT",       "00000000100000000000000000" },
            { "BIN",        "00000001000000000000000000" },
            { "AIN",        "00000010000000000000000000" },
            { "BOUT",       "00000100000000000000000000" },
            { "AOUT",       "00001000000000000000000000" },
            { "DONE",       "00010000000000000000000000" },
            { "OUT0",       "00100000000000000000000000" },
            { "INC_AM",     "01000000000000000000000000" },
            { "OUT1",       "10000000000000000000000000" },
            { "NOP",        "00000000000000000000000000" }
        };

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
            {"AMEM","11" }
        };


        static int Main(string[] args)
        {

            if(args.Length <=  1)
            {
                Console.WriteLine("CPUP Microcode Assembler\nusage: CPUPMA.EXE MICRO.MA OUTPUT.MIF");
                return 1;
            }

            String[] input;
            try
            {
                input = System.IO.File.ReadAllLines(args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Read From File");
                return 2;
            }

            Console.WriteLine("Linting"); //Its really just replacing tabs with spaces, nothing fancy
            for(int i = 0; i < input.Length; i++)
            {
                input[i] = input[i].Replace("\t", " ");
            }

            Console.WriteLine("Parsing Functions");

            List<Function> functions = new List<Function>();
            List<Instruction> instructions = new List<Instruction>();

            string funcName = "";
            bool funcDef = false;
            List<String> code = new List<string>();
            for(int i = 0; i < input.Length; i++)
            {
                if (funcDef)
                {
                    if(input[i].Trim().ToLower() == "end")
                    {
                        Function f = new Function(funcName, code.ToArray());
                        functions.Add(f);
                        code.Clear();
                        funcDef = false;
                    }
                    else
                    {
                        if(input[i].Trim() != "")
                        {
                            try
                            {
                                code.Add(Line(input[i]));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                return 3;
                            }
                        }
                    }
                }
                else
                {
                    if(input[i].ToLower().Contains("define ."))
                    {
                        funcName = input[i].Substring(8).Trim();
                        funcDef = true;
                    }
                }
            }


            Console.WriteLine("Parsing Instructions");


            //Instructions
            string instName = "";
            bool instDef = false;
            code = new List<string>();
            string arg1 = "";
            string arg2 = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (instDef)
                {
                    if (input[i].Trim().ToLower() == "end")
                    {
                        Instruction instruction = new Instruction(instName, arg1, arg2, code.ToArray());
                        instructions.Add(instruction);
                        code.Clear();
                        instDef = false;
                        arg1 = "";
                        arg2 = "";
                    }
                    else
                    {
                        if (input[i].Trim() != "") //Skipping whitespace
                        {
                            if (input[i].Contains(".")){//using a function
                                string func = input[i].Trim().Substring(1);
                                int findex = -1;
                                for(int j = 0; j < functions.Count; j++)
                                {
                                    if(functions[j].name == func)
                                    {
                                        findex = j;
                                    }
                                }
                                if (findex == -1)
                                {
                                    Console.WriteLine("Cannot find function by name: {0} at line {1}", func, i + 1);
                                    return 5;
                                }
                                code.AddRange(functions[findex].microcode);
                            }
                            else
                            {
                                try
                                {
                                    code.Add(Line(input[i]));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    return 3;
                                }
                            }

                        }
                    }
                }
                else
                {
                    if (input[i].ToLower().Contains("define $"))
                    {
                        instName = input[i].Substring(8).Trim().Split(' ')[0];
                        if(input[i].Substring(8).Trim().Split(' ').Length >= 2)
                        {

                            if (typedict.ContainsKey(input[i].Substring(8).Trim().Split(' ')[1]))
                            {
                                arg1 = input[i].Substring(8).Trim().Split(' ')[1];
                            }
                            else
                            {
                                Console.WriteLine("Error Parsing Line: {0}\n{1} is not a valid type.", i + 1, input[i].Substring(8).Trim().Split(' ')[1]);
                                return 4;
                            }

                        }
                        if (input[i].Substring(8).Trim().Split(' ').Length == 3)
                        {
                            if(typedict.ContainsKey(input[i].Substring(8).Trim().Split(' ')[2]))
                            {
                                arg2 = input[i].Substring(8).Trim().Split(' ')[2];
                            }
                            else
                            {
                                Console.WriteLine("Error Parsing Line: {0}\n{1} is not a valid type.", i + 1, input[i].Substring(8).Trim().Split(' ')[2]);
                                return 4;
                            }
                        }
                        
                        instDef = true;
                        code = new List<string>();
                    }
                }
            }
            Console.WriteLine("Parsing Complete");
            MIFWriter.Write(instructions, args[1]);
            return 0;
        }

        static string Line(string line)
        {
            string result = sigdict["NOP"];

            foreach(string word in line.Trim().Split(" "))
            {
                if (sigdict.ContainsKey(word))
                {
                    result = Combine(result, sigdict[word]);
                }
                else if(word == "")
                {

                }
                else
                {
                    throw new Exception("Error Parsing Line:\n" + line + "\n Could not parse word:" + word);

                }
            }
            return result;
        }


        static string Combine(string a, string b)
        {
            string result = "";
            for (int i = 0; i < b.Length; i++)
            {
                string A = a.Substring(i, 1);
                string B = b.Substring(i, 1);

                if(B == "1" || A == "1")
                {
                    result = result + "1";
                }
                else
                {
                    result = result + "0";
                }
            }
            return result;
        }
    }
}
