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
        BackgroundWorker bgWorker;

        TextBox[,] textBoxes;

        public SudokuForm()
        {
            InitializeComponent();

            bgWorker = new BackgroundWorker();
            //bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;

            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgressChanged);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_Done);

            textBoxes = new TextBox[9, 9];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Sudoku Solver";

            setUpSquares();
        }

        private void updateBoard(int r, int c, int val)
        {
            if (val == -1)
            {
                textBoxes[r, c].Clear();
            }
            else
            {
                textBoxes[r, c].Text = val.ToString();
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // update the count box
            if (e.ProgressPercentage == 100)
            {
                countDown.Text = ((int)e.UserState).ToString();
            }
            else
            {
                Point p = (Point)e.UserState;
                updateBoard(p.X, p.Y, e.ProgressPercentage);
            }
        }

        private void bgWorker_Done(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Thread done!");
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e){

            List<object> param = (List<object>)e.Argument;

            bool demo = (bool)param[1];

            int[,] seed = new int[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    seed[i, j] = -1;
                }
            }

            Random rand = new Random();
            for (int i = 1; i <= 9; i++)
            {
                int r = rand.Next(0, 8);
                int c = rand.Next(0, 8);

                //textBoxes[r, c].Text = i + "";  //bleh
                seed[r, c] = i;

            }

            Solver solver = new Solver(seed);
            int[,] solution = solver.solve();

            int[,] puzzle = new int[9, 9];
            Array.Copy(solution, puzzle, solution.Length);

            if (demo)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        //textBoxes[i, j].Text = puzzle[i, j] + "";
                        //updateBoard(i, j, puzzle[i, j]);
                        bgWorker.ReportProgress(puzzle[i, j], new Point(i, j));
                    }
                }
            }

            //this.Refresh();

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
            //int limit = slider.Value;
            //int limit = 50;
            int limit = 81 - (int)param[0];
            Console.WriteLine("Starting algorithm with limit: " + limit);
            while (n < limit)
            {
                

                // Clear out current square
                puzzle[node.r, node.c] = -1;

                // Increment n every time we set something to -1
                n++;

                uniqueCheck.setInput(puzzle);

                // If it not uniquely solvable, back up
                if (uniqueCheck.isUnique(solution))
                {
                    //textBoxes[node.r, node.c].Text = "";
                    if (demo)
                    {
                        bgWorker.ReportProgress(-1, new Point(node.r, node.c));
                    }
                    //this.Refresh();
                }
                else
                {
                    // Decrement n every time we undo an operation
                    puzzle[node.r, node.c] = node.lastValue;
                    //textBoxes[node.r, node.c].Text = node.lastValue + "";
                    if (demo)
                    {
                        bgWorker.ReportProgress(node.lastValue, new Point(node.r, node.c));
                    }
                    //this.Refresh();
                    n--;
                    node = node.getParent();
                }



                // Make sure we haven't reached the top
                if (node == null)
                    break;

                // Randomly pick the next node
                // If the current node has no more children, back up

                TreeNode nextNode = node.getNextNode();
                while (nextNode == null && node != null)
                {
                    puzzle[node.r, node.c] = node.lastValue;
                    //textBoxes[node.r, node.c].Text = node.lastValue + "";
                    if (demo)
                    {
                        bgWorker.ReportProgress(node.lastValue, new Point(node.r, node.c));
                    }
                    //this.Refresh();
                    n--;
                    node = node.getParent();
                    nextNode = node.getNextNode();
                }

                // Make sure we haven't reached the top
                if (node == null)
                    break;

                node = nextNode;

                // return the current count;
                bgWorker.ReportProgress(100, 81 - n);
            }


            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puzzle[i, j] != -1)
                        //textBoxes[i, j].Text = puzzle[i, j] + "";
                        bgWorker.ReportProgress(puzzle[i, j], new Point(i, j));
                    else
                    {
                        //textBoxes[i, j].Text = "";
                        bgWorker.ReportProgress(-1, new Point(i, j));
                    }
                }
            }
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
            countDown.Clear();
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
                    textBoxes[i, j].Left += i * 52 + 5;
                    textBoxes[i, j].Top += j * 48 + 5;

                    this.Controls.Add(textBoxes[i, j]);
                }
            }

            //draw in seperating lines
            Label hLine1 = new Label();
            hLine1.AutoSize = false;
            hLine1.BorderStyle = BorderStyle.FixedSingle;
            hLine1.Height = 2;
            hLine1.Width = 464;
            hLine1.Left = 6;
            hLine1.Top = 146;

            Label hLine2 = new Label();
            hLine2.AutoSize = false;
            hLine2.BorderStyle = BorderStyle.FixedSingle;
            hLine2.Height = 2;
            hLine2.Width = 464;
            hLine2.Left = 6;
            hLine2.Top = 290;

            Label vLine1 = new Label();
            vLine1.AutoSize = false;
            vLine1.BorderStyle = BorderStyle.FixedSingle;
            vLine1.Height = 428;
            vLine1.Width = 2;
            vLine1.Left = 314;
            vLine1.Top = 6;

            Label vLine2 = new Label();
            vLine2.AutoSize = false;
            vLine2.BorderStyle = BorderStyle.FixedSingle;
            vLine2.Height = 428;
            vLine2.Width = 2;
            vLine2.Left = 158;
            vLine2.Top = 6;

            this.Controls.Add(hLine1);
            this.Controls.Add(hLine2);
            this.Controls.Add(vLine1);
            this.Controls.Add(vLine2);

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
            clearBoxes();
            List<object> paramList = new List<object>();
            paramList.Add(slider.Value);
            paramList.Add(demoCheckBox.Checked);
            bgWorker.RunWorkerAsync(paramList);

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void demoCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void countDown_TextChanged(object sender, EventArgs e)
        {

        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            sliderLabel.Text = slider.Value.ToString();
        }

        private void starting_Click(object sender, EventArgs e)
        {

        }

        private void slider_Scroll(object sender, EventArgs e)
        {

        }
    }
}
