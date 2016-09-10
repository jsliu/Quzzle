using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Quzzle
{
    /* Breadth First Search algorithm */
    public class BFS
    {
        private Queue<State> unvisited;
        private Hashtable visited;
        State initial_, final_;

        public BFS(State initial)
        {
            unvisited = new Queue<State>();
            visited = new Hashtable();
            initial_ = initial;
        }

        public void run()
        {
            unvisited.Enqueue(initial_);
            visited.Add(initial_.toMask(), initial_.Prev);

            State curr = new State();
            while (unvisited.Any())
            {
                curr = unvisited.Dequeue();

                if (curr.isSolved())
                {
                    Console.WriteLine("Found solution with {0} steps", curr.step);
                    break;
                }
                else if (curr.step > 500)
                {
                    Console.WriteLine("Too many steps.");
                    break;
                }

                curr.move(new Search(search));
            }

            final_ = curr;

            print();
        }

        private void search(State next)
        {
            if (!visited.ContainsKey(next.toMask()))
            {
                visited.Add(next.toMask(), next.Prev);
                unvisited.Enqueue(next);
            }
        }

        private void print()
        {
            Stack<Mask> path = new Stack<Mask>();
            Mask curr = final_.toMask();

            while (!curr.Equals(initial_.toMask()))
            {
                path.Push(curr);
                Mask prev = visited[curr] as Mask;
                curr = prev;
            }
            path.Push(curr);

            int step = 0;
            using (StreamWriter sw = new StreamWriter(@"Blackrock_Interview_Solution.txt"))
            {
                while (path.Count > 0)
                {
                    Mask m = path.Pop();
                    Console.WriteLine("#### Step {0} ###", step);
                    sw.WriteLine("#### Step {0} ###", step);
                    Console.WriteLine("=================");
                    sw.WriteLine("=================");
                    m.print(sw);
                    Console.WriteLine("=================");
                    sw.WriteLine("=================");
                    step++;
                }
            }
        }
    }
}
