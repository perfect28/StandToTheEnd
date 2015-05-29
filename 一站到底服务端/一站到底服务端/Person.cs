using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 一站到底服务端
{
    public enum State
    {
        Alive, Dead
    }

    public class Person
    {
        public string Name
        {
            get;
            set;
        }
        public State state
        {
            get;
            set;
        }
        public int Position
        {
            get;
            set;
        }
        public Person()
        {
            Name = "";
            state = State.Alive;
            Position = 0;
        }
    }
}
