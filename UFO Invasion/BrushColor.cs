using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO_Invasion
{
    internal abstract class BrushColor : BrushColorBase
    {
        public Color FonColor;             // цвет фона
        public Color LaserColor;         // цвет лазера
        public Color DashBug;             // цвет штриховки НЛО
        public Color KilledBug;            // цвет сбитого НЛО

        public HatchBrush New_br(int rch)
        {
            return new HatchBrush(HatchStyle.DashedUpwardDiagonal, DashBug, RandomColor(rch));
        }   // кисть для задания цвета НЛО
        public Color RandomColor(int rch)
        {
            int r, g, b;
            byte[] bytes1 = new byte[3];        // массив 3 цветов
            Random rnd1 = new Random(rch);
            rnd1.NextBytes(bytes1);             // генерация в массив
            r = Convert.ToInt16(bytes1[0]);
            g = Convert.ToInt16(bytes1[1]);
            b = Convert.ToInt16(bytes1[2]);
            return Color.FromArgb(r, g, b);     // возврат цвета
        }     // генератор случайного цвета
    }
}
