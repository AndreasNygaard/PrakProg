using System;
using static System.Console;
using static System.Math;
using static System.Double;
using System.Collections;
using System.Collections.Generic;
public class schroedinger{

static public Func<double,vector,vector> SE(double e){
	Func<double,vector,vector>
		Shroedinger=delegate(double r, vector y){
			return new vector(y[1],-(2*e+2/r)*y[0]);
			};
	return Shroedinger;
	}

static public double SE_solve(double rmax, double eps){
	double rmin=1e-3;
	double h=0.001;
	double acc=1e-4;
	List<double> t;
	List<vector> yt;
	vector yrmin = new vector(rmin-rmin*rmin, 1-2*rmin);

	Func<vector,vector> M=delegate(vector e){
		if(rmax<rmin) return new vector(new double[]{rmax-rmax*rmax});
		double energy = e[0];
		(t, yt)=rk23.driver(SE(energy),rmin,yrmin,rmax,h,acc,eps);
		int n=t.Count;
		return new vector(new double[]{yt[n-1][0]});
		};

	vector e_start= new vector(new double[]{-1});
	vector root = rootfinding.newton(M,e_start,eps);
	WriteLine($"\nrmax = {rmax} Bohr radii");
	root.print("root of M(e)=0:");

	(t, yt)=rk23.driver(SE(root[0]),rmin,yrmin,rmax,h,acc,eps);
	int m=t.Count;
	for(int i=0;i<m;i++)
		Error.WriteLine($"{t[i]} {yt[i][0]}");
	Error.WriteLine("\n");

	return root[0];
	}

static public double SE_solve_improve(double rmax, double eps){
	double rmin=1e-3;
	double h=0.001;
	double acc=1e-4;
	List<double> t;
	List<vector> yt;
	vector yrmin = new vector(rmin-rmin*rmin, 1-2*rmin);

	Func<vector,vector> M=delegate(vector e){
		if(rmax<rmin) return new vector(new double[]{rmax-rmax*rmax});
		double energy = e[0];
		(t, yt)=rk23.driver(SE(energy),rmin,yrmin,rmax,h,acc,eps);
		int n=t.Count;
		return new vector(new double[]{yt[n-1][0]-rmax*Exp(-Sqrt(-2*energy)*rmax)});
		};

	vector e_start= new vector(new double[]{-1});
	vector root = rootfinding.newton(M,e_start,eps);
	WriteLine($"\nrmax = {rmax} Bohr radii");
	root.print("root of M(e)=0:");

	(t, yt)=rk23.driver(SE(root[0]),rmin,yrmin,rmax,h,acc,eps);
	int m=t.Count;
	for(int i=0;i<m;i++)
		Error.WriteLine($"{t[i]} {yt[i][0]}");
	Error.WriteLine("\n");

	return root[0];
	}

}
