using System;
using static System.Console;
using static System.Math;
using System.Diagnostics;
class test{
static void Main(){
	int sweeps, rotations_1, rotations_n, n;
	double time;
	matrix D_c,D_1,D_n,V;
	for(int k=0;k<5;k++){
		n=73+k*5;
		var rnd = new Random(1);
		var A = new matrix(n,n);
		for(int i=0;i<n;i++)for(int j=i;j<n;j++){
			A[i,j]=2*(rnd.NextDouble()-0.59);
			}

		Stopwatch stopwatch = Stopwatch.StartNew(); // Stopwatch
		(D_c,V,sweeps)=diag.cyclic(A);
		stopwatch.Stop();

		time=stopwatch.ElapsedMilliseconds*0.001;

		Error.WriteLine($"{n} {time}");
		WriteLine($"{n} {D_c[0,0]} {sweeps*n*(n-1)/2}");
		}
	Write("\n\n");
	Error.Write("\n\n");

	for(int k=0;k<5;k++){
		n=100+k*7;
		var rnd = new Random(1);
		var A = new matrix(n,n);
		for(int i=0;i<n;i++)for(int j=i;j<n;j++){
			A[i,j]=2*(rnd.NextDouble()-0.5);
			}

		Stopwatch stopwatch = Stopwatch.StartNew(); // Stopwatch
		(D_1,rotations_1)=diag.values(A,1);
		stopwatch.Stop();

		time=stopwatch.ElapsedMilliseconds*0.001;

		Error.WriteLine($"{n} {time}");
		WriteLine($"{n} {D_1[0,0]} {rotations_1}");
		}
	Write("\n\n");
	Error.Write("\n\n");

	for(int k=0;k<7;k++){
		n=23+k*5;
		var rnd = new Random(1);
		var A = new matrix(n,n);
		for(int i=0;i<n;i++)for(int j=i;j<n;j++){
			A[i,j]=2*(rnd.NextDouble()-0.5);
			}

		Stopwatch stopwatch = Stopwatch.StartNew(); // Stopwatch
		(D_n,rotations_n)=diag.values(A,n);
		stopwatch.Stop();

		time=stopwatch.ElapsedMilliseconds*0.001;

		Error.WriteLine($"{n} {time}");
		WriteLine($"{n} {D_n[0,0]} {rotations_n}");
		}

}
}
