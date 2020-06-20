using System;
using static System.Console;
using static System.Math;
using static System.Double;
using System.IO;
class main{

static void Main(){

	WriteLine("______ Exam problem 11 ______\n");

	Func<vector,double> F_HF=delegate(vector z){
		return Pow(Pow(z[0],2)+z[1]-11,2)+Pow(z[0]+Pow(z[1],2)-7,2);
		};
	Func<vector,double> F_5D=delegate(vector z){
		return z[0]*Pow(z[3],2)-10*z[2]*z[4]+5*Pow(z[1],2);
		};
	WriteLine("We test the pseudo- and quasi-random methods on the following integral:");
	WriteLine("Himmelblau's function, ∫[0,6]dx ∫[0,6]dy (x^2+y-11)^2 + (x+y^2-7)^2\n");
	int N = 5000;
	vector a = new vector(new double[]{0,0});
	vector b = new vector(new double[]{6,6});
	double exact = 56952.0/5.0;
	WriteLine($"With regular Monte Carlo (Pseudo-random)\n");
	(double integral, double error) = mc.plainmc(F_HF,a,b,N,print_points:true);
	WriteLine($"Number of points N = {N}");
	WriteLine($"The integral is estimated to {integral}");
	WriteLine($"Exact integral is {exact}");
	WriteLine($"The estimated error is {error}");
	WriteLine($"The exact error is {Abs(exact-integral)}");
	Write("\n");

	StreamWriter pointWriter = new StreamWriter("sample_points.txt", true);
	pointWriter.Write("\n\n");
	pointWriter.Close();

	WriteLine($"With the Lattice sequence (Quasi-random)\n");
	(integral, error) = mc.plainmc(F_HF,a,b,N,print_points:true,Lattice:true);
	WriteLine($"Number of points N = {N}");
	WriteLine($"The integral is estimated to {integral}");
	WriteLine($"Exact integral is {exact}");
	WriteLine($"The exact error is {Abs(exact-integral)}");
	Write("\n");

	pointWriter = new StreamWriter("sample_points.txt", true);
	pointWriter.Write("\n\n");
	pointWriter.Close();

	WriteLine($"With the Halton sequence (Quasi-random)\n");
	(integral, error) = mc.plainmc(F_HF,a,b,N,print_points:true,Halton:true);
	WriteLine($"Number of points N = {N}");
	WriteLine($"The integral is estimated to {integral}");
	WriteLine($"Exact integral is {exact}");
	WriteLine($"The exact error is {Abs(exact-integral)}");
	Write("\n");
	WriteLine("The estimated errors of the quasi-random methods are no good, since the");
	WriteLine("central limit theorem doesn't apply when the points are not statistically");
	WriteLine("independent. Instead we use the difference in the integral estimates of");
	WriteLine("the two quasi-random sequences Lattice and Halton as an error estimate.\n");

	WriteLine("The figure Sample.svg shows the sampling points of these three methods");
	WriteLine("when calculating the integral of the Himmelblau's function. The lattice");
	WriteLine("is very clear in the Lattice sequence, and the Halton sequence gives a");
	WriteLine("much more evenly distributed sampling than the pseudo-random Monte Carlo.");
	Write("\n\n\n");



	WriteLine("We will use the Himmelblau's function to test the error as a function of N.\n");
	int n=80;
	double true_error;
	a = new vector(new double[]{0,0});
	b = new vector(new double[]{6,6});
	exact = 56952.0/5.0;
	for(int i=2;i<n+2;i++){
		int Ni = i*i*i*11;
		(integral, error) = mc.plainmc(F_HF,a,b,Ni);
		true_error = Abs(integral-exact);
		Error.WriteLine($"{Ni} {error} {true_error}");
		}
	Error.Write("\n\n");

        for(int i=2;i<n+2;i++){
                int Ni = i*i*i*11;
                (integral, error) = mc.plainmc(F_HF,a,b,Ni,Quasi:true);
                true_error = Abs(integral-exact);
                Error.WriteLine($"{Ni} {error} {true_error}");
                }
	Error.Write("\n\n");
	WriteLine("The result is seen in the figure Convergence2D.svg.");
	Write("\n\n");


	WriteLine("We will also check the convergence of a 5D integral in the region [0,2] for\nall dimensions:\nf(x,y,z,p,q) = x·p^2 -10·z·q + 5·y^2\n\nThe exact integral is then ∫ f dV = -64\n");
	n=100;
	a = new vector(new double[]{0,0,0,0,0});
	b = new vector(new double[]{2,2,2,2,2});
	exact = -64.0;
	for(int i=1;i<n+1;i++){
		int Ni = i*i*i*15;
		(integral, error) = mc.plainmc(F_5D,a,b,Ni);
		true_error = Abs(integral-exact);
		Error.WriteLine($"{Ni} {error} {true_error}");
		}
	Error.Write("\n\n");

	for(int i=1;i<n+1;i++){
		int Ni = i*i*i*15;
		(integral, error) = mc.plainmc(F_5D,a,b,Ni,Quasi:true);
		true_error = Abs(integral-exact);
		Error.WriteLine($"{Ni} {error} {true_error}");
		}

	WriteLine("The result is seen in the figure Convergence5D.svg.");
	Write("\n\n");

	WriteLine("We clearly see that the error of the quasi-random method falls faster with respect");
	WriteLine("to N than that of the pseudo-random method which falls as O(1/√N), both in 2D and");
	WriteLine("5D. According to the Wikipedia page 'Quasi-Monte Carlo method', an upper bound on");
	WriteLine("the error estimation should fall as O(log(N)^s/N), where s is the dimension. In the");
	WriteLine("2D case, it is not clear from the figure whether the quasi-random error falls as");
	WriteLine("O(log(N)^2/N) or as O(log(N)/N), but we can however see that it doesn't fall slower");
	WriteLine("than O(log(N)^2/N) and it doesn't falls as fast as just O(1/N).");
	WriteLine("When examining the 5D case, we see that the O(log(N)^5/N) behavior might hold for");
	WriteLine("smaller N (still large enough to give a reasonable result though), but for very");
	WriteLine("large N it seems to fall down to an O(log(N)^2/N) behavior or at least faster than");
	WriteLine("the upper bound. We can once again think of the O(1/N) behavior as a lower bound,");
	WriteLine("which brings the conclusion:\n");
	WriteLine("For quasi-random methods, the error as a function of N falls somewhere between the");
	WriteLine("behaviors O(1/N) (lower bound) and O(log(N)^s/N) (upper bound) where s is the ");
	WriteLine("dimension, thus making it faster converging than the pseudo-random method with");
	WriteLine("convergence rate O(1/√N).");
	}
}

