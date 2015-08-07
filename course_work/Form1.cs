using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
using System.Threading;

namespace course_work
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        } 
        //построение графика Fi(fet)
        private void button1_Click(object sender, EventArgs e)
        {
            Special_functions comput = new Special_functions();
            Complex[] c;
            const int N = 30;
            c= new Complex[N];
            double[] fi;
            fi = new double[N];
            double[] Re_c, Im_c;
            Re_c = new double[N];
            Im_c = new double[N];
            fi[0] = 0;
            double h = 2 * Math.PI / Convert.ToDouble(N - 1);
            for (int i = 1; i < N; i++)
            {
                fi[i] = fi[i - 1] + h;
            }
            int m;
            m = Convert.ToInt32(textBox2.Text);                        
            for (int i = 0; i < N; i++)
            {
                c[i] = comput.fet(fi[i], m);
                Re_c[i] = c[i].Real;                
            }           

            this.chart1.Series["Series1"].Points.DataBindXY(fi, Re_c);
            
        }

        //построение графика tet
        private void button2_Click(object sender, EventArgs e)
        {
            Special_functions comput = new Special_functions();
            const int N = 11;
            int l, m;
            l = Convert.ToInt32(textBox1.Text);
            m = Convert.ToInt32(textBox2.Text);
            if (Math.Abs(m) > Math.Abs(l))
                MessageBox.Show("Квантовое магнитное число должно быть не больше орбитального к.ч.");
            else
            {
                double[] tet, f;
                tet = new double[N];
                f = new double[N];
                double h = Math.PI / Convert.ToDouble(N - 1);
                double x;                
                for (int i = 0; i < N; i++)
                {
                    x = i * h;
                    tet[i] = Math.Round(x, 2);
                    f[i] = comput.tetta(x, l, m);
                }
                this.chart1.Series["Series1"].Points.DataBindXY(tet, f);
            }

        }
        //построение графика R(r)
        private void button3_Click(object sender, EventArgs e)
        {
            Special_functions comput = new Special_functions();
            const int N = 20;
            int l, nr,n;
            l = Convert.ToInt32(textBox1.Text);
            n = Convert.ToInt32(textBox3.Text);
            nr = n - l - 1;
            double[] r, f;
            r = new double[N];
            f = new double[N];            
            double h = 30.0 / Convert.ToDouble(N - 1);
            double x;            
            for (int i = 0; i < N; i++)
            {
                x = i * h;
                r[i] = Math.Round(x, 1);
                f[i] = comput.rad_ur(x, nr, l);                              
            }
            this.chart1.Series["Series1"].Points.DataBindXY(r, f);
            
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            Special_functions comput = new Special_functions();
            string fileName = "res.txt"; 
            int i, j;
            const int N = 200;
            int l, m, n;
            l = Convert.ToInt32(textBox1.Text); //магнитное число
            m = Convert.ToInt32(textBox2.Text); //угловой момент
            n = Convert.ToInt32(textBox3.Text); //главное квантовое число
            double[,] fi,tet, psi;
            fi= new double[N,N];
            tet = new double [N,N];
            psi = new double[N,N];
            double r;
            r = Convert.ToDouble(textBox4.Text); //фиксируем r       
            double h = 2*Math.PI / Convert.ToDouble(N - 1); //определяем шаг для fi от 0 до 2Pi

            fi[0, 0] = 0.0;
            tet[0, 0] = 0.0;
            
            for (j = 1; j < N; j++)                            
                fi[0,j] = fi[0,j - 1] + h;

            h = Math.PI / Convert.ToDouble(N - 1); //определяем шаг для tetta от 0 до Pi
            for (i = 1; i < N; i++)                            
                tet[i, 0] = tet[i-1, 0 ] + h;
           
            //узлы для вычисление psi 
            for(i=1; i<N;i++)
                for (j = 0; j < N; j++)                
                    fi[i, j] = fi[0, j];
            for (i = 0; i < N; i++)
                for(j = 1; j < N; j++)  
                    tet[i, j] = tet[i, 0];

            double const_r = comput.rad_ur(r, n-l-1, l);

            for (i = 0; i < N; i++)
                for (j = 0; j < N; j++)
                    psi[i, j] = comput.fet(fi[i, j], m).Real * comput.tetta(tet[i, j], l, m) * const_r;
            
            //this.chart1.Series["Series2"].Points.DataBindXY(fi, psi);
            using (StreamWriter sw = new StreamWriter
                (new FileStream(fileName, FileMode.Create, FileAccess.Write)))
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-Us");

                sw.Write(n); sw.Write(' '); sw.Write(l); sw.Write(' ');
                sw.WriteLine(m);
                //запись fi
                for (i = 0; i < N; i++)
                {
                    for (j = 0; j < N; j++)
                    {
                        sw.Write(fi[i, j]); sw.Write(' ');                        
                    }
                    sw.WriteLine(' ');
                }                
                
                //запись tet
                for (i = 0; i < N; i++)
                {
                    for (j = 0; j < N; j++)
                    {
                        sw.Write(tet[i, j]); sw.Write(' ');                        
                    }
                    sw.WriteLine(' ');
                }
                
                //запись psi    
                for (i = 0; i < N; i++)
                {
                    for (j = 0; j < N; j++)
                    {
                        sw.Write(Math.Pow(psi[i, j],2)); sw.Write(' ');
                        sw.Write(' ');
                    }
                    sw.WriteLine(' ');
                }                
                textBox5.Text = "Данные были успешно записаны";  
            }            
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
        }

        

     
    }
}
