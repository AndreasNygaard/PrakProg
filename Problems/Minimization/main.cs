using System;
using static System.Console;
using static System.Math;
using static System.Double;
class main{

static void Main(){

	WriteLine("______ Assignment A ______\n");
	WriteLine("Testing minimization routine:\n");
	Func<vector,double> F_RVF=delegate(vector z){
		return Pow(1-z[0],2)+100*Pow(z[1]-Pow(z[0],2),2);
		};
	Func<vector,double> F_HF=delegate(vector z){
		return Pow(Pow(z[0],2)+z[1]-11,2)+Pow(z[0]+Pow(z[1],2)-7,2);
		};

	vector xstart = new vector(new double[]{0,0});
	double acc=1e-4;

	WriteLine("Rosenbrock's valley function:");
	(vector r_min, double n_steps) = minimize.qnewton(F_RVF,xstart,acc);
	vector grad = minimize.gradient(F_RVF,r_min);
	WriteLine($"Starting point: (x0, y0) = ({xstart[0]}, {xstart[1]})");
	WriteLine($"The minimum is at (x_min, y_min) = ({r_min[0],10:g3}, {r_min[1],10:g3})");
	WriteLine($"n_steps = {n_steps}");
	WriteLine($"Accuracy = {acc}");
	WriteLine($"|gradient| = {grad.norm()}");
	if(grad.norm()<acc)WriteLine("|gradient| < accuracy: test passed");
	else		WriteLine("|gradient| > accuracy: TEST FAILED");
	Write("\n");

	xstart = new vector(new double[]{5,3});

	WriteLine("Himmelblau's function:");
	(r_min, n_steps) = minimize.qnewton(F_HF,xstart,acc);
	grad = minimize.gradient(F_HF,r_min);
	WriteLine($"Starting point: (x0, y0) = ({xstart[0]}, {xstart[1]})");
	WriteLine($"The minimum is at (x_min, y_min) = ({r_min[0],10:g4}, {r_min[1],10:g4})");
	WriteLine($"n_steps = {n_steps}");
	WriteLine($"Accuracy = {acc}");
	WriteLine($"|gradient| = {grad.norm()}");
	if(grad.norm()<acc)WriteLine("|gradient| < accuracy: test passed");
	else            WriteLine("|gradient| > accuracy: TEST FAILED");
	Write("\n\n\n");




	WriteLine("______ Assignment B ______\n");
	WriteLine("Higgs data is as follows:\n");
	vector E   = new vector(new double[]{101,103,105,107,109,111,113,115,117,119,121,123,125,127,129,131,133,135,137,139,141,143,145,147,149,151,153,155,157,159});
	vector sig = new vector(new double[]{-0.25,-0.30,-0.15,-1.71,0.81,0.65,-0.91,0.91,0.96,-2.52,-1.01,2.01,4.83,4.58,1.26,1.01,-1.26,0.45,0.15,-0.91,-0.81,-1.41,1.36,0.50,-0.45,1.61,-2.21,-1.86,1.76,-0.50});
	vector err = new vector(new double[]{2.0,2.0,1.9,1.9,1.9,1.9,1.9,1.9,1.6,1.6,1.6,1.6,1.6,1.6,1.3,1.3,1.3,1.3,1.3,1.3,1.1,1.1,1.1,1.1,1.1,1.1,1.1,0.9,0.9,0.9});
	E.print("Energy in GeV:");
	sig.print("sigma:        ");
	err.print("Error:        ");
	Write("\n");
	Func<vector,double> Dev=delegate(vector z){
		int n=E.size;
		double sum=0;
		for(int i=0;i<n;i++){
			sum+=Pow(z[2]/(Pow(E[i]-z[0],2)+Pow(z[1],2)/4)-sig[i],2)/(Pow(err[i],2));
			}
		return sum;
		};

	xstart = new vector(new double[]{120,2,6});
	acc=1e-3;
	(r_min, n_steps) = minimize.qnewton(Dev,xstart,acc);
	WriteLine("We minimize the deviation to fit the data to the Breit-Wigner function:");
	grad = minimize.gradient(Dev,r_min);
        WriteLine($"Starting point: (m0, Γ0, A0) = ({xstart[0]}, {xstart[1]}, {xstart[2]})");
        WriteLine($"The minimum is at (m_min, Γ_min, A_min) = ({r_min[0]}, {r_min[1]}, {r_min[2]})");
        WriteLine($"n_steps = {n_steps}");
        WriteLine($"Accuracy = {acc}");
        WriteLine($"|gradient| = {grad.norm()}");
        if(grad.norm()<acc)WriteLine("|gradient| < accuracy: test passed");
        else            WriteLine("|gradient| > accuracy: TEST FAILED");
        Write("\n\n\n");
	for(int i=0;i<E.size;i++)
		Error.WriteLine($"{E[i]} {sig[i]} {err[i]}");
	Error.Write("\n\n");
	int N = 100;
	double dE = (161.0-99.0)/N;
	for(int i=0;i<N;i++)
		Error.WriteLine($"{99+i*dE} {r_min[2]/(Pow((99+i*dE)-r_min[0],2)+Pow(r_min[1],2)/4)}");





	WriteLine("______ Assignment C ______\n");
	WriteLine("Himmelblau's function has four minima, so we try using the downhill\nsimplex method to obtain some of these:\n");
	WriteLine("The four minima are at (x, y)=\n(3.0, 2.0),\n(-2.805, 3.131),\n(-3.779, -3.283),\n(3.584, -1.848)\n");

	int k=0;
	matrix simplex = new matrix(2,3);
	simplex[0][0]=6;
	simplex[0][1]=11;
	simplex[1][0]=-8;
	simplex[1][1]=-13;
	simplex[2][0]=4;
	simplex[2][1]=19;
	simplex.print("We use the following simplex:");
	(r_min, k) = minimize.downhill_simplex(F_HF,simplex,2,1e-6);
	WriteLine($"The solution here is (x, y) = ({r_min[0]}, {r_min[1]})");
	WriteLine($"nsteps = {k}\n");

	simplex[0][0]=20;
	simplex[0][1]=15;
	simplex[1][0]=8;
	simplex[1][1]=3;
	simplex[2][0]=4;
	simplex[2][1]=1;
        simplex.print("We now use another simplex:");
        (r_min, k) = minimize.downhill_simplex(F_HF,simplex,2,1e-6);
        WriteLine($"The solution here is (x, y) = ({r_min[0]}, {r_min[1]})");
        WriteLine($"nsteps = {k}\n");

	WriteLine("These points correspond to known minima, so we conclude that the\ndownhill simplex routine works succesfully.");
	}
}
