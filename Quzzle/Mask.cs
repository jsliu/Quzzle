using System;
using System.Diagnostics;
using System.IO;

namespace Quzzle
{
    public class Mask
    {
        private int[,] board_ = new int[Globals.kRows, Globals.kColumns];

        public Mask()
        {
            for (int i = 0; i < Globals.kRows; i++)
            {
                for (int j = 0; j < Globals.kColumns; j++)
                {
                    board_[i, j] = 0;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Mask p = obj as Mask;
            if ((System.Object)p == null) return false;

            bool isEqual = true;
            for (int i = 0; i < Globals.kRows; i++)
            {
                for (int j = 0; j < Globals.kColumns; j++)
                {
                    isEqual = isEqual && (board_[i, j] == p.board_[i, j]);
                }
            }

            return isEqual;
        }

        public override int GetHashCode()
        {
            return 37 * board_[0, 0] + board_[1, 1];
        }


        public void print(StreamWriter sw)
        {
            for (int i = 0; i < Globals.kRows; i++)
            {
                for (int j = 0; j < Globals.kColumns; j++)
                {
                    Console.Write(board_[i, j]);
                    sw.Write(board_[i, j]);
                }
                Console.Write("\n");
                sw.Write("\n");
            }
        }

        public void set(int value, int x, int y)
        {
            Debug.Assert(value > 0);
            Debug.Assert(x >= 0 && x < Globals.kRows);
            Debug.Assert(y >= 0 && y < Globals.kColumns);
            board_[x, y] = value;
        }

        public bool empty(int x, int y)
        {
            Debug.Assert(x >= 0 && x < Globals.kRows);
            Debug.Assert(y >= 0 && y < Globals.kColumns);
            return board_[x, y] == 0;
        }
    }
}