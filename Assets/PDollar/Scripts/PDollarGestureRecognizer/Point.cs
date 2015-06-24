using System;

namespace PDollarGestureRecognizer
{
	public class Point
	{
		public float X, Y;
		public int StrokeID;      
		
		public Point(float x, float y, int strokeId)
		{
			this.X = x;
			this.Y = y;
			this.StrokeID = strokeId;
		}
	}
}
