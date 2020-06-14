using System;
using static System.Math;
using static System.Double;

public static class rootfinding{

public static vector newton(Func<vector,vector> f, vector x_start, double eps=1e-3, double dx=1e-7){
	vector x=x_start.copy();
	int n=x.size;
	matrix J = new matrix(n,n);
	vector fx = new vector(n);
	vector df = new vector(n);
	vector y = new vector(n);
	vector fy = new vector(n);
	vector Dx = new vector(n);
	bool bool1=true;
	do{
		bool bool2=true;
		fx=f(x);
		for(int j=0;j<n;j++){
			x[j]+=dx;
			df=f(x)-fx;
			for(int i=0;i<n;i++){
				J[i,j]=df[i]/dx;
				}
			x[j]-=dx;
			}
		qrDecomp.qr_givens_decomp(J);
		Dx=qrDecomp.qr_givens_solve(J,fx*(-1));
		double s=2;
		do{
			s/=2;
			y = x+Dx*s;
			fy = f(y);
			if(fy.norm()<(1-s/2)*fx.norm())bool2=false;
			if(s<0.02)bool2=false;
			}while(bool2);

		x=y;
		fx=fy;
		if(Dx.norm()<dx)bool1=false;
		if(fx.norm()<eps)bool1=false;
		}while(bool1);

	return x;
	}

}
