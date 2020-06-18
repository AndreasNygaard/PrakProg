using System;
using static System.Console;
using static System.Math;
using System.Diagnostics;
class test{
public static int Main(string[] args){
	int n=5, nvalues=1,  max_print=8;
	string method="cyclic";
	if (args.Length > 0) n = int.Parse(args[0]);
	if (args.Length > 1) method = args[1];
	if (args.Length > 2) nvalues = int.Parse(args[2]);
	if(method!="cyclic" && method!="values")return 1;

	var rnd = new Random(1);
	var a = new matrix(n,n);
	var D = new matrix(n,n);
	var V = new matrix(n,n);

	for(int i=0;i<n;i++)for(int j=i;j<n;j++)
		a[i,j]=2*(rnd.NextDouble()-0.59);
	int r, sweeps;
	double t;
	if(method=="cyclic"){
		Stopwatch stopwatch = Stopwatch.StartNew();
		(D, V, sweeps)=diag.cyclic(a);
		stopwatch.Stop();
		t=stopwatch.ElapsedMilliseconds*0.001;
		r=sweeps*n*(n-1)/2;
		}
	else if(method=="values"){
		Stopwatch stopwatch = Stopwatch.StartNew();
		(D, r)=diag.values(a,nvalues);
		stopwatch.Stop();
		t=stopwatch.ElapsedMilliseconds*0.001;
		}
	else return 1;
	WriteLine($"{n} {D[0,0]} {r}");
	Error.WriteLine($"{n} {t}");

	return 0;
}
}
