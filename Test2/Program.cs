using System;
using System.Collections.Generic;

namespace Test2
{
    class Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public Point(double a, double b) { x = a; y = b; }
        public static double length(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2)+Math.Pow(a.y - b.y, 2));
        }
        public void show()
        {
            Console.WriteLine("({0}, {1})", x, y);
        }
    }
    class Vector
    {
        public double x;
        public double y;
        public Vector(double a, double b)
        {
            x = a; y = b;
        }
        public double length 
        { 
            get { return Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2)); } 
        }
    }
    class Square
    {
        private List<Point> points = new List<Point>(4);
        public Square(Point a, Point b, Point c, Point d)
        {
            points.Add(a); points.Add(b);
            points.Add(c); points.Add(d);
        }
        private double square()
        {
            return Math.Pow(Point.length(points[0], points[1]), 2);
        }
        private double diametr()
        {
            return Point.length(points[0], points[2]);
        }
        public void shift(Vector v)
        {
            foreach (Point p in points)
            {
                p.x += v.x; p.y += v.y;
            }
        }
        public void show()
        {
            foreach (Point p in points)
            {
                p.show();
            }
            Console.WriteLine("Square = {0}", this.square());
            Console.WriteLine("Diametr = {0}", this.diametr());
        }
    }
    class Program
    {
        static void test1()
        {
            Square a = new Square(new Point(0, 0), new Point(5, 0), new Point(5, 5), new Point(0, 5));
            a.show();
            Vector v = new Vector(2, 0);
            a.shift(v);
            a.show();
        }
        static void test2()
        {
            Square a = new Square(new Point(0, 0), new Point(-4, 5), new Point(-9, 1), new Point(-5, -4));
            a.show();
            Vector v = new Vector(4, -5);
            a.shift(v);
            a.show();
        }
        static void test3()
        {
            Square a = new Square(new Point(2, 1), new Point(1, -3), new Point(-3, -2), new Point(-2, 2));
            a.show();
            Vector v = new Vector(4, -5);
            a.shift(v);
            a.show();
            Vector v2 = new Vector(-4, 5);
            a.shift(v2);
            a.show();
        }
        static void Main(string[] args)
        {
            test3();
            Console.ReadKey();
        }
    }
}
