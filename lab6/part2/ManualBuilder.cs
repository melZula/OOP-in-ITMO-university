using System;

namespace part2
{
    class ManualBuilder: IBuider
    {
        public void reset() {}
        public void setMotherboard(Motherboard m)
        {
            System.Console.WriteLine("Motherboard: {0} - {1}", m.name, m.manufacturer);
        }
        public void setCPU(CPU cpu)
        {
            System.Console.WriteLine("CPU: {0} -  {1} - {2}", cpu.name, cpu.manufacturer, cpu.frequency);
        }
        public void setStorage(Storage s)
        {
            System.Console.WriteLine("Storage: {0} - {1} - {2}", s.name, s.manufacturer, s.type);
        }
        public void setRAM(RAM ram)
        {
            System.Console.WriteLine("Memory: {0} - {1} - {2}", ram.name, ram.manufacturer, ram.type);
        }
        public void setGPU(GPU gpu)
        {
            System.Console.WriteLine("Graphics: {0} - {1}", gpu.name, gpu.manufacturer);
        }
        public void setCooler(Cooler c)
        {
            System.Console.WriteLine("Cooler {0} - {1}", c.name, c.manufacturer);
        }
    }
}