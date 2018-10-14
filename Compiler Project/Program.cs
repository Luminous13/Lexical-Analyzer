using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

namespace Compiler_Project
{
    class Program
    {
        static string input;
        static List<string> inputList = new List<string>();

        static MatchCollection matches;

        //=================================
        static List<string> identifier = new List<string>();
        static List<string> operators = new List<string>();
        static List<string> separators = new List<string>();
        static List<string> keywords = new List<string>();
        static List<string> literal = new List<string>();
        //=================================

        static void Main(string[] args)
        {
            bool ans = true;

            do
            {
                Write("Input: ");

                input = ReadLine().Trim();

                if (input.Contains("//"))
                    WriteLine($"Comment: {input.Replace("//", "")}");
                else
                {
                    string _keyword = @"\bif\b|\bwhile\b|\breturn\b|\bdo\b|\belse\b|\bthen\b";
                    string _separator = @"[(,),{,},;]";
                    string _operators = @"==|>=|<=|=|\+|\-|\*|/|<|>";
                    string _literal = @"^\d+$|^\d+(\d+)*(\.\d+(e\d+)?)?$|\btrue\b|\bfalse\b";


                    ProcessInput(operators, _operators);
                    ProcessInput(separators, _separator);

                    // Store the remaining values in a list
                    inputList = input.Trim().Split(' ').ToList();

                    // Take out the keywords and replace with spaces
                    foreach (string word in inputList)
                    {
                        if (Regex.IsMatch(word, _keyword))
                            keywords.Add(word);
                        else if (Regex.IsMatch(word, _literal) || word.Contains('"'))
                            literal.Add(word);
                        else
                            if (word != "") identifier.Add(word);
                    }

                }

                WriteLine();
                WriteLine("Tokens");
                WriteLine("=======================");

                PrintOutput(keywords, "Keywords");
                PrintOutput(separators, "Separators");
                PrintOutput(operators, "Operators");
                PrintOutput(identifier, "Identifiers");
                PrintOutput(literal, "Literals");

                Write("\n\nAgain? (Y or N): ");
                if (ReadKey().Key == ConsoleKey.N)
                    ans = false;

                WriteLine("\n");
                ClearAll();
            } while (ans);
        }

        static void ProcessInput(List<string> list, string _lookFor)
        {
            matches = Regex.Matches(input, _lookFor);
            foreach (Match match in matches)
            {
                list.Add(match.ToString());
                input = input.Replace(match.ToString(), " ");
            }
        }

        static void PrintOutput(List<string> list, string name)
        {
            if (list.Count != 0)
            {
                WriteLine();
                Write($"{name}: ");

                for (int i = 0; i < list.Count; i++)
                {
                    if (i == list.Count - 1)
                        Write(list[i]);
                    else
                        Write($"{list[i]}, ");
                }
            }
        }

        static void ClearAll()
        {
            input = "";
            inputList.Clear();

            identifier.Clear();
            operators.Clear();
            separators.Clear();
            keywords.Clear();
            literal.Clear();
        }
    }
}
