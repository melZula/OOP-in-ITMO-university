using System;

namespace part2
{
    public class Motherboard
    {
        public Motherboard(string n, string m)
        {
            name = n; manufacturer = m;
        }
        public string name { get; set; }
        public string manufacturer { get; set; }
    }
    public class CPU
    {
        public CPU(string n, string m, double fr)
        {
            name = n; manufacturer = m; frequency = fr;
        }
        public string name { get; set; }
        public string manufacturer { get; set; }
        public double frequency { get; set; }
    }
    public enum StorageType
    {
        HDD = 1,
        SSD
    }
    public class Storage
    {
        public Storage(string n, string m, StorageType t)
        {
            name = n; manufacturer = m; type = t;
        }
        public string name { get; set; }
        public string manufacturer { get; set; }
        public StorageType type { get; set; }
    }
    public enum RAMType
    {
        DDR3 = 1,
        DDR4
    }
    public class RAM
    {
        public RAM(string n, string m, RAMType t)
        {
            name = n; manufacturer = m; type = t;
        }
        public string name { get; set; }
        public string manufacturer { get; set; }
        public RAMType type { get; set; }
    }
    public class GPU
    {
        public GPU(string n, string m)
        {
            name = n; manufacturer = m;
        }
        public string name { get; set; }
        public string manufacturer { get; set; }       
    }
    public class Cooler
    {
        public Cooler(string n, string m)
        {
            name = n; manufacturer = m;
        }
        public string name { get; set; }
        public string manufacturer { get; set; }  
    }
}