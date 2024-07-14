using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DollarOneRecognizer
{
    public struct Point
    {
        public float X, Y;
        public Point(float x, float y) { X = x; Y = y; }
    }

    public struct Template
    {
        public string Name;
        public List<Point> Points;
        public Template(string name, List<Point> points)
        {
            Name = name;
            Points = points;
        }
    }

    private List<Template> templates;
    private const int NumPoints = 64;
    private const float SquareSize = 250.0f;

    public DollarOneRecognizer()
    {
        templates = new List<Template>();
    }

    public void AddTemplate(string name, List<Point> points)
    {
        templates.Add(new Template(name, Resample(points, NumPoints)));
    }

    public string Recognize(List<Point> points)
    {
        List<Point> candidate = Resample(points, NumPoints);
        candidate = RotateToZero(candidate);
        candidate = ScaleToSquare(candidate, SquareSize);
        candidate = TranslateToOrigin(candidate);

        float minDistance = float.MaxValue;
        string bestMatch = "";

        foreach (Template template in templates)
        {
            float distance = PathDistance(candidate, template.Points);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestMatch = template.Name;
            }
        }

        return bestMatch;
    }

    private List<Point> Resample(List<Point> points, int n)
    {
        float interval = PathLength(points) / (n - 1);
        float D = 0.0f;
        List<Point> newPoints = new List<Point> { points[0] };

        for (int i = 1; i < points.Count; i++)
        {
            float d = Distance(points[i - 1], points[i]);
            if ((D + d) >= interval)
            {
                float qx = points[i - 1].X + ((interval - D) / d) * (points[i].X - points[i - 1].X);
                float qy = points[i - 1].Y + ((interval - D) / d) * (points[i].Y - points[i - 1].Y);
                Point q = new Point(qx, qy);
                newPoints.Add(q);
                points.Insert(i, q);
                D = 0.0f;
            }
            else
            {
                D += d;
            }
        }

        if (newPoints.Count == n - 1)
        {
            newPoints.Add(new Point(points.Last().X, points.Last().Y));
        }

        return newPoints;
    }

    private List<Point> RotateToZero(List<Point> points)
    {
        Point centroid = Centroid(points);
        float theta = Mathf.Atan2(points[0].Y - centroid.Y, points[0].X - centroid.X);
        return RotateBy(points, -theta);
    }

    private List<Point> RotateBy(List<Point> points, float theta)
    {
        List<Point> newPoints = new List<Point>();
        Point centroid = Centroid(points);

        float cosTheta = Mathf.Cos(theta);
        float sinTheta = Mathf.Sin(theta);

        foreach (Point p in points)
        {
            float dx = p.X - centroid.X;
            float dy = p.Y - centroid.Y;
            newPoints.Add(new Point(
                dx * cosTheta - dy * sinTheta + centroid.X,
                dx * sinTheta + dy * cosTheta + centroid.Y
            ));
        }

        return newPoints;
    }

    private List<Point> ScaleToSquare(List<Point> points, float size)
    {
        Rect boundingBox = BoundingBox(points);
        List<Point> newPoints = new List<Point>();

        foreach (Point p in points)
        {
            float scaledX = p.X * (size / boundingBox.width);
            float scaledY = p.Y * (size / boundingBox.height);
            newPoints.Add(new Point(scaledX, scaledY));
        }

        return newPoints;
    }

    private List<Point> TranslateToOrigin(List<Point> points)
    {
        Point centroid = Centroid(points);
        List<Point> newPoints = new List<Point>();

        foreach (Point p in points)
        {
            newPoints.Add(new Point(p.X - centroid.X, p.Y - centroid.Y));
        }

        return newPoints;
    }

    private float PathLength(List<Point> points)
    {
        float length = 0.0f;
        for (int i = 1; i < points.Count; i++)
        {
            length += Distance(points[i - 1], points[i]);
        }
        return length;
    }

    private float PathDistance(List<Point> points1, List<Point> points2)
    {
        float distance = 0.0f;
        for (int i = 0; i < points1.Count; i++)
        {
            distance += Distance(points1[i], points2[i]);
        }
        return distance / points1.Count;
    }

    private float Distance(Point p1, Point p2)
    {
        return Mathf.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
    }

    private Point Centroid(List<Point> points)
    {
        float x = 0.0f;
        float y = 0.0f;

        foreach (Point p in points)
        {
            x += p.X;
            y += p.Y;
        }

        return new Point(x / points.Count, y / points.Count);
    }

    private Rect BoundingBox(List<Point> points)
    {
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        foreach (Point p in points)
        {
            if (p.X < minX) minX = p.X;
            if (p.Y < minY) minY = p.Y;
            if (p.X > maxX) maxX = p.X;
            if (p.Y > maxY) maxY = p.Y;
        }

        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }
}
