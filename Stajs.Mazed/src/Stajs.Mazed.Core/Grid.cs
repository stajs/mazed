using System.Text;

namespace Stajs.Mazed.Core
{
	public class Grid
	{
		public Cell[,] Cells { get; }

		public Grid()
		{
			Cells = new [,]
			{
				{ new Cell { IsEntry = true }, new Cell(), new Cell(), new Cell() },
				{ new Cell(), new Cell(), new Cell(), new Cell() },
				{ new Cell(), new Cell(), new Cell(), new Cell { IsExit = true } }
			};
		}

		public Grid(Cell[,] cells)
		{
			Cells = cells;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			for (var i = 0; i < Cells.GetLength(0); i++)
			{
				for (var j = 0; j < Cells.GetLength(1); j++)
				{
					sb.Append($"{i}{j}:");
				}

				sb.AppendLine();
			}

			sb.AppendLine();

			for (var i = 0; i < Cells.GetLength(0); i++)
			{
				for (var j = 0; j < Cells.GetLength(1); j++)
				{
					if (i == 0)
					{
						if (j == 0)
						{
							sb.Append('╔');
							continue;
						}

						if (j + 1 == Cells.GetLength(1))
						{
							sb.Append('╗');
							continue;
						}

						sb.Append('═');
						continue;
					}

					if (i + 1 == Cells.GetLength(0))
					{
						if (j == 0)
						{
							sb.Append('╚');
							continue;
						}

						if (j + 1 == Cells.GetLength(1))
						{
							sb.Append('╝');
							continue;
						}

						sb.Append('═');
						continue;
					}

					if (j == 0 || j + 1 == Cells.GetLength(1))
					{
						sb.Append('║');
						continue;
					}

					sb.Append(' ');
				}

				sb.AppendLine();
			}

			return sb.ToString();
		}
	}
}