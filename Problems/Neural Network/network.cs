using System;
using static System.Math;
using static System.Console;

public class network{

public vector p;
public Func<double,double> f=delegate(double x){
	return x*Exp(-x*x);
	};
public Func<double,double> f_deriv=delegate(double x){
	return Exp(-x*x)*(1-2*Pow(x,2));
	};
public Func<double,double> f_int=delegate(double x){
	return (-1)*Exp(-x*x)/2;
	};
public readonly int n;
private double startPoint;


public network(int n){
	this.n=n;
	this.p=new vector(3*n);
	}

public double feed(double x){
	double sum=0;
	for(int i=0;i<n;i++){
		double a=p[3*i+0];
		double b=p[3*i+1];
		double w=p[3*i+2];
		sum+=w*f((x-a)/b);
		}
	return sum;
	}

public double feedDeriv(double x){
	double sum = 0;
	for(int i=0; i<n; i++){
		double a = p[3*i+0];
		double b = p[3*i+1];
		double w = p[3*i+2];
		sum += f_deriv((x-a)/b)*w/b;
		}
	return sum;
	}

public double feedInt(double x){
	double sum = 0;
	for(int i=0; i<n; i++){
		double a = p[3*i+0];
		double b = p[3*i+1];
		double w = p[3*i+2];
		sum += f_int((x-a)/b)*b*w;
		sum -= f_int((startPoint-a)/b)*b*w;
		}
	return sum;
	}

public (int,int) train(vector xs,vector ys){
	int ncalls=0;
	startPoint=xs[0];
	Func<vector,double> mismatch=(u)=>{
		ncalls++;
		p=u;
		double sum=0;
		for(int k=0;k<xs.size;k++){
			sum+=Pow(feed(xs[k])-ys[k],2);
			}
		return sum/xs.size;
		};
	vector v=p;
	int nsteps;
	(v, nsteps) = minimize.qnewton(mismatch,p,1e-4);
	p=v;
	return (nsteps,ncalls);
	}

}




