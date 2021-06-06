using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotnet_calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static int Transfer(string a)
        {
            double value = 0;

            int i = a.Length - 1;
            int count = 0;
            while (count != a.Length)
            {
                double tmp = Convert.ToDouble(a[i].ToString());
                double tmp1 = Math.Pow(8, count);
                value += tmp * Math.Pow(8, count);
                count++;
                i--;
            }
            return Convert.ToInt32(value);
        }

        private string Calc(string val1, string oper, string val2)
        {
            try
            {
                int a = Transfer(val1);
                int b = Transfer(val2);

                int res = 0;

                if (oper == "/")
                {
                    if (b != 0)
                    {
                        res = a / b;
                    }
                    else
                    {
                        return "Деление на ноль";
                    }
                }

                if (oper == "*")
                {
                    res = a * b;

                }
                if (oper == "+")
                {
                    res = a + b;
                }
                if (oper == "-")
                {
                    res = a - b;
                }

                return Convert.ToString(res, 8);
            }
            catch(Exception e1)
            {
                return "Ошибка";
            }

            return "Ошибка";

        }



        private string Calculate(string text)
        {
            List<string> list = new List<string>();
            string tmp = "";
            for (int i = 0; i < text.Length; i++)
            {
                if(text[i] == '+' || text[i] == '-' || text[i] == '*' || text[i] == '/')
                {
                    list.Add(tmp);
                    list.Add(text[i].ToString());
                    tmp = "";
                }
                else
                {
                    tmp += text[i];
                }

            }
            list.Add(tmp);

            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].ToString() == "*" || list[i].ToString() == "/")
                {
                    if (i == 0 || i == list.Count - 1)
                        return "Ошибка";

                    string res = Calc(list[i - 1].ToString(), list[i].ToString(), list[i + 1].ToString());

                    if (res == "Ошибка")
                        return "Ошибка";
                    list.RemoveAt(i - 1);
                    list.RemoveAt(i - 1);
                    list.RemoveAt(i - 1);
                    list.Insert(i - 1, res);
                    i--;
                }


            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ToString() == "+" || list[i].ToString() == "-")
                {
                    if (i == 0 || i == list.Count - 1)
                        return "Ошибка";

                    string res = Calc(list[i - 1].ToString(), list[i].ToString(), list[i + 1].ToString());

                    if (res == "Ошибка")
                        return "Ошибка";
                    list.RemoveAt(i - 1);
                    list.RemoveAt(i - 1);
                    list.RemoveAt(i - 1);
                    list.Insert(i - 1, res);
                    i = 0;
                }


            }

            return list[0].ToString();
        }



        private string Search(string text)
        {

            bool open = false;
            int open_index = -1;
            int close_index = -1;
            bool close = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    open = true;
                    open_index = i;
                }

                if (text[i] == ')')
                {
                    if (open  == true)
                    {
                        close = true;
                        close_index = i;
                        string res = Calculate(text.Substring(open_index + 1, close_index - open_index -1));
                        text = text.Remove(open_index, close_index - open_index + 1);
                        text = text.Insert(open_index, res);

                        i = 0;

                        open = false;
                        close = false;
                        open_index = i - 1;
                        close_index = i - 1;

                        
                    }
                    else
                        return "Неверно введено выражение: нет скобки (";
                }
            }
            if (open == true && close == false)
            {
                return "Неверно введено выражение: нет скобки )";
            }

            for (int i = 0; i < text.Length; i++)
            {
                if(text[i] == '(')
                {
                    Search(text);
                }
            }
            return Calculate(text);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = Search(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
