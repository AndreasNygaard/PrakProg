using System;
using static System.Console;
using static System.Math;
using static System.Double;
using System.Collections;
using System.Collections.Generic;
class main{

static void Main(){
	double eps=1e-4;

	WriteLine("______ Assignment A ______\n");
	WriteLine("Testing rootfinding with simple functions:\n");
	Func<vector,vector> F_A1=delegate(vector z){
		vector r = new vector(1);
		r[0]=2*z[0]-1;
		return r;
		};
	Func<vector,vector> F_A2=delegate(vector z){
		vector r = new vector(1);
		r[0]=5*Pow(z[0],2)-7*z[0]+1;
		return r;
		};
	vector x_start= new vector(new double[]{1});
	vector root = rootfinding.newton(F_A1,x_start,eps);
	root.print("root of f(x)=2x-1 with starting point x0=1:");

	x_start= new vector(new double[]{1});
	root = rootfinding.newton(F_A2,x_start,eps);
	root.print("root of f(x)=5x^2-7x+1 with starting point x0=1:");

	x_start= new vector(new double[]{0});
	root = rootfinding.newton(F_A2,x_start,eps);
	root.print("root of f(x)=5x^2-7x+1 with starting point x0=0:");
	WriteLine("\nAll of these fit the analytical results.\n\n");


	WriteLine("Finding extremums of the 'Rosenbrock's valley function (RVF)':\n");
	WriteLine("We must look for roots of the gradient");
	Func<vector,vector> F_RVF=delegate(vector z){
		vector r = new vector(2);
		double x=z[0],y=z[1],b=10;
		r[0]=2*(1-x)*(-1)+b*2*(y-x*x)*(-1)*2*x;
		r[1]=b*2*(y-x*x);
		return r;
		};
	x_start= new vector(new double[]{0,0});
	root = rootfinding.newton(F_RVF,x_start,eps);
        root.print("root of gradient of RVF with starting point (x0,y0)=(0,0):");
	F_RVF(root).print("f(root)=");
	WriteLine($"eps            = {eps}");
	WriteLine($"f(root).norm() = {F_RVF(root).norm()}");
	if(F_RVF(root).norm()<eps)WriteLine("test passed");
	else                  WriteLine("test failed");
	Write("\n\n\n");





	WriteLine("______ Assignment B ______\n");

	WriteLine("Solutions with different r_max:\n");
	double e_2=schroedinger.SE_solve(2.0,eps);
	double e_3=schroedinger.SE_solve(3.0,eps);
	double e_5=schroedinger.SE_solve(5.0,eps);
	double e_10=schroedinger.SE_solve(10.0,eps);

	Error.WriteLine($"2 {e_2}\n3 {e_3}\n5 {e_5}\n10 {e_10}\n\n");

	WriteLine("\nThe figures Energy.svg and S-wave.svg shows the energy and s-wave \nfunction for the different r_max along with the exact solutions.\nWe see that we must have a rather high (~10) r_max to \nget the correct solution");
	Write("\n\n\n");


	WriteLine("______ Assignment C ______\n");

	WriteLine("We can see the convergence of the solution as a function of r_max \nin the plots Energy.svg and S-wave.svg. These have been made \nusing the simple boundary condition.\n");
	WriteLine("Solutions with different r_max and better boundary condition:\n");
	double e_1=schroedinger.SE_solve_improve(1.0,eps);
	e_2=schroedinger.SE_solve_improve(2.0,eps);
	e_3=schroedinger.SE_solve_improve(3.0,eps);
	e_5=schroedinger.SE_solve_improve(5.0,eps);

	Error.WriteLine($"1 {e_1}\n2 {e_2}\n3 {e_3}\n5 {e_5}\n\n");

	WriteLine("\nIn the figures Energy_improve.svg and S-wave_improve.svg, \nwe see the results with a better boundary condition. \nWe can see that we do not need as high an r_max as before, since we \ntrack the exact solution for any r_max until r_max is reached.");
        Write("\n\n\n");

	}
}
