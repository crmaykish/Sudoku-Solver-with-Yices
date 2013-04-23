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
                    textBoxes[i, j].MaxLength = 1;
                    textBoxes[i, j].Width = 30;
                    textBoxes[i, j].Height = 30;
                    textBoxes[i, j].Left += i * 30 + 5;
                    textBoxes[i, j].Top += j * 30 + 5;

                    this.Controls.Add(textBoxes[i, j]);
                }
            }
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
    }
}
