using System;
using static System.Console;

public class test{

public static int Main(string[] args){
int n=5, nvalues=1,  max_print=8;
string method="cyclic";
if (args.Length > 0) n = int.Parse(args[0]);
if (args.Length > 1) method = args[1];
if (args.Length > 2) nvalues = int.Parse(args[2]);
if(method!="cyclic" && method!="values")return 1;
Error.WriteLine("n={0}",n);

var rnd = new Random(1);
var a = new matrix(n,n);
vector e = new vector(n);
matrix v = new matrix(n,n);
if(n>max_print)
	{
	for(int i=0;i<n;i++)for(int j=i;j<n;j++)
		a[i,j]=2*(rnd.NextDouble()-0.59);
	int r;
	if(method=="cyclic") r=jacobi.cyclic(a,e,v)*n*(n-1)/2;
	else if(method=="values") r=jacobi.values(a,e,nvalues,v);
	else return 1;
	WriteLine($"{n} {e[0]} {r}");
	return 0;
	}
else
	{
	nvalues=2;
	for(int i=0;i<n;i++)for(int j=i;j<n;j++)
		{
		a[i,j]=rnd.NextDouble(); a[j,i]=a[i,j];
		}
	Console.WriteLine("Eigenvalue Decomposition A=V*D*V^T");
	Console.WriteLine($"calculating first {nvalues} eigenvalues");
	a.print("Random matrix A:");
	matrix b = a.copy();
	int r=jacobi.values(a,e,nvalues,v);
	System.Console.WriteLine($"rotations:{r}");
	matrix VTBV = v.T*b*v;
	VTBV.print($"V^T*A*V : {nvalues} rows should be zeroed");
Write($"First {nvalues} eigenvalues (should equal the diagonal elements above):\n");
for(int i=0;i<nvalues;i++)Write($"{e[i]:f3}");
Write("\n");
	return 0;
	}
}
}
