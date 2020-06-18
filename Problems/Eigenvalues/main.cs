using System;
using static System.Console;
using static System.Math;
class main{
static void Main(){
	int n=5;
	var rnd = new Random(1);
	var A = new matrix(n,n);
	for(int i=0;i<n;i++)for(int j=i;j<n;j++){
		A[i,j]=2*(rnd.NextDouble()-0.5); A[j,i]=A[i,j];
		}


	WriteLine("____Assignment A____");
	WriteLine("Eigenvalue Decomposition A=V*D*V^T");
	A.print("Random symmetric matrix A:");
	(matrix D, matrix V, int sweeps)=diag.cyclic(A);
	WriteLine($"Number of sweeps={sweeps}");
	matrix VTAV = V.T*A*V;
	for(int i=0;i<n;i++) // Rounding elements to avoid displaying numbers that are essentially zero
		for(int j=0;j<n;j++){
			VTAV[i,j]=Round(VTAV[i,j],6);
			D[i,j]=Round(D[i,j],6);
			V[i,j]=Round(V[i,j],6);
			}
	V.print("Matrix with eigenvectors V:");
	VTAV.print("V^T*A*V (should be diagonal):");
	D.print("Eigenvalues matrix D:\n");
	if(D.approx(VTAV))WriteLine("D=V^T*A*V, test passed");
	else WriteLine("D!=V^T*A*V, test failed");
	Write("\n\n");

	WriteLine("Quantum particle in a box");
	int N=99;
	matrix H = new matrix(N,N);
	for(int i=0;i<N-1;i++){
		H[i,i]=2*Pow(N+1,2);
		H[i,i+1]=-1*Pow(N+1,2);
		H[i+1,i]=-1*Pow(N+1,2);
		}
	H[N-1,N-1]=2*Pow(N+1,2);
	(matrix eig_val, matrix eig_vec, int sweeps2)=diag.cyclic(H);
	for(int i=0;i<N/2;i++)Error.WriteLine($"{i} {eig_val[i,i]} {Pow(PI*(i+1),2)}");
	Error.WriteLine("\n\n");
	for(int k=0;k<5;k++){
		Error.WriteLine($"{0} {0}");
		double factor=Sign(eig_vec[0,k])/Sqrt(1.0/(N+1));
		for(int i=0;i<N;i++)Error.WriteLine($"{(i+1.0)/(N+1.0)} {factor*eig_vec[i,k]}");
		Error.WriteLine($"{1.0} {0}\n");
		}
	WriteLine("\nSee figures A_eig_vec.svg and A_eig_val.svg. We can see that our energy (eigenvalue)\ncalculation is accurate for the lowest 10-15% of the levels. The eigenfunctions\nlook as they should from the analytical result - sinusoidal oscillations.");
	Write("\n\n\n");

	n=7;
	rnd = new Random(1);
	A = new matrix(n,n);
	for(int i=0;i<n;i++)for(int j=i;j<n;j++){
		A[i,j]=2*(rnd.NextDouble()-0.5); A[j,i]=A[i,j];
		}

	WriteLine("____Assignment B____");
	WriteLine("Eigenvalue Decomposition A=V*D*V^T");
	A.print("Random symmetric matrix A:");
	(matrix D_c, matrix V_c, int sweeps_b)=diag.cyclic(A);
	WriteLine($"Number of sweeps={sweeps_b}");

	(matrix D_1, int rotations_1)=diag.values(A,1);
	(matrix D_n, int rotations_n)=diag.values(A,n);
	D_c.print("Eigenvalues of cyclic method:");
	WriteLine($"Number of rotations: {sweeps_b*n*(n-1)/2}\n");
	D_1.print("Eigenvalue of 1-value method (only first row):");
	WriteLine($"Number of rotations: {rotations_1}\n");
	D_n.print("Eigenvalues of value-by-value method:");
	WriteLine($"Number of rotations: {rotations_n}\n");
	Write("\n\n");

	WriteLine("See the figures B_rot.svg and B_time.svg for a comparison in\nhow the different methods perform as a function of dimension.\nI have used the System.Diagnostics.Stopwatch to estimate times, since this\nproved easier than the POSIX time utility which was a bit tricky with my OS.\nFrom the figures we can see the O(n^3) behavior, which is explicitly\ncalculated for the times in B_time.svg.");
	Write("\n\n");

	WriteLine("By changing the rotation angle, we can get the highest Eigenvalues first.");
	WriteLine("Eigenvalues from highest to lowest:");
	vector e = diag.high_eig(A,n);
	e.print("Eigenvalues:");

}
}
