﻿using System;
using System.Collections.Generic;

namespace Quzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Block> blocks = new List<Block>();
            blocks.Add(new Block(Shape.kSquare, 0, 0));
            blocks.Add(new Block(Shape.kHorizon, 2, 0));
            blocks.Add(new Block(Shape.kVertical, 2, 1));
            blocks.Add(new Block(Shape.kVertical, 3, 1));
            blocks.Add(new Block(Shape.kVertical, 0, 3));
            blocks.Add(new Block(Shape.kHorizon, 1, 3));
            blocks.Add(new Block(Shape.kHorizon, 1, 4));
            blocks.Add(new Block(Shape.kSingle, 3, 3));
            blocks.Add(new Block(Shape.kSingle, 3, 4));

            State initial = new State(blocks, 0);
            BFS bfs = new BFS(initial);
            bfs.run();

            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
    }

    public static class Globals
    {
        public const int kRows = 5;
        public const int kColumns = 4;
        public const int kBlocks = 9;
    }

}
