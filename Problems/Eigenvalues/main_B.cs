using System;
using static System.Console;
using static System.Math;
class main{
static void Main(){
	int n=7;
	var rnd = new Random(1);
	var A = new matrix(n,n);
	for(int i=0;i<n;i++)for(int j=i;j<n;j++){
		A[i,j]=2*(rnd.NextDouble()-0.5); A[j,i]=A[i,j];
		}

	WriteLine("____Assignment B____");
	WriteLine("Eigenvalue Decomposition A=V*D*V^T");
	A.print("Random symmetric matrix A:");
	(matrix D_c, matrix V_c, int sweeps)=diag.cyclic(A);
	WriteLine($"Number of sweeps={sweeps}");

	(matrix D_1, int rotations_1)=diag.values(A,1);
	(matrix D_n, int rotations_n)=diag.values(A,n);
	D_c.print("Eigenvalues of cyclic method:");
	WriteLine($"Number of rotations: {sweeps*n*(n-1)/2}\n");
	D_1.print("Eigenvalue of 1-value method (only first row):");
	WriteLine($"Number of rotations: {rotations_1}\n");
	D_n.print("Eigenvalues of value-by-value method:");
	WriteLine($"Number of rotations: {rotations_n}\n");

	Write("\n\n");
	WriteLine("Eigenvalues from highest to lowest");
	vector e = diag.high_eig(A,n);
	e.print("Eigenvalues:");

}
}
