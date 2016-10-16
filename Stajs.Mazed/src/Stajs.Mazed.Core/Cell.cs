namespace Stajs.Mazed.Core
{
	public class Cell
	{
		public bool IsVisible { get; set; } = true;
		public bool IsEntry { get; set; }
		public bool IsExit { get; set; }
		public bool HasBeenVisited { get; set; }
	}
}