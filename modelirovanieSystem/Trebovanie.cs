using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelirovanieSystem
{
    class Trebovanie
    {
        int type;
        public Trebovanie(int type)
        {
            this.type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj is Trebovanie)
                return ((Trebovanie)obj).type == type;
            else return false;
        }

        public bool is2type()
        {
            return type == 2;
        }
    }
}
