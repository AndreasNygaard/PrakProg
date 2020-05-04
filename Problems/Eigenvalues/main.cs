using System;
using static System.Console;
using static System.Math;
class main{
static void Main(){
	vector t0 = new vector(new double[]{1  ,2  ,3 ,4 ,6 ,9   ,10  ,13  ,15  });
	vector y0 = new vector(new double[]{117,100,88,72,53,29.5,25.2,15.2,11.1});
	vector dy0 = 0.05*y0;
	for(int i=0;i<t0.size;i++)Error.WriteLine($"{t0[i]} {y0[i]} {dy0[i]}");
	Error.Write("\n\n");
	int n=t0.size;
	vector t=new vector(n);
	vector y=new vector(n);
	vector dy=new vector(n);
	for(int i=0;i<n;i++){
		t[i]=t0[i];
		y[i]=Log(y0[i]);
		dy[i]=dy0[i]/y0[i];
		}
	var f = new Func<double,double>[]{x=>1,x=>-x};
	(vector c, vector dc, matrix sigma) = ols_fit.fit_coeff(t,y,dy,f);
	// Write function non-logarithmically
	double a = Exp(c[0]);
	double k = c[1];
	double t_half = Log(2)/k;
	double da = dc[0]*Exp(c[0]); // using error propagation
	double dk = dc[1];
	double dt_half = Log(2)*dk/Pow(k,2);


	WriteLine("____Assignment A____");
	t0.print("Time t:");
	y0.print("Activity y:");
	dy0.print("Errors dy:");
	int N = 100; double step = (t0[t0.size-1]-t[0])/N;
	for(int i=0;i<=N;i++)Error.WriteLine($"{t0[0]+i*step} {a*Exp(-k*(t0[0]+i*step))}");
	Error.Write("\n\n");
	WriteLine($"\nFitted exponential function:\ny(t)={a:f1}·exp(-{k:f3}·t)\n\nA plot of this fit is seen in A.svg");
	WriteLine($"\n\nThe half-life of ThX is found to be {t_half} days\n\n\n");


	WriteLine("____Assignment B____");
	sigma.print("Covariance matrix:");
	WriteLine($"\nFitted exponential function with errors on coefficients:\ny(t)=[{a:f1} ± {da:f3}]·exp(-[{k:f3} ± {dk:f4}]·t)");
	WriteLine($"\n\nErrors on fit-coefficients in y(t)=a·exp(-k·t):\n\na = ({a} ± {da}) % activity\nk = ({k} ± {dk}) days^(-1)");
	WriteLine($"\nHalf-life of ThX with uncertainty is ({t_half:f3} ± {dt_half:f5}) days");
	WriteLine("\nThe modern value of the half-life is 3.66 days, and this is not within our uncertainty\n\n\n");


	WriteLine("____Assignment C____");
	for(int i=0;i<=N;i++)Error.WriteLine($"{t0[0]+i*step} {(a+da)*Exp(-k*(t0[0]+i*step))}");
	Error.Write("\n\n");
	for(int i=0;i<=N;i++)Error.WriteLine($"{t0[0]+i*step} {(a-da)*Exp(-k*(t0[0]+i*step))}");
	Error.Write("\n\n");
	for(int i=0;i<=N;i++)Error.WriteLine($"{t0[0]+i*step} {a*Exp(-(k+dk)*(t0[0]+i*step))}");
	Error.Write("\n\n");
	for(int i=0;i<=N;i++)Error.WriteLine($"{t0[0]+i*step} {a*Exp(-(k-dk)*(t0[0]+i*step))}");
	Error.Write("\n\n");
	WriteLine("We plot four additional fits, each corrected with an uncertainty of a coefficient");
	WriteLine("\nThese fits are as follows:");
	WriteLine($"y(t)=[{a:f1} + {da:f3}]·exp(-{k:f3}·t)");
	WriteLine($"y(t)=[{a:f1} - {da:f3}]·exp(-{k:f3}·t)");
	WriteLine($"y(t)={a:f1}·exp(-[{k:f3} + {dk:f4}]·t)");
	WriteLine($"y(t)={a:f1}·exp(-[{k:f3} - {dk:f4}]·t)");
	WriteLine("\nA plot of these fits are seen in C.svg");
}
}
