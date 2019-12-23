using System;

namespace part2
{
    class PCbuilder: IBuider
    {
        private PC pc { get; set; }
        public void reset()
        {
            pc = new PC();
        }
        public void setMotherboard(Motherboard m)
        {
            System.Console.WriteLine("setting motherboard...");
        }
        public void setCPU(CPU cpu)
        {
            System.Console.WriteLine("setting CPU...");
        }
        public void setStorage(Storage s)
        {
            System.Console.WriteLine("setting storage...");
        }
        public void setRAM(RAM ram)
        {
            System.Console.WriteLine("setting memory...");
        }
        public void setGPU(GPU gpu)
        {
            System.Console.WriteLine("setting graphics...");
        }
        public void setCooler(Cooler c)
        {
            System.Console.WriteLine("setting cooler...");
        }
        public PC getResult()
        {
            return pc;
        }
    }
}