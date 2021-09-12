using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8P
{
    public class Board : IComparable<Board>
    {
        public int[,] Matrix { get; set; }

        public Board(int[,] matrix)
        {
            this.Matrix = matrix;
        }

        public int Manhattan()
        {
            var manhattanDistance = 0;

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    var num = Matrix[i, j];

                    switch (num)
                    {
                        case 1: manhattanDistance += Math.Abs(0 - i) + Math.Abs(0 - j); break;
                        case 2: manhattanDistance += Math.Abs(0 - i) + Math.Abs(1 - j); break;
                        case 3: manhattanDistance += Math.Abs(0 - i) + Math.Abs(2 - j); break;
                        case 4: manhattanDistance += Math.Abs(1 - i) + Math.Abs(0 - j); break;
                        case 5: manhattanDistance += Math.Abs(1 - i) + Math.Abs(1 - j); break;
                        case 6: manhattanDistance += Math.Abs(1 - i) + Math.Abs(2 - j); break;
                        case 7: manhattanDistance += Math.Abs(2 - i) + Math.Abs(0 - j); break;
                        case 8: manhattanDistance += Math.Abs(2 - i) + Math.Abs(1 - j); break;

                        default:
                            break;
                    }
                }
            }

            return manhattanDistance;
        }

        public Tuple<int, int> GetZeroIndex()
        {
            int width = this.Matrix.GetLength(0);
            int height = this.Matrix.GetLength(1);

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    if (this.Matrix[x, y].Equals(0))
                    {
                        return Tuple.Create(x, y);
                    }
                }
            }

            return Tuple.Create(-1, -1);
        }

        public bool IsEqual(Board otherBoard)
        {
            for (int i = 0; i < this.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.Matrix.GetLength(1); j++)
                {
                    if (this.Matrix[i, j] != otherBoard.Matrix[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public List<Board> Neighbors()
        {
            var childrens = new List<Board>();
            var (zeroX, zeroY) = GetZeroIndex();



            var rightBoard = MoveZeroToTheRight(zeroX, zeroY);
            if (rightBoard != null)
            {
                childrens.Add(rightBoard);
            }

            var leftBoard = MoveZeroToTheLeft(zeroX, zeroY);
            if (leftBoard != null)
            {
                childrens.Add(leftBoard);
            }

            var downBoard = MoveZeroDown(zeroX, zeroY);
            if (downBoard != null)
            {
                childrens.Add(downBoard);
            }

            var upBoard = MoveZeroUp(zeroX, zeroY);
            if (upBoard != null)
            {
                childrens.Add(upBoard);
            }

            return childrens;
        }

        private Board MoveZeroToTheLeft(int x, int y)
        {
            if (PointIsOutside(x, y-1))
            {
                return null;
            }

            var clonedBoard = CloneMe();

            var temp = clonedBoard.Matrix[x, y-1];
            clonedBoard.Matrix[x, y-1] = 0;
            clonedBoard.Matrix[x, y] = temp;

            return clonedBoard;
        }

        private Board MoveZeroToTheRight(int x, int y)
        {
            if (PointIsOutside(x, y+1))
            {
                return null;
            }

            var clonedBoard = CloneMe();

            var temp = clonedBoard.Matrix[x, y+1];
            clonedBoard.Matrix[x, y+1] = 0;
            clonedBoard.Matrix[x, y] = temp;

            return clonedBoard;
        }

        private Board MoveZeroUp(int x, int y)
        {
            if (PointIsOutside(x-1, y))
            {
                return null;
            }

            var clonedBoard = CloneMe();

            var temp = clonedBoard.Matrix[x-1, y];
            clonedBoard.Matrix[x-1, y] = 0;
            clonedBoard.Matrix[x, y] = temp;

            return clonedBoard;
        }

        private Board MoveZeroDown(int x, int y)
        {
            if (PointIsOutside(x+1, y))
            {
                return null;
            }

            var clonedBoard = CloneMe();

            var temp = clonedBoard.Matrix[x+1, y];
            clonedBoard.Matrix[x+1, y] = 0;
            clonedBoard.Matrix[x, y] = temp;

            return clonedBoard;
        }

        private Board CloneMe()
        {
            var newMatrix = new int[3, 3];

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = Matrix[i, j];
                }
            }

            return new Board(newMatrix);
        }

        private bool PointIsOutside(int x, int y)
        {
            if (x < 0 || y < 0 || x > Matrix.GetLength(0) - 1 || y > Matrix.GetLength(1) - 1)
            {
                return true;
            }

            return false;
        }

        public string TooString()
        {
            var str = new StringBuilder();

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                var innerStr = new StringBuilder();
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    innerStr.Append(Matrix[i, j] + " ");
                }

                str.Append(innerStr.ToString().Trim());
                str.Append("\n");
            }

            return str.ToString();
        }

        public int CompareTo(Board other)
        {
            // Heuristic value
            var thisValue = this.Manhattan();
            var otherValue = other.Manhattan();

            if (thisValue < otherValue)
            {
                return 1;
            }
            else if (thisValue > otherValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
