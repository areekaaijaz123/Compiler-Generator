using System;
using Newtonsoft.Json.Linq;

namespace Compiler_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Program_Generator program = new Program_Generator("json", "Search");
            JObject program_attribute = program.Write_Program_Code();
        }
    }
}
