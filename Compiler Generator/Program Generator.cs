using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Compiler_Generator
{
    class Program_Generator
    {
        String program_object, function_name, return_type;

        JObject program_code;
        JArray parameters, conditions;
        int total_params = -1;
        int total_conditions = -1;

        string write_path = "../../../../../Code.cpp";
        string read_path = "../../../../../";

        public Program_Generator(String object_type = "json", String program_name = "Factorial")
        {
            String file = read_path + program_name + "." + object_type;
            program_object = File.ReadAllText(file);
            read_path = read_path + program_name + "." + object_type;
        }

        public JObject Write_Program_Code()
        {
            program_code = JObject.Parse(program_object);

            function_name = program_code.SelectToken("function").Value<string>();
            return_type = program_code.SelectToken("return-type").Value<string>();

            parameters = (JArray)program_code["parameters"];
            total_params = parameters.Count;

            conditions = (JArray)program_code["conditions"];
            total_conditions = conditions.Count;

            using (StreamWriter file = new StreamWriter(write_path))
            using (StreamReader r = new StreamReader(read_path))
            {
                Console.WriteLine("Writing Code in Cpp");

                file.Write(return_type + " " + function_name + " (");
                if (total_params > -1)
                {
                    for (int i = 0; i < total_params; i++)
                    {
                        if (i == (total_params - 1))
                            file.Write(parameters[i].SelectToken("type").Value<string>() + " " +
                                parameters[i].SelectToken("parameter").Value<string>());
                        else
                            file.Write(parameters[i].SelectToken("type").Value<string>() + " " +
                                parameters[i].SelectToken("parameter").Value<string>() + ", ");
                    }
                    file.Write(") {\n");
                }

                if (total_conditions > -1)
                {
                   for(int i = 0; i < total_conditions; i++)
                    {
                        if (i == 0)
                        {
                            file.Write("if (" + conditions[i].SelectToken("condition").Value<string>() + ") \n");
                            file.Write("{\n return " + conditions[i].SelectToken("action").Value<string>() + ";\n}\n");
                        }
                            
                        else if (i != (total_conditions - 1))
                        {
                            file.Write("else if (" + conditions[i].SelectToken("condition").Value<string>() + ") \n");
                            file.Write("{\n return " + conditions[i].SelectToken("action").Value<string>() + ";\n}\n");
                        }

                        else
                        {
                            file.Write("else return " + conditions[i].SelectToken("condition").Value<string>());
                            file.Write("(" + conditions[i].SelectToken("action").Value<string>() + ");\n");
                        }
                            
                    }
                }

            }
                return program_code;
        }
    }
}
