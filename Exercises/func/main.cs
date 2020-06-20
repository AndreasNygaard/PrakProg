using System;
using static System.Console;
using static System.Math;
using static System.Double;
class main{
public static void Main(){

	double result = quad.o8av(f1,0,1,1e-7,1e-7);
	WriteLine($"∫[0,1]dx ln(x)/√x = {result}");
	WriteLine("Analytic result is -4\n");

	result = quad.o8av(f2,NegativeInfinity,PositiveInfinity,1e-7,1e-7);
	WriteLine($"∫[-∞,∞]dx exp(-x^2) = {result}");
	WriteLine($"Analytic result is √π = {Sqrt(PI)}\n");

	for(int i=0;i<8;i++){
		result = quad.o8av(F3(i/2.0),0,1,1e-7,1e-7);
		WriteLine($"∫[0,1]dx ln(1/x)^{i/2.0,1:f1} = {result}");
		WriteLine($"Analytic result is Γ({i/2.0,1:f1}+1) = {math.gamma(i/2.0+1),1:f4}\n");
		}
	}

public static Func<double,double> f1=delegate(double x){
	return Log(x)/Sqrt(x);
	};

public static Func<double,double> f2=delegate(double x){
	return Exp(-x*x);
	};

public static Func<vector,double> f3=delegate(vector z){
	return Pow(Log(1.0/z[0]),z[1]);
	};

public static Func<double,double> F3(double p){
	Func<double,double> f = (x) => Pow(Log(1.0/x),p);
	return f;
	}

}
