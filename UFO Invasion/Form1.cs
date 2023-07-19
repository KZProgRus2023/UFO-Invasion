using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UFO_Invasion;


namespace UFO_Invasion
{
    public Player player = new Player();     // Игрок, который сбивает НЛО (объект)
    public Boolean laser = false;                    // Его оружие — бластер
    public Bitmap imageP;                               //  Изображения игрока
    public int Result = 0;                                   // Количество сбитых НЛО (счет игры)
    public Graphics g;                                       // холст для битвы
    public BrushColor bc = new BrushColor();       // набор кистей и цветов
    public Enemies nlo = new Enemies();                // Все НЛО

    public partial class Form1 : Form
    {
    public const int N_max = 200;    // Максимальное количество НЛО на экране
        internal object imageP;
        internal object bc;
        internal object g;
        internal bool laser;
        private object nlo;

        public Form1()
        {
            InitializeComponent();
        }

        public static int N_max { get; internal set; }
        public int Result { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            nlo.k_generation = 0;
            nlo.Enemy(this);
            timer1.Start();
            timer2.Start();
            button3.Enabled = true;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (laser)
            {
                laser = false;
                button2.Text = "Включить Лазер";
            }
            else
            {
                laser = true;
                button2.Text = "Отключить Лазер";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            imageP = new Bitmap(imageList1.Images[1], 100, 100);
            int procent = Result * 100 / (nlo.Delta_N * nlo.N_generation);
            string msg = "Подбито " + Result.ToString() + " НЛО, " + procent.ToString() + "% результат";
            MessageBox.Show(msg, "Ваш результат", MessageBoxButtons.OK);
            player.Show_player(this, 50, 50);
            nlo.N = 0;
            button1.Enabled = true;
            Result = 0;
            textBox1.Text = Result.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(BackColor);
            Result = Result + nlo.Select_bugs();
            nlo.Show_bugs(this);
            textBox1.Text = Result.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            nlo.k_generation++;
            timer2.Interval -= 100;
            if (nlo.k_generation < nlo.N_generation)
                nlo.Enemy(this);
            else
                timer2.Stop();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            player.Show_player(this, e.X, e.Y);
            if (laser)
                g.DrawLine(player.laser_pen, player.point.X + player.size.Width / 2, player.point.Y, player.point.X + player.size.Width / 2, 0);
            nlo.Killed_bugs(this, e.X, e.Y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();          // инициализация холста
            BackColor = bc.FonColor;            // цвет фона
            imageP = new Bitmap(imageList1.Images[0], 100, 100);
            player.New_player(this);            // инициализация игрока
            nlo = new Enemies();                // инициализация противника
            nlo.New_Enemies(this);              // инициализация НЛО как объектов
        }
    }
}
