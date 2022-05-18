using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EightQueensRiddleSolution
{
    public class QueensChessBoard
    {
        private const char QueenCharacter = 'Q';
        private const char DangerousSpotCharacter = '*';

        private int queens;
        private char[,] board;

        public QueensChessBoard(int queens)
        {
            this.queens = queens;
            this.board = new char[queens, queens];
        }

        public string SolveOutput(bool displayTable)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (CountQueens() < this.queens)
            {
                Array.Clear(board);
                Enumerable.Range(0, this.queens).ToList().ForEach(x => PlaceQueen());
            }

            stopwatch.Stop();

            if (displayTable)
            {
                PrintBoard();
                Console.WriteLine($"Compute time: {stopwatch.ElapsedMilliseconds} ms ({stopwatch.ElapsedMilliseconds / 1000d} sec) ({stopwatch.ElapsedTicks} ticks)");
            }

            return GetTableString();
        }

        private void PlaceQueen()
        {
            (int, int)? result = GetRandomFreePosition();
            if (result.HasValue)
            {
                board[result.Value.Item1, result.Value.Item2] = QueenCharacter;
                FillDangerousPositionsFromRowColumn(result.Value.Item1, result.Value.Item2);
            }
        }

        private int CountQueens()
        {
            int queens = 0;
            foreach (char chr in board)
                if (chr == QueenCharacter)
                    queens++;
            return queens;
        }

        private (int, int)? GetRandomFreePosition()
        {
            
            List<(int, int)> allPositions = new List<(int, int)>();
            for (int r = 0; r < board.GetLength(0); r++)
            {
                for (int c = 0; c < board.GetLength(1); c++)
                {
                    if (board[r, c] == DangerousSpotCharacter || board[r, c] == QueenCharacter)
                        continue;
                    allPositions.Add((r, c));
                }
            }

            if (allPositions.Count <= 0)
                return default;

            return allPositions[Random.Shared.Next(allPositions.Count)];
        }

        private void FillDangerousPositionsFromRowColumn(int row, int column)
        {
            
            for (int i = 0; i < board.GetLength(1); i++)
            {
                if (i != column)
                    board[row, i] = DangerousSpotCharacter;
            }

            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (i != row)
                    board[i, column] = DangerousSpotCharacter;
            }

            var leftTop = column > 0 ? (row - column >= 0 ? (row - column, 0) : (0, Math.Abs(row - column))) : (row, column);
            var RightTop = column < board.GetLength(1) - 1 ? (row + column < board.GetLength(0) ? (0, row + column) : (Math.Max(0, row + column - board.GetLength(0) + 1), board.GetLength(1) - 1)) : (row, column);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                if(leftTop.Item1 < board.GetLength(0) && leftTop.Item2 < board.GetLength(1))
                {
                    if(leftTop.Item1 != row && leftTop.Item2 != column)
                        board[leftTop.Item1, leftTop.Item2] = DangerousSpotCharacter;
                    leftTop.Item1++;
                    leftTop.Item2++;
                }

                if (RightTop.Item1 < board.GetLength(0) && RightTop.Item2 < board.GetLength(1) && RightTop.Item2 >= 0)
                {
                    if (RightTop.Item1 != row && RightTop.Item2 != column)
                        board[RightTop.Item1, RightTop.Item2] = DangerousSpotCharacter;
                    RightTop.Item1++;
                    RightTop.Item2--;
                }
            }
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

        private string GetTableString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(char chr in board)
                sb.Append(chr);
            return sb.ToString();
        }
    }
}
