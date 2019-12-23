using System;
using System.Collections.Generic;

namespace part1
{
    public interface IStrategy
    {
        public List<Item> execute(Data data);
    }
}