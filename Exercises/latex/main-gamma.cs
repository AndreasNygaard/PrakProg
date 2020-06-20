using System;
using static System.Double;
using static System.Math;
static class main{
static void Main(){
	double eps=1.0/256,dx=1.0/128;
	double result;
	for(double x=-4+eps;x<=6+eps;x+=dx){
		if(x<0){
			result = PI/Sin(PI*x)/quad.o8av(F(1-x),0,PositiveInfinity,1e-7,1e-7);
			}
		else{
			result = quad.o8av(F(x),0,PositiveInfinity,1e-7,1e-7);
			}
		System.Console.WriteLine("{0} {1}",x,result);
		}
	for(double x=1;x<=8;x++){
		System.Console.Error.WriteLine("gamma : {0,5} {1,16:f8}",x,math.gamma(x));
		}
	}

public static Func<double,double> F(double z){
	Func<double,double> f = (x) => Pow(x,z-1)*Exp(-x);
	return f;

	}
}
