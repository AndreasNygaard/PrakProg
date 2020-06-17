using System;
using static System.Console;
using static System.Math;
using static System.Double;
class main{

static void Main(){

	WriteLine("______ Assignment A ______\n");
	WriteLine("Testing the neural network for fitting to a function:\n");
	Func<double,double> F_fit=delegate(double x){
		return Cos(5*x-1)*Exp(-x*x);
		};

	int n=5;
	var ann=new network(n);
	double a=-1,b=1;
	int nx=20;
	vector xs=new vector(nx);
	vector ys=new vector(nx);
	for(int i=0;i<nx;i++){
		xs[i]=a+(b-a)*i/(nx-1);
		ys[i]=F_fit(xs[i]);
		Error.Write($"{xs[i]} {ys[i]}\n");
		}
	Error.Write("\n\n");
	for(int i=0;i<ann.n;i++){
		ann.p[3*i+0]=a+(b-a)*i/(ann.n-1);
		ann.p[3*i+1]=1;
		ann.p[3*i+2]=1;
	}
	ann.p.print("Before training: p =");
	(int nsteps ,int ncalls) = ann.train(xs,ys);
	ann.p.print("After training:  p =");
	WriteLine($"Minimization steps: {nsteps}");
	WriteLine($"Function calls:     {ncalls}");
	for(double z=a;z<=b;z+=1.0/64)
		Error.Write($"{z} {ann.feed(z)}\n");
	Error.Write("\n\n");
	WriteLine("The fitted function can be seen in the figure Fit.svg.");
	Write("\n\n\n");


	WriteLine("______ Assignment B ______\n");

	WriteLine("We now use different feeders to get the derivative and antiderivative.\nThese can be seen in the figures Derivative.svg and Antiderivative.svg");
	for(double z=a;z<=b;z+=1.0/64)
		Error.Write($"{z} {ann.feedDeriv(z)}\n");
	Error.Write("\n\n");
	for(double z=a;z<=b;z+=1.0/64)
                Error.Write($"{z} {ann.feedInt(z)}\n");
        Error.Write("\n\n");

	}
}
