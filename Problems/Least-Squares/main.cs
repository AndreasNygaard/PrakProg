using static qrDecomp;
using System;
using static System.Console;
using static System.Math;
class main{
static void Main(){
	int n=5,m=4;
	matrix A1=new matrix(n,m);
	var rnd=new Random(1);
	for(int i=0;i<n;i++)
	for(int j=0;j<m;j++)
		A1[i,j]=2*(rnd.NextDouble()-0.5);
	WriteLine("____Assignment A1____");
	A1.print($"QR-decomposition:\nrandom {n}x{m} matrix A:");
	(matrix Q, matrix R1) = qrDecomp.qr_gs_decomp(A1);
	R1.print("matrix R:");
	matrix QTQ = Q.T*Q;
	matrix QR  = Q*R1;
	QTQ.print("Q^T*Q:");
	QR.print("Q*R:");
	if(A1.approx(QR))WriteLine("Q*R=A, test passed");
	else WriteLine("Q*R!=A, test failed");
	Write("\n\n");


	matrix A2=new matrix(n,n);
	rnd=new Random(2);
	for(int i=0;i<n;i++)
	for(int j=0;j<n;j++)
		A2[i,j]=2*(rnd.NextDouble()-0.5);
	vector b = new vector(n);
	rnd=new Random(3);
	for(int i=0;i<n;i++)
		b[i]=2*(rnd.NextDouble()-0.5);
	WriteLine("____Assignment A2____");
	A2.print($"Solving system of equations A*x=Q*R*x=b:\nrandom {n}x{n} matrix A:");
	(matrix Q2, matrix R2) = qrDecomp.qr_gs_decomp(A2);
	Q2.print("matrix Q:");
	R2.print("matrix R:");
	b.print("vector b:");
	vector x2 = qrDecomp.qr_gs_solve(Q2,R2,b);
	vector Ax2=A2*x2;
	x2.print("solution vector x:");
	Ax2.print("A*x:");
	if(b.approx(A2*x2))WriteLine("A*x=b, test passed");
	else WriteLine("A*x!=b, test failed");
	Write("\n\n");


	matrix A3=new matrix(n,n);
	rnd=new Random(4);
	for(int i=0;i<n;i++)
	for(int j=0;j<n;j++)
		A3[i,j]=2*(rnd.NextDouble()-0.5);
	WriteLine("____Assignment B____");
	A3.print($"Decomposing A into Q*R:\nrandom {n}x{n} matrix A:");
	(matrix Q3, matrix R3) = qrDecomp.qr_gs_decomp(A3);
	Q3.print("matrix Q:");
	R3.print("matrix R:");
	matrix B = qrDecomp.qr_gs_inverse(Q3,R3);
	matrix I = new matrix(n,n);
	for(int i=0;i<n;i++)
	for(int j=0;j<n;j++){
		I[i,j]=0;
		if(i==j)I[i,j]=1;
		}
	I.print($"Idenity matrix I({n}):");
	B.print("Inverse matrix B:");
	matrix AB = A3*B;
	matrix AB_round = new matrix(n,n); // rounded matrix to 10th decimal to get a nicer output
	for(int i=0;i<=n-1;i++)
	for(int j=0;j<=n-1;j++)AB_round[i,j]=Math.Round((Double)AB[i,j], 10);
	AB_round.print("A*B:");
	if(AB.approx(I))WriteLine("A*B=I, test passed"); // test is still with unrounded matrix
	else WriteLine("A*B!=I, test failed");
	Write("\n\n");


	WriteLine("____Assignment C____");
	A2.print($"Givens rotation of the same coefficient matrix as in part A(2):\nrandom {n}x{m} matrix A:");
	matrix T = A2.copy();
	qrDecomp.qr_givens_decomp(T);
	T.print("Matrix with R in the upper trinagle and angles of the Givens rotation below, T:");
	b.print("Same vector b as in A(2):");
	vector Gb = b.copy();
	vector x4 = qrDecomp.qr_givens_solve(T,Gb);
	vector Ax4=A2*x4;
	x4.print("solution vector x:");
	Ax4.print("A*x:");
	if(b.approx(A2*x4))WriteLine("A*x=b, test passed");
	else WriteLine("A*x!=b, test failed");
	Write("\n\n");
}
}
