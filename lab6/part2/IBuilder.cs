using  System;

namespace part2
{
    public interface IBuider
    {
        public void reset();
        public void setMotherboard(Motherboard m);
        public void setCPU(CPU cpu);
        public void setStorage(Storage s);
        public void setRAM(RAM ram);
        public void setGPU(GPU gpu);
        public void setCooler(Cooler c);
    }
}