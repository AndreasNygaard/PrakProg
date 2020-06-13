using System;
using static System.Math;
public class diag{
public static (matrix,matrix,int) cyclic(matrix A){
	int n=A.size1;
	int sweeps=0;
	bool changed;
	matrix D = A.copy();
	matrix V = new matrix(n,n);
	for(int i=0;i<n;i++)V[i,i]=1.0;
	do{sweeps++; changed=false; int p,q;
		for(q=n-1;q>0;q--)for(p=0;p<q;p++){
			double dpp=D[p,p];
			double dqq=D[q,q];
			double dpq=D[p,q];
			double phi=0.5*Math.Atan2(2*dpq,dqq-dpp);
			double c = Math.Cos(phi), s = Math.Sin(phi);
			double dpp1=c*c*dpp-2*s*c*dpq+s*s*dqq;
			double dqq1=s*s*dpp+2*s*c*dpq+c*c*dqq;
			if(dpp1!=dpp || dqq1!=dqq){
				changed=true;
				D[p,p]=dpp1;
				D[q,q]=dqq1;
				D[p,q]=0.0;
				D[q,p]=0.0;
				for(int i=0;i<p;i++){
					double dip=D[i,p];
					double diq=D[i,q];
					D[i,p]=c*dip-s*diq;
					D[i,q]=c*diq+s*dip;
					D[p,i]=D[i,p];
					D[q,i]=D[i,q];
					}
				for(int i=p+1;i<q;i++){
					double dpi=D[p,i];
					double diq=D[i,q];
					D[p,i]=c*dpi-s*diq;
					D[i,q]=c*diq+s*dpi;
					D[i,p]=D[p,i];
					D[q,i]=D[i,q];
					}
				for(int i=q+1;i<n;i++){
					double dpi=D[p,i];
					double dqi=D[q,i];
					D[p,i]=c*dpi-s*dqi;
					D[q,i]=c*dqi+s*dpi;
					D[i,p]=D[p,i];
					D[i,q]=D[q,i];
					}
				for(int i=0;i<n;i++){
					double vip=V[i,p];
					double viq=V[i,q];
					V[i,p]=c*vip-s*viq;
					V[i,q]=c*viq+s*vip;
					}
				}
			}
		}while(changed);

	return (D,V,sweeps);
	}


public static (matrix,int) values(matrix A, int nvalues=1){
	bool changed; int rotations=0, n=A.size1;
	matrix D = A.copy();
//	matrix V = new matrix(n,n);
//	for(int i=0;i<n;i++)V[i,i]=1.0;
	for(int p=0;p<nvalues;p++)do{
		changed=false;
	for(int q=p+1;q<n;q++){
		double dpp=D[p,p];
		double dqq=D[q,q];
		double dpq=D[p,q];
		double phi=0.5*Math.Atan2(2*dpq,dqq-dpp);
		double c = Math.Cos(phi), s = Math.Sin(phi);
		double dpp1=c*c*dpp-2*s*c*dpq+s*s*dqq;
		double dqq1=s*s*dpp+2*s*c*dpq+c*c*dqq;
		if(dpp1!=dpp || dqq1!=dqq){
			rotations++;
			changed=true;
			D[p,p]=dpp1;
			D[q,q]=dqq1;
			D[p,q]=0.0;
			D[q,p]=0.0;
			for(int i=p+1;i<q;i++){
				double dpi=D[p,i];
				double diq=D[i,q];
				D[p,i]=c*dpi-s*diq;
				D[i,q]=c*diq+s*dpi;
				D[i,p]=D[p,i];
				D[q,i]=D[i,q];
				}
			for(int i=q+1;i<n;i++){
				double dpi=D[p,i];
				double dqi=D[q,i];
				D[p,i]=c*dpi-s*dqi;
				D[q,i]=c*dqi+s*dpi;
				D[i,p]=D[p,i];
				D[i,q]=D[q,i];
				}
//			for(int i=0;i<n;i++){
//				double vip=V[i,p];
//				double viq=V[i,q];
//				V[i,p]=c*vip-s*viq;
//				V[i,q]=c*viq+s*vip;
//				}
			}
		}
		}while(changed);
	return (D,rotations);
	}

public static vector high_eig(matrix A, int nvalues=1){
	bool changed; int n=A.size1;
	matrix D = A.copy();
	for(int p=0;p<nvalues;p++)do{
		changed=false;
	for(int q=p+1;q<n;q++){
		double dpp=D[p,p];
		double dqq=D[q,q];
		double dpq=D[p,q];
		double phi=0.5*Math.Atan2(-2*dpq,-dqq+dpp);
		double c = Math.Cos(phi), s = Math.Sin(phi);
		double dpp1=c*c*dpp-2*s*c*dpq+s*s*dqq;
		double dqq1=s*s*dpp+2*s*c*dpq+c*c*dqq;
		if(dpp1!=dpp || dqq1!=dqq){
			changed=true;
			D[p,p]=dpp1;
			D[q,q]=dqq1;
			D[p,q]=0.0;
			D[q,p]=0.0;
			for(int i=p+1;i<q;i++){
				double dpi=D[p,i];
				double diq=D[i,q];
				D[p,i]=c*dpi-s*diq;
				D[i,q]=c*diq+s*dpi;
				D[i,p]=D[p,i];
				D[q,i]=D[i,q];
				}
			for(int i=q+1;i<n;i++){
				double dpi=D[p,i];
				double dqi=D[q,i];
				D[p,i]=c*dpi-s*dqi;
				D[q,i]=c*dqi+s*dpi;
				D[i,p]=D[p,i];
				D[i,q]=D[q,i];
				}
			}
		}
		}while(changed);
	vector e = new vector(n);
	for(int i=0;i<n;i++){
		e[i]=D[i,i];
		}
	return e;
	}

}
