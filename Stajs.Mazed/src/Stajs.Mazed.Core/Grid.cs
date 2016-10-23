using System;
using System.Text;
using static System.Console;

namespace Stajs.Mazed.Core
{
	public class Grid
	{
		public Cell[,] Cells { get; }

		public Grid()
		{
			Cells = new Cell[3, 3];

			var height = Cells.GetLength(0);
			var width = Cells.GetLength(1);
			var lastRow = Cells.GetUpperBound(0);
			var lastCol = Cells.GetUpperBound(1);

			for (var row = 0; row < height; row++)
			{
				for (var col = 0; col < width; col++)
				{
					var cell = new Cell
					{
						AvailableDirections = Directions.North | Directions.East | Directions.South | Directions.West
					};

					if (row == 0)
						cell.RemoveDirection(Directions.North);

					if (col == 0)
						cell.RemoveDirection(Directions.West);

					if (col == lastCol)
						cell.RemoveDirection(Directions.East);

					if (row == lastRow)
						cell.RemoveDirection(Directions.South);

					Cells[row, col] = cell;
				}
			}
		}

		public Grid(Cell[,] cells)
		{
			Cells = cells;
		}

		public void Carve()
		{
			var currentCell = Cells[0, 0];
			currentCell.RemoveDirection(Directions.East);

			var seed = 12;
			var random = new Random(seed);


		}

		private void Remove(Directions wall)
		{
			
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