using System.Collections.Generic;
using System.Diagnostics;

namespace Quzzle
{
    public delegate void Search(State next);

    public class State
    {
        List<Block> blocks_;
        Mask prev_;
        int step_;

        public List<Block> blocks
        {
            set { blocks = value; }
            get { return blocks_; }
        }

        public Mask Prev
        {
            get { return prev_; }
        }

        public int step
        {
            get { return step_; }
        }

        public State()
        {
            blocks_ = new List<Block>();
            step_ = 0;
        }

        public State(State state)
        {
            blocks_ = new List<Block>();
            for (int i = 0; i < Globals.kBlocks; i++)
            {
                blocks_.Add(new Block(state.blocks_[i]));
            }
            step_ = state.step_;
        }

        public State(List<Block> blocks, int step)
        {
            blocks_ = blocks;
            step_ = step;
        }

        public Mask toMask()
        {
            Mask m = new Mask();
            for (int i = 0; i < Globals.kBlocks; i++)
            {
                Block b = blocks_[i];
                b.mask((int)b.shape, m);
            }

            return m;
        }

        public bool isSolved()
        {
            Block square = blocks_[0];
            Debug.Assert(square.shape == Shape.kSquare);
            return (square.left == 2 && square.top == 0);
        }

        public override bool Equals(object obj)
        {
            State s = obj as State;
            bool isEqual = true;
            for (int i = 0; i < Globals.kBlocks; i++)
            {
                isEqual = isEqual && blocks_[i].Equals(s.blocks_[i]);
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return blocks_[0].left << 3 + blocks_[0].top;
        }

        public void move(Search search)
        {
            prev_ = toMask();

            for (int i = 0; i < Globals.kBlocks; i++)
            {
                Block b = blocks_[i];

                // move up
                if (b.top > 0 && prev_.empty(b.top - 1, b.left) && prev_.empty(b.top - 1, b.right))
                {
                    State next = new State(this);
                    next.prev_ = prev_;
                    next.step_++;
                    next.blocks_[i].top--;
                    search(next);
                }

                // move down
                if (b.bottom < Globals.kRows - 1 && prev_.empty(b.bottom + 1, b.left) && prev_.empty(b.bottom + 1, b.right))
                {
                    State next = new State(this);
                    next.prev_ = prev_;
                    next.step_++;
                    next.blocks_[i].top++;
                    search(next);
                }

                // move left
                if (b.left > 0 && prev_.empty(b.top, b.left - 1) && prev_.empty(b.bottom, b.left - 1))
                {
                    State next = new State(this);
                    next.prev_ = prev_;
                    next.step_++;
                    next.blocks_[i].left--;
                    search(next);
                }

                // move right
                if (b.right < Globals.kColumns - 1 && prev_.empty(b.top, b.right + 1) && prev_.empty(b.bottom, b.right + 1))
                {
                    State next = new State(this);
                    next.prev_ = prev_;
                    next.step_++;
                    next.blocks_[i].left++;
                    search(next);
                }
            }
        }

    }
}
