using NUnit.Framework;

namespace Test2
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Square a = new Square(new Point(0, 0), new Point(5, 0), new Point(5, 5), new Point(0, 5));
            Vector v = new Vector(2, 0);
            a.shift(v);
            Assert.AreEqual(expected: 2, actual: a.points[0].x, delta: 0.001);
        }
        [Test]
        public void Test2()
        {
            Square a = new Square(new Point(0, 0), new Point(-4, 5), new Point(-9, 1), new Point(-5, -4));
            Vector v = new Vector(4, -5);
            a.shift(v);
            Assert.AreEqual(expected: -9, actual: a.points[3].y, delta: 0.001);
        }
        [Test]
        public void Test3()
        {
            Square a = new Square(new Point(2, 1), new Point(1, -3), new Point(-3, -2), new Point(-2, 2));
            Vector v = new Vector(4, -5);
            a.shift(v);
            Vector v2 = new Vector(-4, 5);
            a.shift(v2);
            Assert.AreEqual(expected: 1, actual: a.points[0].y, delta: 0.001);
        }
        [Test]
        public void Test4()
        {
            Square a = new Square(new Point(0, 0), new Point(-4, 5), new Point(-9, 1), new Point(-5, -4));
            Vector v = new Vector(4, -5);
            a.shift(v);
            Assert.AreEqual(expected: 41, actual: a.square(), delta: 0.001);
        }
    }
}