using System;
using static System.Console;
using static System.Math;
using static System.Double;
class main{

static void Test(double Q, double exact, double acc, double eps, int calls){
	double aerr=Abs(Q-exact),tol=acc+eps*Abs(Q);
	WriteLine($"Exact result: {exact} \nacc: {acc} \neps: {eps} \ntol: {tol} \nerr: {aerr} \ncalls: {calls}");
	if(aerr<tol)WriteLine("err<tol: test passed");
	else WriteLine("err>tol: TEST FAILED!");
	}

static void Main(){
	int calls=0;

	Func<double,double> F_A1=delegate(double x){calls++;return Sqrt(x);};
	Func<double,double> F_A2=delegate(double x){calls++;return 4*Sqrt(1-Pow(x,2));};

	double a=0.0;
	double b=1.0;

	WriteLine("______ Assignment A ______\n");
	WriteLine("Testing integrals with adaptive integrator:\n");

	(double Q_A1, double err_A1) = integrate.adaptive(F_A1,a,b,1e-7,1e-5);
	WriteLine($"∫[0,1]dx √(x) = {Q_A1}");
	Test(Q_A1,2.0/3.0,1e-7,1e-5,calls);
	WriteLine($"Error estimate: {err_A1}\n");

	calls=0;
	(double Q_A2, double err_A2) = integrate.adaptive(F_A2,a,b,1e-7,1e-5);
	WriteLine($"∫[0,1]dx 4√(1-x^2) = {Q_A2}");
	Test(Q_A2,PI,1e-7,1e-5,calls);
	WriteLine($"Error estimate: {err_A2}\n");
	Write("\n\n");





	WriteLine("______ Assignment B ______\n");
	WriteLine("Testing integrals with divergencies with Clenshaw-Curtis transformation:\n");
	Func<double,double> F_B1=delegate(double x){calls++;return 1/(Sqrt(x));};
	Func<double,double> F_B2=delegate(double x){calls++;return Log(x)/(Sqrt(x));};

	calls=0;
	(double Q_B1a, double err_B1a) = integrate.adaptive(F_B1,a,b,1e-4,1e-4);
	WriteLine("WITHOUT Clenshaw-Curtis transformation");
	WriteLine($"∫[0,1]dx 1/√(x) = {Q_B1a}");
	Test(Q_B1a,2,1e-4,1e-4,calls);
	WriteLine($"Error estimate: {err_B1a}\n");

	calls=0;
	(double Q_B1cc, double err_B1cc) = integrate.Clenshaw_Curtis(F_B1,a,b,1e-4,1e-4);
	WriteLine("WITH Clenshaw-Curtis transformation");
	WriteLine($"∫[0,1]dx 1/√(x) = {Q_B1cc}");
	Test(Q_B1cc,2,1e-4,1e-4,calls);
	WriteLine($"Error estimate: {err_B1cc}\n");
	Write("\n\n");

	calls=0;
	(double Q_B2a, double err_B2a) = integrate.adaptive(F_B2,a,b,1e-4,1e-4);
	WriteLine("WITHOUT Clenshaw-Curtis transformation");
	WriteLine($"∫[0,1]dx ln(x)/√(x) = {Q_B2a}");
	Test(Q_B2a,-4,1e-4,1e-4,calls);
	WriteLine($"Error estimate: {err_B2a}\n");

	calls=0;
	(double Q_B2cc, double err_B2cc) = integrate.Clenshaw_Curtis(F_B2,a,b,1e-4,1e-4);
	WriteLine("WITH Clenshaw-Curtis transformation");
	WriteLine($"∫[0,1]dx ln(x)/√(x) = {Q_B2cc}");
	Test(Q_B2cc,-4,1e-4,1e-4,calls);
	WriteLine($"Error estimate: {err_B2cc}\n");
	Write("\n\n");


	WriteLine("Calculating ∫[0,1]dx 4√(1-x^2) with and without Clenshaw-Curtis \nas well as with the o8av routine\n");
	calls=0;
	(double Q_B3a, double err_B3a) = integrate.adaptive(F_A2,a,b,1e-7,1e-7);
	WriteLine("WITHOUT Clenshaw-Curtis transformation");
	WriteLine($"∫[0,1]dx ln(x)/√(x) = {Q_B3a}");
	Test(Q_B3a,PI,1e-7,1e-7,calls);
	WriteLine($"Error estimate: {err_B3a}\n");

	calls=0;
	(double Q_B3cc, double err_B3cc) = integrate.Clenshaw_Curtis(F_A2,a,b,1e-7,1e-7);
	WriteLine("WITH Clenshaw-Curtis transformation");
	WriteLine($"∫[0,1]dx ln(x)/√(x) = {Q_B3cc}");
	Test(Q_B3cc,PI,1e-7,1e-7,calls);
	WriteLine($"Error estimate: {err_B3cc}\n");

	calls=0;
	double Q_B3o8 = integrate.o8av(F_A2,a,b,1e-7,1e-7);
	WriteLine("Using o8av");
	WriteLine($"∫[0,1]dx ln(x)/√(x) = {Q_B3o8}");
	Test(Q_B3o8,PI,1e-7,1e-7,calls);
	Write("\n");
	WriteLine("We see that given the same accuracies, the Clenshaw-Curtis is the closest to the exact\nresult, but it also has the most calls by far. The o8av routine has almost 2 orders of \nmagnitude less calls and performs almost as well, while the adaptive routine without \nClenshaw-Curtis has around 2/3 of the calls of Clenshaw-Curtis and has around the same \naccuracy as the o8av routine.");
	Write("\n\n");





        WriteLine("______ Assignment C ______\n");
	WriteLine("Testing my integrator with infinite limits\n");
	Func<double,double> F_C1=delegate(double x){calls++;return Exp(-x);};
	Func<double,double> F_C2=delegate(double x){calls++;return Exp(-Pow((x-3),2));};

	WriteLine("Calculating ∫[0,∞]dx exp(-x) with my own integrator and with the o8av routine\n");
	calls=0;
	(double Q_C1a, double err_C1a) = integrate.adapt_inf(F_C1,0,PositiveInfinity,1e-4,1e-4);
	WriteLine("My own adaptive integrator");
	WriteLine($"∫[0,∞]dx exp(-x) = {Q_C1a}");
	Test(Q_C1a,1,1e-4,1e-4,calls);
	WriteLine($"Error estimate: {err_C1a}\n");

	calls=0;
	double Q_C1o8 = integrate.o8av(F_C1,0,PositiveInfinity,1e-4,1e-4);
	WriteLine("Using o8av");
	WriteLine($"∫[0,∞]dx exp(-x) = {Q_C1o8}");
	Test(Q_C1o8,1,1e-4,1e-4,calls);
	Write("\n\n");

	WriteLine("Calculating ∫[-∞,∞]dx exp(-(x-3)^2) with my own integrator and with the o8av routine\n");
	calls=0;
	(double Q_C2a, double err_C2a) = integrate.adapt_inf(F_C2,NegativeInfinity,PositiveInfinity,1e-3,1e-3);
	WriteLine("My own adaptive integrator");
	WriteLine($"∫[-∞,∞]dx exp(-(x-3)^2) = {Q_C2a}");
	Test(Q_C2a,Sqrt(PI),1e-3,1e-3,calls);
	WriteLine($"Error estimate: {err_C2a}\n");

	calls=0;
	double Q_C2o8 = integrate.o8av(F_C2,NegativeInfinity,PositiveInfinity,1e-3,1e-3);
	WriteLine("Using o8av");
	WriteLine($"∫[-∞,∞]dx exp(-(x-3)^2) = {Q_C2o8}");
	Test(Q_C2o8,Sqrt(PI),1e-3,1e-3,calls);
	Write("\n");

	}
}
