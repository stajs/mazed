using System;
using System.Linq;
using System.Collections.Generic;

namespace Stajs.Mazed.Core
{
	public class Cell
	{
		private static List<Directions> _allDirections = Enum.GetValues(typeof(Directions)).Cast<Directions>().ToList();

		public Directions AvailableDirections { get; set; }
		public IEnumerable<int> AvailableDirectionsValues => _allDirections.Where(value => AvailableDirections.HasFlag(value)).Select(value => (int) value);
		public bool IsVisible { get; set; } = true;
		public bool IsEntry { get; set; }
		public bool IsExit { get; set; }
		public bool HasBeenVisited { get; set; }

		public void RemoveDirection(Directions direction)
		{
			AvailableDirections = AvailableDirections &= ~direction;
		}
	}
}