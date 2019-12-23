using System;

namespace part1
{
    public class Context
    {
        private IStrategy strategy;
        public void setStrategy(IStrategy st)
        {
            this.strategy = st;
        }
        public void executeStrategy(Data data)
        {
            var all = strategy.execute(data);
            foreach (Item i in all)
            {
                foreach (var x in i.GetType().GetProperties())
                {
                    System.Console.Write(x.GetValue(i, null)+" ");
                }
                System.Console.WriteLine();
            }
        }
    }
}