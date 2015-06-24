using System;

namespace PDollarGestureRecognizer
{
	public class Geometry
	{
		public static float SqrEuclideanDistance(Point a, Point b)
		{
			return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
		}
		public static float EuclideanDistance(Point a, Point b)
		{
			return (float)Math.Sqrt(SqrEuclideanDistance(a, b));
		}
	}
}
