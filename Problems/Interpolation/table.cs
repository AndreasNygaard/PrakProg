using static System.Math;
using static System.Console;
public static class Table{
static void Main(){
	double dx=1.0/2;
	for(double x=-4;x<=4;x+=dx)
        System.Console.WriteLine("{0} {1}",x,Cos(x));
}}

