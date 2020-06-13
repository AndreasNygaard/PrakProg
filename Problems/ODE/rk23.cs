using System;
using static System.Math;
using System.Collections;
using System.Collections.Generic;
public class rk23{
public static (vector,vector) rkstep23(
	Func<double,vector,vector> f, /* the right-hand-side of dy/dt=f(t,y) */
	double t,                     /* the current value of the variable */
	vector yt,                    /* the current value y(t) of the sought function */
	double h                     /* the step to be taken */){
//	vector yh,                    /* output: y(t+h) */
//	vector err                    /* output: error estimate */){

	vector k0 = f(t,yt);
	vector k1 = f(t+h/2  , yt+(h/2  )*k0);
	vector k2 = f(t+3*h/4, yt+(3*h/4)*k1);
	vector ka = (2*k0+3*k1+4*k2)/9;
	vector kb = k1;
	vector yh = yt+ka*h;
	vector err = (ka-kb)*h;
	return (yh,err);
	}

public static (List<double>, List<vector>) driver(
	Func<double,vector,vector> f, /* right-hand-side of dy/dt=f(t,y) */
	double a,                     /* the start-point a */
	vector y,                     /* y(a) */
	double b,                     /* the end-point of the integration */
	double h,                      /* initial step-size */
	double acc,                   /* absolute accuracy goal */
	double eps                   /* relative accuracy goal */){

	double dt=h;
	double e_i, tau_i;
	var t = new List<double>();
	var yt = new List<vector>();
	t.Add(a);
	yt.Add(y);
	int i=0;
	do{
		if(t[i]+dt>b)dt=b-t[i];
		(vector yh, vector err) = rkstep23(f,t[i],yt[i],dt);
		e_i=err.norm();
		tau_i=(eps*yt[i].norm()+acc)*Sqrt(dt/(b-a));
		if(e_i<tau_i){
			i++;
			yt.Add(yh);
			t.Add(t[i-1]+dt);
			}
		if(e_i>0)dt*=Pow(tau_i/e_i,0.25)*0.95; else h*=2;
	}while(t[i]<b);
	return (t,yt);
	}
}
