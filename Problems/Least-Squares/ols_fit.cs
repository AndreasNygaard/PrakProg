using static System.Math;
public class qrDecomp{
public static (matrix, matrix) qr_gs_decomp(matrix A){
	int m=A.size2;
	matrix Q = A.copy();
	matrix R = new matrix(m,m);
	for(int i=0;i<m;i++){
		R[i,i]=Q[i].norm();
		Q[i]/=R[i,i];
		for(int j=i+1;j<m;j++){
			R[i,j]=Q[i]%Q[j];
			Q[j]-=Q[i]*R[i,j];
			}
		}
	return (Q,R);
	}

public static vector qr_gs_solve(matrix Q, matrix R, vector b){
	var x=Q%b;
	for(int i=x.size-1;i>=0;i--){
		double c=0;
		for(int j=i+1;j<x.size;j++)c+=R[i,j]*x[j];
		x[i]=(x[i]-c)/R[i,i];
		}
	return x;
	}

public static matrix qr_gs_inverse(matrix Q, matrix R){
	int n=Q.size1;
	int m=Q.size2;
	if(m!=n)throw new System.ArgumentException("Matrix not square", "Q");
	matrix B = new matrix(n,n); // inverse matrix
	for(int i=0;i<=n-1;i++){
		vector ei = new vector(n);
		for(int j=0;j<=n-1;j++){ei[j]=0;} // unit vector is calculated
		ei[i]=1;
		vector xi=qr_gs_solve(Q,R,ei); // system of equations is solved
		for(int j=0;j<=n-1;j++){B[j,i]=xi[j];} // xi is put as i'th column in B
		}
	return B;
	}

public static void qr_givens_decomp(matrix A){/* A is overwritten with a matrix which is R in the upper triangle 
						and contains the Givens angles in the lower triangle*/
	for(int q=0;q<A.size2;q++)
	for(int p=q+1;p<A.size1;p++){
		double theta=Atan2(A[p,q],A[q,q]);
		for(int k=q;k<A.size2;k++){
			double xq=A[q,k],xp=A[p,k];
			A[q,k]=xq*Cos(theta)+xp*Sin(theta);
			A[p,k]=-xq*Sin(theta)+xp*Cos(theta);
			}
		A[p,q]=theta;
		}
	}

public static vector qr_givens_solve(matrix T,vector b){/* a Sequence of Givens rotation matrices is
								multiplied onto and overwrites rhs vector b */
	int n=b.size;
	vector x = new vector(n); // solution vector
	for(int q=0;q<T.size2;q++)
	for(int p=q+1;p<T.size1;p++){
		double theta=T[p,q];
		double bq=b[q],bp=b[p];
		b[q]=bq*Cos(theta)+bp*Sin(theta);
		b[p]=-bq*Sin(theta)+bp*Cos(theta);
		}
	x[n-1]=b[n-1]/T[n-1,n-1]; // Back-substitution
	vector sum_Rx = new vector(n);
	for(int i=n-2;i>=0;i--){
		for(int j=i+1;j<=n-1;j++){sum_Rx[i]+=T[i,j]*x[j];}
		x[i]=(b[i]-sum_Rx[i])/T[i,i];
		}
	return x;
	}

}
