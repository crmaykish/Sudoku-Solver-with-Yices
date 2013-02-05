using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace SudokuSolver
{
    class Solver
    {
        private List<String> lines;
        private int[,] userInput;

        public Solver(int[,] userInput)
        {
            lines = new List<String>();
            this.userInput = userInput;
        }

        public int[,] solve()
        {
            // Generate the yices input file
            generateDefines();
            generateAsserts();

            // Write the input to a local text file
            writeYicesInputFile();

            // Run yices
            string[] yicesOutput = runYices();

            // Parse the solution
            return parseYicesOutput(yicesOutput);
        }

        // Grab the square values from the yices output file
        private int[,] parseYicesOutput(string[] output)
        {
            int[,] results = new int[9, 9];

            foreach (string s in output)
            {
                if (s.Contains("true"))
                {
                    int r = Int32.Parse(s.Substring(4, 1));
                    int c = Int32.Parse(s.Substring(5, 1));
                    int v = Int32.Parse(s.Substring(6, 1)) + 1;

                    results[r, c] = v;
                }
            }

            return results;
        }

        // setup and execute the yices command, capture the output
        private string[] runYices()
        {
            // Run the yices process and capture the result
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "yices.exe";
            start.Arguments = "-e yices_input.yc";
            start.CreateNoWindow = true;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            Process process = Process.Start(start);
            StreamReader reader = process.StandardOutput;
            string result = reader.ReadToEnd();

            // each string is a true output line from yices
            return result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }

        private void writeYicesInputFile()
        {
            System.IO.File.WriteAllLines("yices_input.yc", lines);
        }

        // generate the define statements for the yices input file
        private void generateDefines()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        lines.Add("(define b" + i + "" + j + "" + k + ":: bool)");
                    }
                }
            }
        }

        // generate the assert statements for the Sudoku logic
        private void generateAsserts()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        setUpRowRestrictions(i, j, k);
                        setUpColRestrictions(i, j, k);
                        setUpSquareRestrictions(i, j, k);
                    }
                    setUpEmptySquareRestrictions(i, j);
                }
            }

            setUpUserInputRestrictions();

            lines.Add("(check)");
        }

        // Each value only once per row
        private void setUpRowRestrictions(int i, int j, int k)
        {
            String row = "(assert (=> b" + i + j + k + " (not (or ";

            for (int a = 0; a < 9; a++)
            {
                if (a != i)
                {
                    row += "b" + a + j + k + " ";
                }

            }
            row += "))))";
            lines.Add(row);
        }

        // Each value only once per row
        private void setUpColRestrictions(int i, int j, int k)
        {
            String col = "(assert (=> b" + i + j + k + " (not (or ";

            for (int b = 0; b < 9; b++)
            {
                if (b != j)
                {
                    col += "b" + i + b + k + " ";
                }
            }
            col += "))))";
            lines.Add(col);
        }

        // Each value only once per square
        private void setUpSquareRestrictions(int i, int j, int k)
        {
            String sq = "(assert (=> b" + i + j + k + " (not (or ";

            int limi = i < 3 ? 0 : 3 * (i / 3);
            int limj = j < 3 ? 0 : 3 * (j / 3);

            for (int c = limi; c < limi + 3; c++)
            {
                for (int d = limj; d < limj + 3; d++)
                {
                    if (i != c || j != d)
                    {
                        sq += "b" + c + d + k + " ";
                    }

                }
            }
            sq += "))))";
            lines.Add(sq);
        }

        // every square must have a value
        private void setUpEmptySquareRestrictions(int i, int j)
        {
            String or = "(assert (or b" + i + j + "0 ";
            for (int r = 1; r < 9; r++)
            {
                or += "b" + i + j + r + " ";
            }
            or += "))";
            lines.Add(or);
        }

        // if the user enters any values, create restrictions for those
        private void setUpUserInputRestrictions()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (userInput[i, j] != -1)
                    {
                        int value = userInput[i,j] - 1;
                        lines.Add("(assert b" + i + j + value + ")");
                    }
                }
            }
        }
    }
}
