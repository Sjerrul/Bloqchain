﻿using Sjerrul.Bloqchain.Ledger;
using System;

namespace Sjerrul.Bloqchain.ConsoleDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            BloqChain<string> chain = new BloqChain<string>();

            chain.AddBloq("Hello");
            chain.AddBloq("Hello everyone!");

            foreach (var bloq in chain)
            {
                Console.WriteLine($"# Bloq {bloq.Index}");
                Console.WriteLine($"Data: {bloq.Data}");
                Console.WriteLine($"Previous Hash: {bloq.PreviousHash}");
                Console.WriteLine($"Hash: {bloq.Hash}");
                Console.WriteLine();
            }

            Console.WriteLine($"Is valid: {chain.IsValid}");
            Console.WriteLine();

            Console.WriteLine("Changing value in Bloq 1");
            chain[1].Data = "Bye";
            Console.WriteLine($"Is valid: {chain.IsValid}");
            Console.WriteLine();


            Console.WriteLine("Recalculating Hash of Bloq 1");
            chain[1].Hash = chain[1].CalculateHash();
            Console.WriteLine($"Is valid: {chain.IsValid}");
            Console.WriteLine();

            Console.WriteLine("Changing PreviousHash of Bloq 2");
            chain[2].PreviousHash = chain[1].Hash;
            Console.WriteLine($"Is valid: {chain.IsValid}");
            Console.WriteLine();

            Console.WriteLine("Recalculating Hash of Bloq 2");
            chain[2].Hash = chain[2].CalculateHash();
            Console.WriteLine($"Is valid: {chain.IsValid}");
            Console.WriteLine();

            foreach (var bloq in chain)
            {
                Console.WriteLine($"# Bloq {bloq.Index}");
                Console.WriteLine($"Data: {bloq.Data}");
                Console.WriteLine($"Previous Hash: {bloq.PreviousHash}");
                Console.WriteLine($"Hash: {bloq.Hash}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
