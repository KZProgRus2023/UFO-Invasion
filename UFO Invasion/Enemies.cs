using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO_Invasion
{
    internal class Enemies
    {
        public int Delta_N;                // количество НЛО в серии
        public int N_generation;     // число генераций — серий
        public int k_generation;     // номер серии
        public int N;                            // актуальное количество НЛО на экране
        public Bugs[] bugs = new Bugs[Form1.N_max];
        public void New_Enemies(Form1 F)
        {
            N_generation = 10;
            Delta_N = Form1.N_max / N_generation;
            k_generation = 0;
            N = 0;
            for (int j = 0; j < Form1.N_max; j++)
                bugs[j] = new Bugs();
        }     // инициализация объектов НЛО
        public void Show_bugs(Form1 F)
        {
            for (int j = 0; j < N; j++)
            {
                bugs[j].Move_bug();
                F.g.FillRegion(bugs[j].br, bugs[j].reg);
            }
        }          // сдвинуть и показать «живые» НЛО
        public void Enemy(Form1 F)
        {
            int N0 = N;
            N = N + Delta_N;
            int rch;
            Random rnd = new Random();
            for (int j = N0; j < N; j++)
            {
                bugs[j] = new Bugs();
                rch = rnd.Next();
                bugs[j].New_bug(F, rch);
                F.g.FillRegion(bugs[j].br, bugs[j].reg);
            }
        }                   // генерация одной серии НЛО
        public void Killed_bugs(Form1 F, int x, int y)
        {
            for (int j = 0; j < N; j++)
            {
                Rectangle r = new Rectangle(x - bugs[j].size.Width / 2, 0, bugs[j].size.Width, y);
                if (bugs[j].reg.IsVisible(r, (Graphics)F.g) & F.laser)
                {
                    bugs[j].br = new HatchBrush(HatchStyle.DarkHorizontal, F.bc.KilledBug, F.bc.KilledBug);
                    object value = F.g.FillRegion(bugs[j].br, bugs[j].reg);
                    bugs[j].life = false;
                }
            }
        }          // определение сбитых НЛО
        public int Select_bugs()
        {
            int k = 0;
            for (int j = 0; j < N; j++)
            {
                if (!bugs[j].life)
                    k++;
            }
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (!bugs[j].life)
                    {
                        for (int j1 = j; j1 < (N - 1); j1++)
                            bugs[j1] = bugs[j1 + 1];
                        break;
                    }
                }
                N--;
            }
            return k;    // счетчик подбитых НЛО
        }             // удаление сбитых НЛО
    }
}
