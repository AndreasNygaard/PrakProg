using System;
using static System.Math;
public class ols_fit{
public static (vector,vector,matrix) fit_coeff(vector x, vector y, vector dy, Func<double,double>[] f){
	int n=x.size,m=f.Length;
	matrix A = new matrix(n,m);
	vector b = new vector(n);
	for(int i=0;i<n;i++){
		b[i]=y[i]/dy[i];
		for(int k=0;k<m;k++)A[i,k] = f[k](x[i])/dy[i];
		}
	(matrix Q, matrix R) = qrDecomp.qr_gs_decomp(A);
	(matrix R_Q, matrix R_R) = qrDecomp.qr_gs_decomp(R); // Decompose R, to find inverse
	matrix R_inv = qrDecomp.qr_gs_inverse(R_Q,R_R);
	vector c = R_inv*Q.T*b;

	matrix sigma = R_inv*R_inv.T; // covariance matrix
	vector dc = new vector(sigma.size1); // uncertainties in coefficients
	for(int i=0;i<sigma.size1;i++)dc[i]=Sqrt(sigma[i,i]);

	return (c,dc,sigma);
	}

}
