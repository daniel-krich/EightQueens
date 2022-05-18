using EightQueensRiddleSolution;
using System.Collections;
using System.Diagnostics;

#region Display single solution

QueensChessBoard board = new QueensChessBoard(8);
board.SolveOutput(true);

#endregion

#region Find all possible solutions

Stopwatch stopwatch = new Stopwatch();

HashSet<string> hashset = new HashSet<string>();

stopwatch.Start();

while (hashset.Count < 92)
{
    QueensChessBoard boardAll = new QueensChessBoard(8);
    hashset.Add(boardAll.SolveOutput(false));
}

stopwatch.Stop();

Console.WriteLine($"Compute time for all solutions ({hashset.Count}): {stopwatch.ElapsedMilliseconds} ms ({stopwatch.ElapsedMilliseconds / 1000d} sec) ({stopwatch.ElapsedTicks} ticks)");

#endregion