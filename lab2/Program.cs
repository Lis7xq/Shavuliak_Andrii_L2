using System;
using System.IO;
using System.Text.Json;

namespace lab2
{
    class Program
    {
        class Fract   //  a/b
        {
            public int A { get; set; }
            public int B { get; set; }

            public void Reduce()
            {
                int x = Math.Abs(A);
                int y = Math.Abs(B);
                while (x != 0 && y != 0)
                {
                    if (x > y) x = x % y;
                    else y = y % x;
                }
                A /= (x + y);
                B /= (x + y);
                if (B < 0)
                {
                    A *= -1;
                    B *= -1;
                }
            }
            public Fract Sum(Fract d2)                                       //       a      d2.a
            {                                                                //      ---  +  ----                                      
                Fract Res = new Fract(A * d2.B + d2.A * B, B * d2.B);        //       b      d2.b
                Res.Reduce();
                return Res;
            }
            public Fract Diff(Fract d2)                                                      //       a      d2.a
            {                                                                                //      ---  -  ----                                      
                Fract Res = new Fract(A * d2.B - d2.A * B, B * d2.B);                        //       b      d2.b
                Res.Reduce();
                return Res;
            }
            public Fract Product(Fract d2)
            {
                Fract Res = new Fract(A * d2.A, B * d2.B);
                Res.Reduce();
                return Res;
            }
            public Fract Div(Fract d2)
            {
                Fract Res = new Fract(A * d2.B, B * d2.A);
                Res.Reduce();
                return Res;
            }
            public bool Equal(Fract d2)
            {
                if (this.Diff(d2).A == 0)
                {
                    return true;
                }
                else return false;
            }
            public bool ABOBA() // Чи правильний
            {
                if (A < B) return true;
                else return false;
            }
            public void Print()
            {
                Console.WriteLine($"{A}/{B}");
            }
            public Fract(int a,int b)
            {
                A = a;
                B = b;
            }
            public void Save()
            {
                string Js = JsonSerializer.Serialize(this);
                Console.WriteLine(Js); 
                File.WriteAllText("fract.json", Js);
            }
        }
        static void Main(string[] args)
        {
            Fract f1 = new Fract(-2,4);

            f1.Reduce();
            f1.Print();

            Fract f2 = new Fract(1,4);

            Fract f3 = f1.Sum(f2);
            f3.Print();

            f1 = f3.Diff(f2);
            f1.Print();

            f3 = f1.Product(f2);
            f3.Print();

            f3 = f3.Div(f1);
            f3.Print();

            if (f3.Equal(f2))
            {
                Console.WriteLine("Equal");
            }

            f1.Print();
            if (f1.ABOBA())
                Console.WriteLine("f1-Правильний");

            Fract FF = new Fract(4,8);
            FF.Save();
            FF = Open();
            FF.Print();


            /*bool Equal(Fract d1, Fract d2)
            {
                if (d1.Diff(d2).a == 0)
                {
                    return true;
                }
                else return false;
            }*/
            Fract Open()
            {
                var Js = File.ReadAllText("fract.json");
                Fract FF = JsonSerializer.Deserialize<Fract>(Js);
                return FF;
            }
        }

    }
}
