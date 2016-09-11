using System.Diagnostics;

namespace Quzzle

{
    public class Block
    {
        //private static readonly int[] delta_right_ = { 0, 0, 0, 1, 1, 1, 0, 0, 0, 1 };
        //private static readonly int[] delta_bottom_ = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 };

        private static readonly int[] delta_right_ = { 0, 0, 1, 0, 1 };
        private static readonly int[] delta_bottom_ = { 0, 0, 0, 1, 1 };

        private Shape shape_;
        private int top_, left_;

        public Shape shape
        {
            get { return shape_; }
        }

        public int left
        {
            get { return left_; }
            set { left_ = value; }
        }

        public int top
        {
            get { return top_; }
            set { top_ = value; }
        }

        public int right
        {
            get
            {
                Debug.Assert(shape_ != Shape.kInvalid);
                return left_ + delta_right_[(int)shape_];
            }
        }

        public int bottom
        {
            get
            {
                Debug.Assert(shape_ != Shape.kInvalid);
                return top_ + delta_bottom_[(int)shape_];
            }
        }

        public Block()
        {
            shape_ = Shape.kInvalid;
            left_ = -1;
            top_ = -1;
        }

        public Block(Block block)
        {
            shape_ = block.shape_;
            left_ = block.left_;
            top_ = block.top_;

            Debug.Assert(shape_ != Shape.kInvalid);
            Debug.Assert(left_ >= 0 && left < Globals.kColumns);
            Debug.Assert(top_ >= 0 && top < Globals.kRows);
        }

        public Block(Shape s, int left, int top)
        {
            shape_ = s;
            left_ = left;
            top_ = top;

            Debug.Assert(shape_ != Shape.kInvalid);
            Debug.Assert(left_ >= 0 && left < Globals.kColumns);
            Debug.Assert(top_ >= 0 && top < Globals.kRows);
        }


        public override bool Equals(object obj)
        {
            Block b = obj as Block;
            return left_ == b.left_ && top_ == b.top_;
        }

        public override int GetHashCode()
        {
            return left_ << 3 + top_;
        }


        public void mask(int value, Mask mask)
        {
            mask.set(value, top_, left_);
            switch (shape_)
            {
                case Shape.kHorizon:
                    mask.set(value, top_, left_ + 1);
                    break;
                case Shape.kVertical:
                    mask.set(value, top_ + 1, left_);
                    break;
                case Shape.kSquare:
                    mask.set(value, top_, left_ + 1);
                    mask.set(value, top_ + 1, left_);
                    mask.set(value, top_ + 1, left_ + 1);
                    break;
                default:
                    Debug.Assert(shape_ == Shape.kSingle);
                    break;
            };
        }

    }
}
