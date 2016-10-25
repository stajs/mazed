using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Stajs.Mazed.Core
{
	public class Cell
	{
		public int Column { get; private set; }
		public int Row { get; private set; }
		public string Id => $"{Row}:{Column}";
		public Cell NorthCell { get; set; }
		public Cell EastCell { get; set; }
		public Cell SouthCell { get; set; }
		public Cell WestCell { get; set; }

		public Wall NorthWall { get; set; }
		public Wall EastWall { get; set; }
		public Wall SouthWall { get; set; }
		public Wall WestWall { get; set; }

		public List<Direction> AvailableDirections => GetAvailableDirections();

		private List<Direction> GetAvailableDirections()
		{
			var directions = new List<Direction>();

			if (NorthCell != null && !NorthCell.HasBeenVisited)
				directions.Add(Direction.North);

			if (EastCell != null && !EastCell.HasBeenVisited)
				directions.Add(Direction.East);

			if (SouthCell != null && !SouthCell.HasBeenVisited)
				directions.Add(Direction.South);

			if (WestCell != null && !WestCell.HasBeenVisited)
				directions.Add(Direction.West);

			return directions;
		}

		public bool HasBeenVisited { get; set; }

		public Cell(int column, int row)
		{
			Column = column;
			Row = row;
		}

		public void RemoveWall(Direction direction)
		{
			switch (direction)
			{
				case Direction.North:
					NorthWall = null;
					if (NorthCell != null) NorthCell.SouthWall = null;
					return;

				case Direction.East:
					EastWall = null;
					if (EastCell != null) EastCell.WestWall = null;
					break;

				case Direction.South:
					SouthWall = null;
					if (SouthCell != null) SouthCell.NorthWall = null;
					break;

				case Direction.West:
					WestWall = null;
					if (WestCell != null) WestCell.EastWall = null;
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}

		public Cell Move(Direction direction)
		{
			switch (direction)
			{
				case Direction.North:
					return NorthCell;

				case Direction.East:
					return EastCell;

				case Direction.South:
					return SouthCell;

				case Direction.West:
					return WestCell;

				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.Append($"Cell: {Id}");

			if (NorthCell != null)
				sb.Append(" North");

			if (EastCell != null)
				sb.Append(" East");

			if (SouthCell != null)
				sb.Append(" South");

			if (WestCell != null)
				sb.Append(" West");

			return sb.ToString();
		}
	}
}