using System;
using static System.Console;
using static System.Math;
using static System.Double;
class main{

static void Main(){

	WriteLine("______ Assignment A ______\n");
	WriteLine("Testing Monte Carlo integrator:\n");
	Func<vector,double> F_sing=delegate(vector z){
		return 1/((1-Cos(z[0])*Cos(z[1])*Cos(z[2]))*Pow(PI,3));
		};
	Func<vector,double> F_HF=delegate(vector z){
		return Pow(Pow(z[0],2)+z[1]-11,2)+Pow(z[0]+Pow(z[1],2)-7,2);
		};
	Func<vector,double> F_poly=delegate(vector z){
		return 4*Pow(z[0],2)-6*z[0]+8;
		};

	vector a = new vector(new double[]{0});
	vector b = new vector(new double[]{8});

	WriteLine("Second-degree polynomial, ∫[0,8]dx 4*x^2 - 6*x + 8:\n");
	int N = 1000;
	double exact = 1664.0/3.0;
	(double integral, double error) = mc.plainmc(F_poly,a,b,N,false);
	WriteLine($"Number of points N = {N}");
	WriteLine($"The integral is estimated to {integral}");
	WriteLine($"Exact integral is {exact}");
	WriteLine($"The estimated error is {error}");
	WriteLine($"The exact error is {Abs(exact-integral)}");
	Write("\n");

	N = 100000;
	(integral, error) = mc.plainmc(F_poly,a,b,N,false);
	WriteLine($"Number of points N = {N}");
	WriteLine($"The integral is estimated to {integral}");
	WriteLine($"Exact integral is {exact}");
	WriteLine($"The estimated error is {error}");
	WriteLine($"The exact error is {Abs(exact-integral)}");
	Write("\n\n");

	WriteLine("Himmelblau's function, ∫[0,6]dx ∫[0,6]dy (x^2+y-11)^2 + (x+y^2-7)^2:\n");
	N = 100000;
	a = new vector(new double[]{0,0});
	b = new vector(new double[]{6,6});
	exact = 56952.0/5.0;
	(integral, error) = mc.plainmc(F_HF,a,b,N,false);
	WriteLine($"Number of points N = {N}");
	WriteLine($"The integral is estimated to {integral}");
	WriteLine($"Exact integral is {exact}");
	WriteLine($"The estimated error is {error}");
	WriteLine($"The exact error is {Abs(exact-integral)}");
	Write("\n");
	WriteLine("We see that we get a better result using more points, and the \nresult converges towards the exact result.\n\n");

	WriteLine("Difficult singular integral, ∫[0,π]dx ∫[0,π]dy ∫[0,π]dz 1/[(1-cos(x)*cos(y)*cos(z)) * π^3]:\n");
        N = 500000;
        a = new vector(new double[]{0,0,0});
        b = new vector(new double[]{PI,PI,PI});
        exact = 1.3932039296856768591842462603255;
        (integral, error) = mc.plainmc(F_sing,a,b,N,false);
        WriteLine($"Number of points N = {N}");
        WriteLine($"The integral is estimated to {integral}");
        WriteLine($"Exact integral is {exact}");
        WriteLine($"The estimated error is {error}");
        WriteLine($"The exact error is {Abs(exact-integral)}");
        Write("\n");
        Write("\n\n\n");



	WriteLine("______ Assignment B ______\n");
	WriteLine("We will use the Himmelblau's function to test the error as a function of N.\n");
	int n=30;
	double true_error;
	a = new vector(new double[]{0,0});
	b = new vector(new double[]{6,6});
	exact = 56952.0/5.0;
	for(int i=5;i<n+5;i++){
		int Ni = i*i*5;
		(integral, error) = mc.plainmc(F_HF,a,b,Ni,false);
		true_error = Abs(integral-exact);
		Error.WriteLine($"{Ni} {error} {true_error}");
		}
	WriteLine("The result is seen in the figure Error.svg. We clearly see that the estimated\nerror drops as 1/√N, and the actual error does as well to some extend.");
	Write("\n\n\n");


	WriteLine("______ Assignment C ______\n");
	WriteLine("We estimate the integral of the difficult singular function using recursive \nstratified sampling\n");
/*	a = new vector(new double[]{0,0,0});
	b = new vector(new double[]{PI,PI,PI});
	N = 500000;
	exact = 1.3932039296856768591842462603255;
	(integral,error) = mc.stratmc1(F_sing,a,b,N,false);
	WriteLine($"Number of points N = {N}");
        WriteLine($"The integral is estimated to {integral}");
        WriteLine($"Exact integral is {exact}");
        WriteLine($"The estimated error is {error}");
        WriteLine($"The exact error is {Abs(exact-integral)}");
        Write("\n");*/

	double[] aa = new double[]{0,0,0};
	double[] bb = new double[]{PI,PI,PI};
	N = 500000;
	exact = 1.3932039296856768591842462603255;
	double[] res = mc.stratmc(F_sing,aa,bb,N,false);
	WriteLine($"Number of points N = {N}");
	WriteLine($"The integral is estimated to {res[0]}");
	WriteLine($"Exact integral is {exact}");
	WriteLine($"The estimated error is {res[1]}");
	WriteLine($"The exact error is {Abs(exact-res[0])}");
	Write("\n\n");

	Func<vector, double> f = delegate(vector x){
		// This function is on a simple rectangular volume
		if(x[0]*x[0]+x[1]*x[1]<=0.9){
			return 1;
			}
		else{
			return 0;
			}
		};

	N=100000;
	aa = new double[]{-1,-1};
        bb = new double[]{1,1};
	res = mc.stratmc(f,aa,bb,N,true);
	WriteLine("We estimate the area of circle with radius 0.9 by integrating \nover a constant circular function.\n");
	WriteLine($"The area is estimated to {res[0]}\n");
	WriteLine("The stratified points can be seen in the figure Strat.svg");

	}
}

