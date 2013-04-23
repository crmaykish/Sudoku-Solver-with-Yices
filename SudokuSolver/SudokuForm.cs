using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SudokuSolver
{
    public partial class SudokuForm : Form
    {
        TextBox[,] textBoxes;

        public SudokuForm()
        {
            InitializeComponent();

            textBoxes = new TextBox[9, 9];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Sudoku Solver";

            setUpSquares();
        }

        // Submit button - run the Solver and write the results to the text boxes
        private void button1_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver(grabUserInput());
            int[,] results = solver.solve();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBoxes[i, j].Text = results[i, j].ToString();
                }
            }
        }

        // Clear all the text boxes
        private void button2_Click(object sender, EventArgs e)
        {
            clearBoxes();
        }

        private void clearBoxes(){
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBoxes[i, j].Clear();
                }
            }
        }

        // initialize and position the text boxes on the form
        private void setUpSquares()
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBoxes[i, j] = new TextBox();
                    textBoxes[i, j].Font = new Font(textBoxes[i, j].Font.FontFamily, 24);
                    textBoxes[i, j].MaxLength = 1;
                    textBoxes[i, j].Width = 48;
                    textBoxes[i, j].Height = 48;
                    textBoxes[i, j].Left += i * 48 + 5;
                    textBoxes[i, j].Top += j * 48 + 5;

                    this.Controls.Add(textBoxes[i, j]);
                }
            }

            //draw in seperating lines
            Label hLine1 = new Label();
            hLine1.AutoSize = false;
            hLine1.BorderStyle = BorderStyle.Fixed3D;
            hLine1.Height = 2;
            hLine1.Width = 450;
            hLine1.Left = 12;
            hLine1.Top = 146;

            Label hLine2 = new Label();
            hLine2.AutoSize = false;
            hLine2.BorderStyle = BorderStyle.Fixed3D;
            hLine2.Height = 2;
            hLine2.Width = 450;
            hLine2.Left = 12;
            hLine2.Top = 300;

            this.Controls.Add(hLine1);
            this.Controls.Add(hLine2);

        }

        // process the user input from the text boxes
        private int[,] grabUserInput()
        {
            int[,] input = new int[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    String value = textBoxes[i, j].Text;
                    input[i, j] = (value != "") ? Int32.Parse(value) : -1;
                }
            }
            return input;
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            int[,] seed = new int[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    seed[i, j] = -1;
                }
            }

            clearBoxes();

            Random rand = new Random();
            for (int i = 1; i <= 9; i++)
            {
                int r = rand.Next(0, 8);
                int c = rand.Next(0, 8);

                textBoxes[r, c].Text = i + "";  //bleh
                seed[r, c] = i;
                
            }

            Solver solver = new Solver(seed);
            int[,] solution = solver.solve();

            int[,] puzzle = new int[9, 9];
            Array.Copy(solution, puzzle, solution.Length);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puzzle[i, j] != -1)
                        textBoxes[i, j].Text = puzzle[i, j] + "";
                    else
                        textBoxes[i, j].Text = "";
                }
            }

            this.Refresh();

            // remove random values...
            //puzzle[0, 0] = -1;

            Solver uniqueCheck = new Solver();
           // uniqueCheck.

            List<TreeNode> children = new List<TreeNode>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    children.Add(new TreeNode(i, j, puzzle[i, j]));
                }
            }

            TreeNode node = children[rand.Next(0, children.Count - 1)];
            children.Remove(node);
            node.setChildren(children);

            int n = 0;
            while(n < 60){

                if (puzzle[node.r, node.c] != -1)
                {

                    puzzle[node.r, node.c] = -1;
                    uniqueCheck.setInput(puzzle);
                    n++;
                    if (!uniqueCheck.isUnique(solution))
                    {
                        n--;
                        puzzle[node.r, node.c] = node.lastValue;
                        node = node.getParent();
                    }
                }

                // Get the next node, perhaps moving up the tree
                TreeNode nextNode = node.getNextNode();
                while (nextNode == null)
                {
                    Console.WriteLine("Backtracking");
                    n--;
                    puzzle[node.r, node.c] = node.lastValue;
                    node = node.getParent();
                    if (node == null) {
                        Console.WriteLine("No solutions");
                        break;
                    }
                    nextNode = node.getNextNode();
                }

                node = nextNode;

                Console.WriteLine(n);
            }

             

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puzzle[i, j] != -1)
                        textBoxes[i, j].Text = puzzle[i, j] + "";
                    else
                        textBoxes[i, j].Text = "";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
