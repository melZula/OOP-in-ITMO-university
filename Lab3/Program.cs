using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Author
    {
        public string name;
        public Author(string n)
        {
            name = n;
        }
    }
    class Album
    {
        public string name;
        public int year;
        public Author author;
        public Album(string n, int y, Author a)
        {
            author = a;
            year = y;
            name = n;
        }
    }
    class Track
    {
        private string TrackName;
        private Album album;
        public Track(string n, Album ab)
        {
            TrackName = n;
            album = ab;
        }
        public void show()
        {
            Console.WriteLine("|Track| name:{0}\n \tAlbum:{1}\n \tYear:{2}\n \tAuthor:{3}", TrackName, album.name, album.year, album.author.name);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Author moon = new Author("M.O.O.N.");
            Album hm2 = new Album("HM2 OST", 2015, moon);
            Track tr1 = new Track("Dust", hm2);
            tr1.show();
            Console.ReadKey();

        }
    }
}
