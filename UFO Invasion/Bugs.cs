using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UFO_Invasion
{
    internal abstract class Bugs : BugsBase
    {
        public Point point;                // положение НЛО в 2D-пространстве
        public Size size;                     // размеры НЛО
        int veloX;                                  // скорость смещения по X
        int veloY;                                  // скорость_падения по Y
        public HatchBrush br;        // кисть для покраски НЛО
        public Region reg = new Region();   // занимаемая им область в пространстве

        public Region GetForm_bug()
        {
            Point pt = new Point();
            Size st = new Size();
            pt.X = point.X;
            pt.Y = point.Y + size.Height / 4;
            st.Width = size.Width;
            st.Height = size.Height / 2;
            Rectangle rec = new Rectangle(pt, st);
            GraphicsPath path1 = new GraphicsPath();
            path1.AddEllipse(rec);
            Region reg = new Region(path1);
            rec.X = point.X + size.Width / 4;
            rec.Y = point.Y;
            rec.Width = size.Width / 2;
            rec.Height = size.Height;
            path1.AddEllipse(rec);
            reg.Union(path1);
            return reg;
        }

        public Boolean life = true;                  // НЛО жив (true) или мертв (false)
        public void New_bug(Form1 F, int rch)
        {
            Random rv = new Random(rch);
            point.X = rv.Next(10, Form1.ActiveForm.Width - 40);
            point.Y = rv.Next(10, Form1.ActiveForm.Height / 5);
            size.Width = rv.Next(20, 50);
            size.Height = size.Width * 2 / 3;
            veloX = rv.Next(7) - 3;
            veloY = rv.Next(3, 10);
            br = F.bc.New_br(rch);
            reg = GetForm_bug();
        } // задать свойства (параметры) НЛО

        public void Move_bug()
        {
            point.X += veloX;
            point.Y += veloY;
            reg = GetForm_bug();
        }// задать новое местоположение НЛО
    }
}
