using System;
using System.Collections.Generic;

namespace _8P
{
    public class Solver
    {
        public int MovesCount { get; set; }
        public List<Board> Boards { get; set; }
        public Board GoalState { get; set; }
        public HashSet<Board> _visited { get; set; }

        public Solver(Board initialBoard)
        {
            _visited = new System.Collections.Generic.HashSet<Board>();
            this.GoalState = new Board(new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0, } });

            Boards = new List<Board>() {initialBoard};

            Solve(initialBoard);
        }

        public void Solve(Board board)
        {
            if (board.IsEqual(GoalState))
            {
                return;
            }

            _visited.Add(board);

            var children = board.Neighbors();

            var minIndex = -1;
            var min = int.MaxValue;

            for (int i = 0; i < children.Count; i++)
            {
                if (!_visited.Contains(children[i]))
                {
                    var value = children[i].Manhattan();

                    if (value < min)
                    {
                        minIndex = i;
                        min = value;
                    }
                }
            }

            MovesCount++;
            Boards.Add(children[minIndex]);

            Solve(children[minIndex]);
        }

        public int Moves()
        {
            return MovesCount;
        }

        public IEnumerable<Board> Solution()
        {
            return Boards;
        }
    }
}