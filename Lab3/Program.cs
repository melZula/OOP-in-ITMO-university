using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Author
    {
        public string name; // required
        public Author(string n) { name = n; }
    }
    class Album
    {
        public string name; // required
        public int year; // required
        public Author author; // required
        public Genre genre;
        public Album(string n, int y, Genre g ,Author a)
        {
            author = a;
            genre = g;
            year = y;
            name = n;
        }
        public Album(string n, int y, Author a)
        {
            author = a;
            year = y;
            name = n;
        }
    }
    class Collection: Album
    {
        private List<Track> list = new List<Track>(30);
        public Collection(string n, Author a) : base(n, 2019, a) { }
        public Collection(string n, List<Track> l, Author a) : base(n, 2019, a) { list.AddRange(l); }
        public void Add(Track tr) { list.Add(tr); }
    }
    class Track
    {
        private string TrackName; // required
        private Album album;
        public Track(string n, Album ab)
        {
            TrackName = n;
            album = ab;
        }
        public void show()
        {
            Console.WriteLine("|Track| name:{0}\n \tAlbum:{1}\n \tGenre:{2}\n \tYear:{3}\n \tAuthor:{4}", TrackName, album.name, album.genre.name, album.year, album.author.name);
        }
    }
    class Genre
    {
        public string name;
        public Genre ParentGenre;
        public Genre ParentGenre2;
        public List<Genre> ChildGenres = new List<Genre> { };
        public Genre(string n, Genre pr)
        {
            name = n;
            ParentGenre = pr;
            ParentGenre.ChildGenres.Add(this);
        }
        public Genre(string n, Genre pr1, Genre pr2)
        {
            name = n;
            ParentGenre = pr1;
            ParentGenre2 = pr2;
        }
        public Genre() { name = "root"; }
    }
    class Catalog
    {
        private Dictionary<string,Author> authors = new Dictionary<string, Author>(20);
        private Dictionary<string, Album> albums = new Dictionary<string, Album>(20);
        private Dictionary<string, Track> tracks = new Dictionary<string, Track>(20);
        private Dictionary<string, Collection> collections = new Dictionary<string, Collection>(20);
        private Genre rootGenre = new Genre();
        private Author username = new Author("User");
        public void add(string athr, string al, string genre, string tr, int yr)
        {
            if (!authors.ContainsKey(athr)) { authors.Add(athr, new Author(athr)); }
            if (!albums.ContainsKey(al)) 
            {
                Genre gr = add(genre);
                albums.Add(al, new Album(al, yr, gr, authors[athr])); 
            }
            if (!tracks.ContainsKey(tr)) 
            { 
                tracks.Add(tr, new Track(tr, albums[al]));
                Console.Write("Added: ");
                tracks[tr].show();
            }
        }
        public Genre add(string genre)
        {
            Genre gr = BFSsearch(genre);
            if (gr == null)
            {
                if (genre.Contains('/'))
                {
                    string[] subs = genre.Split('/');
                    Genre sub1 = add(subs[0]);
                    Genre sub2 = add(subs[1]);
                    gr = new Genre(genre, sub1, sub2);
                }
                else { gr = new Genre(genre, rootGenre); }
                Console.WriteLine("NEW GENRE {0}", gr.name);
            }
            else { Console.WriteLine("This genre is already existed");}
            return gr;
        }
        public Collection add(string n, Author user)
        {
            if (!collections.ContainsKey(n)) { collections.Add(n, new Collection(n, username)); }
            return collections[n];
        }
        public void showAll()
        {
            foreach (KeyValuePair<string, Track> i in tracks) { i.Value.show(); }
        }
        public Genre BFSsearch(string Name)
        {
            HashSet<string> used = new HashSet<string>(20);
            Queue<Genre> q = new Queue<Genre>();
            q.Enqueue(rootGenre);
            while (q.Count != 0)
            {
                Genre u = q.Dequeue();
                foreach (Genre i in u.ChildGenres)
                {
                    if (i.name == Name) { return i; }
                    if (!used.Contains(i.name))
                    {
                        used.Add(i.name);
                        q.Enqueue(i);
                    }
                }
            }
            return null;
        }
    }
    class Program
    {
        private static Catalog ct = new Catalog();
        static void Main(string[] args)
        {
            ct.add("M.O.O.N", "HM2 OST","Synthwave", "Dust", 2015);
            
            Console.ReadKey();
            Menu();
        }
        static void Menu()
        {
            Console.WriteLine("MUSIC CATALOG");

            int mode = 420;
            while (mode != 9)
            {
                switch (mode)
                {
                    case 0:
                        input();
                        break;
                    case 1:
                        input_from_file();
                        break;
                    case 2:
                        ct.showAll();
                        break;
                }
                Console.WriteLine("\nChoose action (write num): ");
                if (!Int32.TryParse(Console.ReadLine(), out mode)) mode = 999;
            }
        }
        static void input()
        {
            try
            {
                Console.Write("Author: ");
                string athr = Console.ReadLine();
                Console.Write("Albom: ");
                string al = Console.ReadLine();
                Console.Write("Genre: ");
                string gr = Console.ReadLine();
                Console.Write("Track: ");
                string tr = Console.ReadLine();
                Console.Write("Year: ");
                int yr = Convert.ToInt32(Console.ReadLine());

                ct.add(athr, al, gr, tr, yr);

            }
            catch { new Exception(); }
        }
        static void input_from_file()
        {
            int counter = 0;
            string line;
            try
            {
                using (StreamReader file = new StreamReader("LessMusic.csv"))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (counter == 0) { counter++; continue; } // pass first line
                        string[] raw = line.Split(';'); // Artist;Track name;Track length;Album name;Year;Genre
                        Int32.TryParse(raw[4], out int yr);
                        ct.add(raw[0], raw[3], raw[5], raw[1], yr);
                        counter++;
                    }
                    file.Close();
                    System.Console.WriteLine("There were {0} lines.", counter);
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
