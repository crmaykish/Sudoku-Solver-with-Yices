using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    class Square
    {
        public int row;
        public int column;
        public int value;

        public Square(int row, int column, int value)
        {
            this.row = row;
            this.column = column;
            this.value = value;
        }
    }
}
