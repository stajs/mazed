using System;
using System.Text;
using static System.Console;

namespace Stajs.Mazed.Core
{
	[Flags]
	public enum Walls
	{
		North,
		East,
		South,
		West
	}

	public class Grid
	{
		public Cell[,] Cells { get; }

		public Grid()
		{
			Cells = new Cell[5, 16];

			for (var i = 0; i < Cells.GetLength(0); i++)
			{
				for (var j = 0; j < Cells.GetLength(1); j++)
				{
					Cells[i, j] = new Cell
					{
						Walls = Walls.North | Walls.East | Walls.South | Walls.West
					};
				}
			}

			//Cells[0, 0].IsEntry = true;
			//Cells[1, 2].IsExit = true;
		}

		public Grid(Cell[,] cells)
		{
			Cells = cells;
		}

		public void Print()
		{
			//for (var i = 0; i < Cells.GetLength(0); i++)
			//{
			//	for (var j = 0; j < Cells.GetLength(1); j++)
			//	{
			//		var oldColor = ForegroundColor;
			//		var cell = Cells[i, j];

			//		if (cell.IsEntry)
			//			ForegroundColor = ConsoleColor.Green;
			//		else if (cell.IsExit)
			//			ForegroundColor = ConsoleColor.Red;
			//		else
			//			ForegroundColor = ConsoleColor.White;

			//		Write($"{i}{j}");
			//		ForegroundColor = oldColor;
			//		Write(":");
			//	}

			//	WriteLine();
			//}

			//WriteLine();

			var height = Cells.GetLength(0);
			var width = Cells.GetLength(1);
			var lastRow = Cells.GetUpperBound(0);
			var lastCol = Cells.GetUpperBound(1);

			for (var row = 0; row < height; row++)
			{
				if (row == 0)
				{
					for (var col = 0; col < width + 1; col++)
					{
						if (col == 0)
							Write("╔═");
						else if (col == width)
							Write("╗");
						else
							Write("╤═");
					}

					WriteLine();
				}

				for (var col = 0; col < width + 1; col++)
				{
					if (col == 0)
						Write("║" + row);
					else if (col == width)
						Write("║");
					else
						Write("│" + row);
				}

				WriteLine();

				if (row != lastRow)
				{
					for (var col = 0; col < width + 1; col++)
					{
						if (col == 0)
							Write("╟─");
						else if (col == width)
							Write("╢");
						else
							Write("┼─");
					}

					WriteLine();
				}
			}

			for (var col = 0; col < width + 1; col++)
			{
				if (col == 0)
					Write("╚═");
				else if (col == width)
					Write("╝");
				else
					Write("╧═");
			}
		}
	}
}