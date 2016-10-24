namespace Stajs.Mazed.Core
{
	public class Wall
	{
		public Cell Cell1 { get; set; }
		public Cell Cell2 { get; set; }

		public Wall(Cell cell1, Cell cell2)
		{
			Cell1 = cell1;
			Cell2 = cell2;
		}

		public override string ToString()
		{
			return $"Wall {Cell1}|{Cell2}";
		}
	}
}