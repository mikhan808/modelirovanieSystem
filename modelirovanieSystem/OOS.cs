using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelirovanieSystem
{
    class OOS
    {
        List<Trebovanie> ochered;
        int time = 0;
        int next_obrabotka_time = 0;
        int l;
        public OOS(int l)
        {
            this.l = l;
            ochered = new List<Trebovanie>();
        }

        public void addTrebovanie(Trebovanie t)
        {
            ochered.Add(t);
        }

        public void obrabotatTrebovanie()
        {
            bool obrabotano = false;
            for(int i=0;i<ochered.Count;i++)
            {
                if (ochered[i].is2type())
                {
                    ochered.RemoveAt(i);
                    obrabotano = true;
                    break;
                }
            }
            if(!obrabotano)
            {
                if (ochered.Count > 0)
                    ochered.RemoveAt(0);
            }
        }

        int getExponentTime(int x,int t)
        {
            return (int)(1 - Math.Exp( -(1/t) * x));
        }

        void transaction()
        {
            if(time == next_obrabotka_time)
            {
                obrabotatTrebovanie();
            }
        }
    }
}
