using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueensRiddleSolution
{
    public class QueensChessBoardSecWay
    {
        private const char QueenCharacter = 'Q';
        private List<(int, int)> QueensPositions = new List<(int, int)>();
        private char[,] board;

        public QueensChessBoardSecWay(int queens)
        {
            this.board = new char[queens, queens];
        }

        public void SolveOutput()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

        Queens:
            try
            {
                Enumerable.Range(0, board.GetLength(0)).ToList().ForEach(x => PlaceQueen());
            }
            catch
            {
                Array.Clear(board);
                QueensPositions.Clear();
                goto Queens;
            }

            stopwatch.Stop();

            PrintBoard();

            Console.WriteLine($"Compute time: {stopwatch.ElapsedMilliseconds} ms ({stopwatch.ElapsedMilliseconds/1000d} sec) ({stopwatch.ElapsedTicks} ticks)");
        }

        private void PlaceQueen()
        {
            int currentRow = QueensPositions.Count;
            List<int> skipColumns = new List<int>();
            foreach(var pos in QueensPositions)
            {
                int rowDiff = Math.Abs(pos.Item1 - currentRow);

                skipColumns.Add(pos.Item2);

                if (pos.Item2 - rowDiff is var bannedColumnDR && bannedColumnDR >= 0)
                    skipColumns.Add(bannedColumnDR);

                if (pos.Item2 + rowDiff is var bannedColumnDL && bannedColumnDL >= 0 && bannedColumnDL < board.GetLength(1))
                    skipColumns.Add(bannedColumnDL);
            }

            var availableColumns = Enumerable.Range(0, this.board.GetLength(1)).Where(o => !skipColumns.Contains(o)).ToList();

            int row = currentRow;
            int column = availableColumns.Skip(Random.Shared.Next(availableColumns.Count)).First();

            this.board[row, column] = QueenCharacter;

            QueensPositions.Add((row, column));
        }
        

        private void PrintBoard()
        {
            int visualHeight = 3;
            int visualWidth = 7;
            ConsoleColor currentCubeColor = ConsoleColor.DarkGray;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int height = 0; height < visualHeight; height++)
                {
                    for (int column = 0; column < board.GetLength(1); column++)
                    {
                        for (int width = 0; width < visualWidth; width++)
                        {

                            if (visualWidth / 2 == width && visualHeight / 2 == height)
                            {

                                if (board[row, column] == QueenCharacter)
                                {
                                    if (Console.BackgroundColor == ConsoleColor.Gray)
                                        Console.ForegroundColor = ConsoleColor.Black;
                                    else
                                        Console.ForegroundColor = ConsoleColor.White;

                                    Console.Write(board[row, column]);
                                }
                                else
                                {
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = currentCubeColor;
                                Console.Write(" ");
                            }
                        }
                        currentCubeColor = currentCubeColor == ConsoleColor.DarkGray ? ConsoleColor.Gray : ConsoleColor.DarkGray;

                    }
                    Console.WriteLine();
                }
                currentCubeColor = currentCubeColor == ConsoleColor.DarkGray ? ConsoleColor.Gray : ConsoleColor.DarkGray;
            }
            Console.ResetColor();
        }
    }
}
