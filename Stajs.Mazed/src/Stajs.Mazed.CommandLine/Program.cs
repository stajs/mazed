using System;
using System.Text;
using Stajs.Mazed.Core;

namespace Stajs.Mazed.CommandLine
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var grid = new Grid();

			Console.OutputEncoding = Encoding.UTF8;
			grid.Carve();
			grid.Print2();
			Console.ReadKey();
		}
	}
}