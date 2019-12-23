using System;

namespace part2
{
    class Program
    {
        static void Main(string[] args)
        {
            Director dr = new Director();
            PCbuilder buildPC = new PCbuilder();
            dr.makeLowCostPC(buildPC);
            PC myPC = buildPC.getResult();

            ManualBuilder buildManual = new ManualBuilder();
            dr.makeLowCostPC(buildManual);
        }
    }
}
