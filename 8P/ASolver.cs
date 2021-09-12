using System.Collections.Generic;
using C5;

namespace _8P
{
    public class ASolver
    {
        public Board GoalState { get; set; }

        public List<Board> Boards { get; set; }
        public bool _isSolvable { get; set; }
        public System.Collections.Generic.HashSet<Board> _visited { get; set; }
        public ASolver(Board initialBoard)
        {
            _isSolvable = Solvable(initialBoard.Matrix);

            if (!_isSolvable)
            {
                return;
            }

            Boards = new List<Board>();
            _visited = new System.Collections.Generic.HashSet<Board>();
            this.GoalState = new Board(new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0, } });

            var queue = new IntervalHeap<Board>();

            queue.Add(initialBoard);

            while (queue.Count > 0)
            {
                var board = queue.DeleteMax();
                Boards.Add(board);
                if (board.IsEqual(this.GoalState))
                {
                    Boards.Add(board);
                    break;
                }

                var children = board.Neighbors();

                for (var i = children.Count - 1; i >= 0; i--)
                {
                    var currentChild = children[i];
                    if (!_visited.Contains(children[i]))
                    {
                        queue.Add(currentChild);
                        _visited.Add(children[i]);
                    }
                }
            }
        }

        public IEnumerable<Board> Solution()
        {
            return Boards;
        }

        public int GetInvCount(int[,] arr)
        {
            int inv_count = 0;
            for (int i = 0; i < 3 - 1; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    // Value 0 is used for empty space
                    if (arr[j, i] > arr[i, j])
                        inv_count++;
                }
                    
            }
           
            return inv_count;
        }

        public bool Solvable(int[,] puzzle)
        {
            // Count inversions in given 8 puzzle
            int invCount = GetInvCount(puzzle);

            return true;
            // return true if inversion count is even.
            return (invCount % 2 == 0);
        }

        public bool IsSolvable()
        {
            return _isSolvable;
        }
    }
}