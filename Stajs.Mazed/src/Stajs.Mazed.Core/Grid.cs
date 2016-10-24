using System;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Console;

namespace Stajs.Mazed.Core
{
	public class Grid
	{
		public Cell[,] Cells { get; }

		public Grid()
		{
			Cells = new Cell[14, 20];

			var height = Cells.GetLength(0);
			var width = Cells.GetLength(1);
			var lastRow = Cells.GetUpperBound(0);
			var lastCol = Cells.GetUpperBound(1);

			for (var row = 0; row < height; row++)
			{
				for (var col = 0; col < width; col++)
				{
					Cells[row, col] = new Cell($"{row}:{col}");
				}
			}

			for (var row = 0; row < height; row++)
			{
				for (var col = 0; col < width; col++)
				{
					var cell = Cells[row, col];

					if (row == 0)
					{
						cell.NorthWall = new Wall(cell, null);
					}
					else
					{
						var northCell = Cells[row - 1, col];
						cell.NorthWall = northCell.SouthWall = new Wall(cell, northCell);
						cell.NorthCell = northCell;
					}

					if (row == lastRow)
					{
						cell.SouthWall = new Wall(cell, null);
					}
					else
					{
						var southCell = Cells[row + 1, col];
						cell.SouthWall = southCell.NorthWall = new Wall(cell, southCell);
						cell.SouthCell = southCell;
					}

					if (col == 0)
					{
						cell.WestWall = new Wall(cell, null);
					}
					else
					{
						var westCell = Cells[row, col - 1];
						cell.WestWall = westCell.EastWall = new Wall(cell, westCell);
						cell.WestCell = westCell;
					}

					if (col == lastCol)
					{
						cell.EastWall = new Wall(cell, null);
					}
					else
					{
						var eastCell = Cells[row, col + 1];
						cell.EastWall = eastCell.WestWall = new Wall(cell, eastCell);
						cell.EastCell = eastCell;
					}
				}
			}
		}

		public Grid(Cell[,] cells)
		{
			Cells = cells;
		}

		public void Carve()
		{
			var seed = 29;
			var random = new Random(seed);

			//Entry;
			var cell = Cells[0, 0];
			cell.RemoveWall(Direction.West);
			cell.HasBeenVisited = true;
			this.Current = cell;
			Print2(ConsoleColor.Red);

			var safety = 0;

			while (cell.AvailableDirections.Any() && safety++ < 100)
			{
				Thread.Sleep(TimeSpan.FromMilliseconds(20));

				var direction = cell.AvailableDirections[random.Next(cell.AvailableDirections.Count)];
				cell.RemoveWall(direction);
				cell = cell.Move(direction);
				cell.HasBeenVisited = true;
				this.Current = cell;

				Print2(ConsoleColor.Red);

				SetCursorPosition(60, 20);
				ForegroundColor = ConsoleColor.Yellow;
				Write($"Iterations: {safety}");
			}

			Print2();

		}

		public Cell Current { get; set; }

		public void Print2(ConsoleColor color = ConsoleColor.Green)
		{
			Clear();

			var height = Cells.GetLength(0);
			var width = Cells.GetLength(1);
			var lastRow = Cells.GetUpperBound(0);
			var lastCol = Cells.GetUpperBound(1);

			for (var row = 0; row < height; row++)
			{
				for (var col = 0; col < width; col++)
				{
					var cell = Cells[row, col];

					var x = col * 2 + 1;
					var y = row * 2 + 1;

					if (cell == Current)
					{
						SetCursorPosition(x, y);
						ForegroundColor = ConsoleColor.White;
						Write("X");
					}

					ForegroundColor = color;

					if (cell.NorthWall != null)
					{
						SetCursorPosition(x, y - 1);
						Write("─");
					}

					if (cell.SouthWall != null)
					{
						SetCursorPosition(x, y + 1);
						Write("─");
					}

					if (cell.EastWall != null)
					{
						SetCursorPosition(x + 1, y);
						Write("│");
					}

					if (cell.WestWall != null)
					{
						SetCursorPosition(x - 1, y);
						Write("│");
					}

					SetCursorPosition(x + 1, y + 1);

					if (cell.SouthCell != null)
					{
						if (col == lastCol)
							Write("┤");
						else
							Write("┼");
					}
					else
					{
						if (row == lastRow && col == lastCol)
							Write("┘");
						else
							Write("┴");
					}

					if (col == 0)
					{
						SetCursorPosition(x - 1, y + 1);
						Write("├");
					}

					if (row == lastRow && col == 0)
					{
						SetCursorPosition(x - 1, y + 1);
						Write("└");
					}

					if (row == 0)
					{
						SetCursorPosition(x + 1, y - 1);
						Write("┬");
					}

					if (row == 0 && col == 0)
					{
						SetCursorPosition(x - 1, y - 1);
						Write("┌");
					}

					if (row == 0 && col == lastCol)
					{
						SetCursorPosition(x + 1, y - 1);
						Write("┐");
					}
				}
			}
		}

		public void Print()
		{
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