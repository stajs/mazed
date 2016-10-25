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
					Cells[row, col] = new Cell(col, row);
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
			var seed = 297237;
			seed = 6954;
			var random = new Random(seed);


			var lastRow = Cells.GetUpperBound(0);
			var lastCol = Cells.GetUpperBound(1);

			//Entry;
			var cell = Cells[0, 0];
			cell.RemoveWall(Direction.West);
			cell.HasBeenVisited = true;
			Current = cell;
			Print();

			PrintWallRemoval(cell, Direction.West);

			var safety = 0;

			while (cell.AvailableDirections.Any() && safety++ < 233)
			{
				Thread.Sleep(TimeSpan.FromMilliseconds(30));

				if (cell.Row == lastRow && cell.Column == lastCol)
				{
					cell.RemoveWall(Direction.East);
					PrintWallRemoval(cell, Direction.East);
				}

				var direction = cell.AvailableDirections[random.Next(cell.AvailableDirections.Count)];
				cell.RemoveWall(direction);
				PrintWallRemoval(cell, direction);
				cell = cell.Move(direction);
				cell.HasBeenVisited = true;
				Current = cell;

				SetCursorPosition(60, 20);
				ForegroundColor = ConsoleColor.Yellow;
				Write($"Iterations: {safety}");
			}
		}

		public Cell Current { get; set; }

		public void PrintWallRemoval(Cell cell, Direction direction)
		{
			var x = cell.Column * 2 + 1;
			var y = cell.Row * 2 + 1;

			var lastRow = Cells.GetUpperBound(0);
			var lastCol = Cells.GetUpperBound(1);

			SetCursorPosition(x, y);
			ForegroundColor = ConsoleColor.Gray;
			//Write(" ");

			switch (direction)
			{
				case Direction.North:
					Write("↑");
					y--;
					break;

				case Direction.East:
					Write("→");
					x++;
					break;

				case Direction.South:
					Write("↓");
					y++;
					break;

				case Direction.West:
					Write("←");
					x--;
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}

			ForegroundColor = ConsoleColor.Red;

			SetCursorPosition(x, y);
			//Write(direction.ToString().First());
			Write(" ");

			var hasNorthWall = cell.NorthWall != null;
			var hasEastWall = cell.EastWall != null;
			var hasSouthWall = cell.SouthWall != null;
			var hasWestWall = cell.WestWall != null;

			var hasNorthEastWall = cell.NorthCell?.EastWall != null;
			var hasEastNorthWall = cell.EastCell?.NorthWall != null;

			var hasNorthWestWall = cell.NorthCell?.WestWall != null;
			var hasWestNorthWall = cell.WestCell?.NorthWall != null;

			var hasSouthWestWall = cell.SouthCell?.WestWall != null;
			var hasWestSouthWall = cell.WestCell?.SouthWall != null;

			var hasSouthEastWall = cell.SouthCell?.EastWall != null;
			var hasEastSouthWall = cell.EastCell?.SouthWall != null;

			if (direction == Direction.North)
			{
				SetCursorPosition(x + 1, y);

				if (cell.Column == lastCol)
					Write("│");
				else if (hasEastWall && hasNorthEastWall && hasEastNorthWall)
					Write("├");
				else if (!hasEastWall && hasNorthEastWall && hasEastNorthWall)
					Write("└");
				else if (hasEastWall && !hasNorthEastWall && hasEastNorthWall)
					Write("│");
				else
					Write("╵");

				SetCursorPosition(x - 1, y);

				if (cell.Column == 0)
					Write("│");
				else if (hasWestWall && hasNorthWestWall && hasWestNorthWall)
					Write("┤");
				else if (!hasWestWall && hasNorthWestWall && hasWestNorthWall)
					Write("┘");
				else if (hasWestWall && !hasNorthWestWall && hasWestNorthWall)
					Write("│");
				else
					Write("╵");
			}

			if (direction == Direction.South)
			{
				SetCursorPosition(x + 1, y);

				if (cell.Column == lastCol)
					Write("│");
				else if (hasEastWall && hasSouthEastWall && hasEastSouthWall)
					Write("├");
				else if (!hasEastWall && hasSouthEastWall && hasEastSouthWall)
					Write("┌");
				else if (hasEastWall && !hasSouthEastWall && hasEastSouthWall)
					Write("│");
				else
					Write("╷");

				SetCursorPosition(x - 1, y);

				if (cell.Column == 0)
					Write("│");
				else if (hasWestWall && hasSouthWestWall && hasWestSouthWall)
					Write("┤");
				else if (!hasWestWall && hasSouthWestWall && hasWestSouthWall)
					Write("┐");
				else if (hasWestWall && !hasSouthWestWall && hasWestSouthWall)
					Write("│");
				else
					Write("╷");
			}

			if (direction == Direction.East)
			{
				SetCursorPosition(x, y - 1);

				if (hasNorthWall && hasNorthEastWall && hasEastNorthWall)
					Write("┴");
				else if (!hasNorthWall && hasNorthEastWall && hasEastNorthWall)
					Write("└");
				else if (hasNorthWall && !hasNorthEastWall && hasEastNorthWall)
					Write("─");
				else
					Write("╶");

				SetCursorPosition(x, y + 1);

				if (cell.Row == lastRow && cell.Column == lastCol)
					Write("─");
				else if (hasSouthWall && hasSouthEastWall && hasEastSouthWall)
					Write("┬");
				else if (!hasSouthWall && hasSouthEastWall && hasEastSouthWall)
					Write("┌");
				else if (hasSouthWall && !hasSouthEastWall && hasEastSouthWall)
					Write("─");
				else
					Write("╶");
			}

			if (direction == Direction.West)
			{
				SetCursorPosition(x, y - 1);

				if (cell.Row == 0 && cell.Column == 0)
					Write("─");
				else if (hasNorthWall && hasNorthWestWall && hasWestNorthWall)
					Write("┴");
				else if (!hasNorthWall && hasNorthWestWall && hasWestNorthWall)
					Write("┘");
				else if (hasNorthWall && !hasNorthWestWall && hasWestNorthWall)
					Write("─");
				else
					Write("╴");

				SetCursorPosition(x, y + 1);

				if (hasSouthWall && hasSouthWestWall && hasWestSouthWall)
					Write("┬");
				else if (!hasSouthWall && hasSouthWestWall && hasWestSouthWall)
					Write("┐");
				else if (hasSouthWall && !hasSouthWestWall && hasWestSouthWall)
					Write("─");
				else
					Write("╴");
			}
		}

		public void Print(ConsoleColor color = ConsoleColor.Red)
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
	}
}