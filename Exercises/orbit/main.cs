using System;
using System.IO;
using static System.Console;
using static System.Math;
using static System.Double;
using System.Collections;
using System.Collections.Generic;

class main{
public static void Main(){

	double a = 0;
	double b = 3;
	vector ya = new vector(new double[]{0.5});
	List<double> xlist = new List<double>();
	List<vector> ylist = new List<vector>();
	vector res = ode.rk23(F,a,ya,b, xlist:xlist, ylist:ylist);
	Write($"{res}");
	int n = xlist.Count;
	StreamWriter pointWriter = new StreamWriter("data.txt", true);
	for(int i=0;i<n;i++)
		pointWriter.WriteLine($"{xlist[i]} {ylist[i][0]}");
	pointWriter.Close();

	}

static Func<double,vector,vector>
	F=delegate(double x, vector y){
		return new vector(y[0]*(1-y[0]));
		};
}
