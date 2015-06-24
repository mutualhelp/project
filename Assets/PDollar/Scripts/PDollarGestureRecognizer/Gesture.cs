using System;

namespace PDollarGestureRecognizer
{
	public class Gesture
	{
		public Point[] Points = null;         
		public string Name = "";                
		private const int SAMPLING_RESOLUTION = 32;
		
		
		public Gesture(Point[] points, string gestureName = "")
		{
			this.Name = gestureName;
			
			this.Points = Scale(points);
			this.Points = TranslateTo(Points, Centroid(Points));
			this.Points = Resample(Points, SAMPLING_RESOLUTION);
		}
		
		#region gesture pre-processing steps: scale normalization, translation to origin, and resampling
		
		
		private Point[] Scale(Point[] points)
		{
			float minx = float.MaxValue, miny = float.MaxValue, maxx = float.MinValue, maxy = float.MinValue;
			for (int i = 0; i < points.Length; i++)
			{
				if (minx > points[i].X) minx = points[i].X;
				if (miny > points[i].Y) miny = points[i].Y;
				if (maxx < points[i].X) maxx = points[i].X;
				if (maxy < points[i].Y) maxy = points[i].Y;
			}
			
			Point[] newPoints = new Point[points.Length];
			float scale = Math.Max(maxx - minx, maxy - miny);
			for (int i = 0; i < points.Length; i++)
				newPoints[i] = new Point((points[i].X - minx) / scale, (points[i].Y - miny) / scale, points[i].StrokeID);
			return newPoints;
		}
		
		private Point[] TranslateTo(Point[] points, Point p)
		{
			Point[] newPoints = new Point[points.Length];
			for (int i = 0; i < points.Length; i++)
				newPoints[i] = new Point(points[i].X - p.X, points[i].Y - p.Y, points[i].StrokeID);
			return newPoints;
		}
		private Point Centroid(Point[] points)
		{
			float cx = 0, cy = 0;
			for (int i = 0; i < points.Length; i++)
			{
				cx += points[i].X;
				cy += points[i].Y;
			}
			return new Point(cx / points.Length, cy / points.Length, 0);
		}
		public Point[] Resample(Point[] points, int n)
		{
			Point[] newPoints = new Point[n];
			newPoints[0] = new Point(points[0].X, points[0].Y, points[0].StrokeID);
			int numPoints = 1;
			
			float I = PathLength(points) / (n - 1); 
			float D = 0;
			for (int i = 1; i < points.Length; i++)
			{
				if (points[i].StrokeID == points[i - 1].StrokeID)
				{
					float d = Geometry.EuclideanDistance(points[i - 1], points[i]);  //두점 사이의 거리
					if (D + d >= I)
					{
						Point firstPoint = points[i - 1];
						while (D + d >= I)
						{
							float t = Math.Min(Math.Max((I - D) / d, 0.0f), 1.0f);
							if (float.IsNaN(t)) t = 0.5f;
							newPoints[numPoints++] = new Point(
								(1.0f - t) * firstPoint.X + t * points[i].X,
								(1.0f - t) * firstPoint.Y + t * points[i].Y,
								points[i].StrokeID
								);
							
							d = D + d - I;
							D = 0;
							firstPoint = newPoints[numPoints - 1];
						}
						D = d;
					}
					else D += d;
				}
			}
			
			if (numPoints == n - 1) 
				newPoints[numPoints++] = new Point(points[points.Length - 1].X, points[points.Length - 1].Y, points[points.Length - 1].StrokeID);
			return newPoints;
		}
		private float PathLength(Point[] points)
		{
			float length = 0;
			for (int i = 1; i < points.Length; i++)
				if (points[i].StrokeID == points[i - 1].StrokeID)
					length += Geometry.EuclideanDistance(points[i - 1], points[i]);
			return length;
		}
		
		#endregion
	}
}