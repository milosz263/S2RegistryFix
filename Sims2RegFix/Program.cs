﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2RegFix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RegBuilder.GenerateRegFile();
            RegBuilder.ExecuteRegFile();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
