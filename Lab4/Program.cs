using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab4
{
    interface Showable
    {
        void show();
        string Name { get; set; }
    }
    class Field<T> : Showable
    {
        private string name { get; set; }
        public T value { get; set; }
        public void show() { Console.WriteLine("{0}={1}", name, value); }
        public string Name { get { return this.name; } set { this.name = value; } }
    }
    class Section: Showable
    {
        private string name;
        public ArrayList fields = new ArrayList();
        public Section(string n)
        {
            Name = n;
        }
        public void show()
        {
            Console.WriteLine("[{0}]", Name);
            foreach (Showable f in fields)
            {
                f.show();
            }
        }
        public string Name { get { return this.name; } set { this.name = value; } }
    }
    class iniEditor
    {
        List<Section> sections = new List<Section>();
        public void input_from_file()
        {
            int counter = 0;
            string line;
            try
            {
                using (StreamReader file = new StreamReader(@"..\..\test1.ini"))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(line) || line[0] == ';' || line[0] == '#') { continue; }
                        else
                        {
                            line = line.Split(';')[0].Split('#')[0];
                            if (line[0] == '[' && line[line.Length-1] == ']')
                            {
                                Section sec = new Section(line.Replace("[","").Replace("]",""));
                                sections.Add(sec);
                            }
                            else
                            {
                                if (line.Contains("="))
                                {
                                    string[] raw = line.Split('=');
                                    raw[0] = raw[0].Trim(); raw[1] = raw[1].Trim();
                                    if ((raw[1][0] == '\'' && raw[1][raw[1].Length - 1] == '\'') || (raw[1][0] == '\"' && raw[1][raw[1].Length - 1] == '\"'))
                                    {
                                        Field<string> f = new Field<string>();
                                        f.Name = raw[0];
                                        f.value = raw[1].Trim('\'').Trim('\"');
                                        sections[sections.Count - 1].fields.Add(f);
                                    }
                                    else
                                    {
                                        if (Regex.Matches(raw[1].Trim(), "^[0-9]+[.,][0-9]+").Count > 0)
                                        {
                                            Field<double> f = new Field<double>();
                                            f.Name = raw[0];
                                            double.TryParse(raw[1].Replace('.',','), out double temp); f.value = temp;
                                            sections[sections.Count - 1].fields.Add(f);
                                        }
                                        else
                                        {
                                            if (Int32.TryParse(raw[1], out int temp))
                                            {
                                                Field<int> f = new Field<int>();
                                                f.Name = raw[0];
                                                f.value = temp;
                                                sections[sections.Count - 1].fields.Add(f);
                                            }
                                            else
                                            {
                                                Field<string> f = new Field<string>();
                                                f.Name = raw[0];
                                                f.value = raw[1];
                                                sections[sections.Count - 1].fields.Add(f);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid string n.{0}", counter);
                                }
                            }
                        }
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
        public void print(string sec, string field, string type)
        {
            bool find = false;
            foreach (Section section in sections)
            {
                if (section.Name == sec)
                {
                    foreach (Showable o in section.fields)
                    {
                        if (  (o.Name == field || string.IsNullOrEmpty(field)) && (o.GetType().GetGenericArguments()[0].ToString().Split('.')[1] == type || string.IsNullOrEmpty(type))  ) 
                        { 
                            o.show();
                            find = true;
                        }
                    }
                }
            }
            if (!find) { Console.WriteLine("No field founded"); }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            iniEditor ed = new iniEditor();
            ed.input_from_file();
            ed.print("SectionOne", "", "Int32"); // String Double Int32
            Console.ReadKey();
        }
    }
}
