using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelirovanieSystem
{
    class OOS
    {
        List<Trebovanie> ochered;//очередь требований
        public int time = 0;//текущее время
        int next_obrabotka_time = 0;//время следующей обработки требования
        double l;//интенсивность пуассоновского потока
        int t1;//среднее время обработки требования первого типа
        int t2;// среднее время обработки требования второго типа
        double p1;// вероятность появления требования первого типа
        double p2;// вероятность появления требования второго типа
        int K;//Нужное Количество обработанных требований;
        int S=0;//Количество обработанных требований
        public int max = 0;
        public int sum = 0; 
        public OOS(double l, int t1, int t2, double p1, int K)
        {
            this.l = l;
            ochered = new List<Trebovanie>();
            this.t1 = t1;
            this.t2 = t2;
            this.p1 = p1;
            p2 = 1 - p1;
            this.K = K;
        }

        public void addTrebovanie(Trebovanie t)//добавления требования в очередь
        {
            ochered.Add(t);
        }

        public void obrabotatTrebovanie()//обработка требования
        {
            bool obrabotano = false;
            for (int i = 0; i < ochered.Count; i++)//ищем в очереди требования второго типа
            {
                if (ochered[i].is2type())//если такое есть
                {
                    ochered.RemoveAt(i);//Удаляем из очереди
                    obrabotano = true;
                    S++;
                    int extime = getExponentTime(t2);
                    File.AppendAllText("log.txt", extime + "\n");
                    next_obrabotka_time = time +extime;// и обрабатываем в течении времени полученному по Экспоненциальному распределению
                    break;
                }
            }
            if (!obrabotano)//если требований второго типа нет
            {
                if (ochered.Count > 0)
                {
                    ochered.RemoveAt(0);
                    S++;
                    int extime = getExponentTime(t1);
                    File.AppendAllText("log.txt", extime + "\n");
                    next_obrabotka_time = time + extime;//обрабатываем первое в очереди требование первого типа
                }
                else next_obrabotka_time++;
            }
        }
        Random random = new Random();

        int getExponentTime(int t)//Получение случайного времени обработки требования по Экспоненциальному распределению
        {
            return (int)(-t * Math.Log(random.NextDouble()));
        }

        void transaction()//То что происходит за 1 ед времени
        {
            sum += ochered.Count;
            if (ochered.Count > max)
                max = ochered.Count;
            if (time >= next_obrabotka_time)
            {
                obrabotatTrebovanie();
            }
            createTrebovaniya(potokPuas());
        }

        public int getOcherdCount()
        {
            return ochered.Count;
        }

        public void start()
        {
            while(S<K)
            {
                transaction();
                time++;
            }
        }

        Random randomTrebovaniy = new Random();

        void createTrebovaniya(int count)
        {
            for(int i=0; i<count; i++)
            {
                if (rand(p1, randomTrebovaniy.Next()))
                {
                    addTrebovanie(new Trebovanie(1));
                }
                else addTrebovanie(new Trebovanie(2));
            }
        }

        double puassonP(double l, int X)
        {
            double v1 = Math.Pow(l, (double)X);
            double v2 = Factorial(X);
            double v3 = Math.Exp(-l);
            return (v1 / v2) * v3;
        }
        int potokPuas()
        {
            int X = randomGeneral.Next(K);
            bool yes = false;
            while(!yes&&X>0)
            {
                yes = rand(puassonP(l, X), randomPuasson.Next());
                if (yes)
                {
                    return X;
                }
                else X--;
            }
            return X;
        }
        Random randomPuasson = new Random();
        Random randomGeneral = new Random();
        bool rand(double p,int rand)
        {
            if (p == 0)
                return false;
            int temp = (int)(1 / p);
            return rand % temp == 1;
        }
        public static double Factorial(int X)
        {

            double r = 1;
            for (int i = 2; i <= X; ++i)
                r *= i;
            return r;

        }
    }
}
