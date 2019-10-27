using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    interface Showable
    {
        string Name { get; set; }
        void show();
    }
    class Author: Showable
    {
        private string name;
        public string Name { get { return name; } set { name = value; } } // required
        public Author(string n) { name = n; }
        public void show()
        {
            Console.WriteLine("|Artist| Name:{0}", name);
        }
    }
    class Album: Showable
    {
        private string name;
        public string Name { get { return name; } set { name = value; } } // required
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
        public void show()
        {
            Console.WriteLine("|Album| Album:{0}\n \tGenre:{1}\n \tYear:{2}\n \tAuthor:{3}", name, genre.Name, year, author.Name);
        }
    }
    class Collection: Album
    {
        public List<Track> list = new List<Track>(30);
        public Collection(string n, Author a) : base(n, 2019, a) { }
        public Collection(string n, List<Track> l, Author a) : base(n, 2019, a) { list.AddRange(l); }
        public void Add(Track tr) { list.Add(tr); }
        public new void show() { Console.WriteLine("|Collection| Name: {0} includes {1} tracks", Name, list.Count); }
    }
    class Track: Showable
    {
        private string name;
        public string Name { get { return name; } set { name = value; } } // required
        public Album album;
        public Track(string n, Album ab)
        {
            name = n;
            album = ab;
        }
        public void show()
        {
            Console.WriteLine("|Track| Name:{0}\n \tAlbum:{1}\n \tGenre:{2}\n \tYear:{3}\n \tAuthor:{4}", name, album.Name, album.genre.Name, album.year, album.author.Name);
        }
    }
    class Genre: Showable
    {
        private string name;
        public string Name { get { return this.name; } set { this.name = value; } }
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
        public Genre() { this.name = "root";  }
        public void show()
        {
            Console.WriteLine("|Genre| Genre:{2}\n \tParentGenre:{3}", name, ParentGenre.Name);
        }
    }
    class Catalog
    {
        public Dictionary<string,Author> authors = new Dictionary<string, Author>(20);
        public Dictionary<string, Album> albums = new Dictionary<string, Album>(20);
        public Dictionary<string, Track> tracks = new Dictionary<string, Track>(20);
        public Dictionary<string, Collection> collections = new Dictionary<string, Collection>(20);
        public Genre rootGenre = new Genre();
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
            Genre gr = SearchEngine.BFSsearch(genre, rootGenre);
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
                Console.WriteLine("NEW GENRE {0}", gr.Name);
            }
            else { Console.WriteLine("This genre is already existed");}
            return gr;
        }
        public Collection add(string n, Author user)
        {
            if (!collections.ContainsKey(n)) { collections.Add(n, new Collection(n, username)); }
            return collections[n];
        }
        public Collection add(string n, Author user, List<Track> trs)
        {
            if (!collections.ContainsKey(n)) { collections.Add(n, new Collection(n, trs ,username)); }
            return collections[n];
        }
        public void showAll()
        {
            foreach (KeyValuePair<string, Track> i in tracks) { i.Value.show(); }
            foreach (KeyValuePair<string, Collection> i in collections) { i.Value.show(); }
        }
    }
    class SearchEngine
    {
        private Catalog ct;
        public List<Track> buffer = new List<Track>(20);
        private List<Showable> cache = new List<Showable>(20);
        public SearchEngine(Catalog c) { ct = c; }
        public static Genre BFSsearch(string Name, Genre rootGenre)
        {
            HashSet<string> used = new HashSet<string>(20);
            Queue<Genre> q = new Queue<Genre>();
            q.Enqueue(rootGenre);
            while (q.Count != 0)
            {
                Genre u = q.Dequeue();
                foreach (Genre i in u.ChildGenres)
                {
                    if (i.Name == Name) { return i; }
                    if (!used.Contains(i.Name))
                    {
                        used.Add(i.Name);
                        q.Enqueue(i);
                    }
                }
            }
            return null;
        }
        public void SearchInteractive()
        {
            Console.WriteLine("Searching engine. Search tracks by attribute. Add to buffer fo making collection. To show buffer print '*' ");
            Console.Write("SEARCH BY :");
            string attribute = Console.ReadLine();
            int counter = 1;
            if (attribute == "*")
            {
                foreach (Track tr in buffer) { Console.WriteLine("{0}: {1}", counter, tr.Name); counter++; }
                return;
            }
            Console.Write(" NAME: ");
            string value = Console.ReadLine();
            switch (attribute.ToLower())
            {
                case "track":
                case "name":
                    searchByTrack(value);
                    break;
                case "author":
                case "artist":
                    searchByArtist(value);
                    break;
                case "album":
                    searchByAlbum(value);
                    break;
                case "genre":
                    searchByGenre(value);
                    break;
                case "collections":
                case "collection":
                    searchByCollections(value);
                    break;
                case "year":
                    searchByYear(value);
                    break;
            }
            counter = 1;
            foreach (Showable o in cache)
            {
                Console.Write("{0}: ", counter);
                o.show();
                Console.WriteLine();
                counter++;
            }
            if (cache.Count > 0)
            {
                Console.Write("Add to buffer(only tracks, for making collection)? y/n > ");
                char c = Convert.ToChar(Console.Read());
                if (c == 'y')
                {
                    foreach (Track t in cache)
                    {
                        buffer.Add(t);
                    }
                }
                cache.Clear();
            }
        }
        public void searchByTrack(string n)
        {
            foreach (KeyValuePair<string, Track> tr in ct.tracks)
            {
                if (tr.Value.Name.ToLower().Contains(n.ToLower())) { cache.Add(tr.Value); }
            }
        }
        public void searchByArtist(string n)
        {
            foreach (KeyValuePair<string, Track> tr in ct.tracks)
            {
                if (tr.Value.album.author.Name.ToLower().Contains(n.ToLower())) { cache.Add(tr.Value); }
            }
        }
        public void searchByAlbum(string n)
        {
            foreach (KeyValuePair<string, Track> tr in ct.tracks)
            {
                if (tr.Value.album.Name.ToLower().Contains(n.ToLower())) { cache.Add(tr.Value); }
            }
        }
        public void searchByGenre(string n)
        {
            foreach (KeyValuePair<string, Track> tr in ct.tracks)
            {
                if (tr.Value.album.genre.Name.ToLower().Contains(n.ToLower())) { cache.Add(tr.Value); }
            }
        }
        public void searchByYear(string n)
        {
            if (Int32.TryParse(n, out int year))
            {
                foreach (KeyValuePair<string, Track> tr in ct.tracks)
                {
                    if (tr.Value.album.year == year) { cache.Add(tr.Value); }
                }
            }
            else { Console.WriteLine("Invalid year "); }
        }
        public void searchByCollections(string n)
        {
            foreach (KeyValuePair<string, Collection> cl in ct.collections)
            {
                if (cl.Value.Name.ToLower().Contains(n.ToLower())) { cache.AddRange(cl.Value.list); }
            }
        }
        public void makeCollectionFromBuffer()
        {
            Console.Write("Name of new collection: ");
            string collname = Console.ReadLine();
            ct.add(collname, null, buffer);
        }
        
    }
    class Program
    {
        private static Catalog ct = new Catalog();
        private static SearchEngine se = new SearchEngine(ct);
        static void Main()
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
                    case 3:
                        string CollName = Console.ReadLine();
                        ct.add(CollName, null);
                        break;
                    case 4:
                        string Coll_Name = Console.ReadLine();
                        ct.add(Coll_Name, null, se.buffer);
                        break;
                    case 5:
                        se.SearchInteractive();
                        break;
                    case 6:
                        se.makeCollectionFromBuffer();
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
