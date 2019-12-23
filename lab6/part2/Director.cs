using System;

namespace part2
{
    class Director
    {
        public PC currPC;
        public void makeLowCostPC(IBuider buider)
        {
            buider.setMotherboard(new Motherboard("Z77", "MSI"));
            buider.setCPU(new CPU("3570k", "Intel", 3.70));
            buider.setStorage(new Storage("WD34345", "WD", StorageType.HDD));
            buider.setRAM(new RAM("UW324", "Kingston", RAMType.DDR3));
            buider.setGPU(new GPU("GTX 1050", "NVidia"));
            buider.setCooler(new Cooler("CL345", "DeepCool"));
        }
    }
}