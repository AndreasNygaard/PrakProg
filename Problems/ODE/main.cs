using System;
using static System.Console;
using static System.Math;
using System.Collections;
using System.Collections.Generic;
class main{
static Func<double,vector,vector>
	F=delegate(double x, vector y){
		return new vector(y[1],-y[0]);
		};

static Func<double,vector,vector> S(double N, double Tc, double Tr){
	Func<double,vector,vector>
        	SIR_function=delegate(double x, vector y){
                	return new vector(-y[1]*y[0]/(N*Tc),y[1]*y[0]/(N*Tc)-y[1]/Tr,y[1]/Tr);
                	};
	return SIR_function;
	}

static void Main(){
	double a=0;
	vector ya = new vector(0,1);
	double b=2.25*PI;
	double h=0.1,acc=1e-3,eps=1e-3;
	List<double> t;
	List<vector> yt;
	(t, yt)=rk23.driver(F,a,ya,b,h,acc,eps);

	for(int i=0;i<t.Count;i++)
		Error.WriteLine($"{t[i]} {yt[i][0]} {yt[i][1]}");
	WriteLine("_____ Assignment A _____");
	WriteLine("The solution of u''=-u with u(0)=0 can be seen in the figure Sin.svg\nIt shows the single-derivative Cosine as well.");
	Write("\n\n\n");
	Error.Write("\n\n");



	// parameters for SIR model
	double N=5800000; // population
	double Tc=4; // time between contacts in days
	double Tr=10; // recovery time in days
	double t0=0;
	vector y0 = new vector(5799990,10,0);
	double t_end=300;
	(t, yt)=rk23.driver(S(N,Tc,Tr),t0,y0,t_end,h,acc,eps);

	for(int i=0;i<t.Count;i++)
		Error.WriteLine($"{t[i]} {yt[i][0]} {yt[i][1]} {yt[i][2]}");
	Error.Write("\n\n");
	WriteLine("_____ Assignment B _____");
	WriteLine($"We choose the parameters \nN={N}, Tc={Tc}, Tr={Tr} \nfor the Covid-19 epidemic in Denmark. \nThe results are seen in the figure SIR.svg");

	WriteLine("\nThe figures Tc_2.svg and Tc_6.svg shows the same \ncalculation, but for different times between contact.");
	WriteLine("\nAs we can see from the figures, the 'Infectious' \ncurve flattens as we increase Tc, because the virus \nis not spread as easily. We also end up with fewer \nremoved at the end, and thus fewer infected overall.");
	(t, yt)=rk23.driver(S(N,2,Tr),t0,y0,t_end,h,acc,eps);
	for(int i=0;i<t.Count;i++)
		Error.WriteLine($"{t[i]} {yt[i][0]} {yt[i][1]} {yt[i][2]}");
	Error.Write("\n\n");

	(t, yt)=rk23.driver(S(N,6,Tr),t0,y0,t_end,h,acc,eps);
	for(int i=0;i<t.Count;i++)
		Error.WriteLine($"{t[i]} {yt[i][0]} {yt[i][1]} {yt[i][2]}");


}
}
