using System;
using static System.Math;
using static System.Double;
using static System.Console;
using System.IO;

public static class mc{

public static (double,double) plainmc(Func<vector,double> F, vector a, vector b, int N, bool print_points=false, bool Lattice=false, bool Halton=false, bool Quasi=false, Random RND=null){
	double volume=1;
	int dim = a.size;
	lattice RNDL = new lattice(dim);
	halton RNDH = new halton(dim);
	if(RND==null)RND=new Random();

	vector x = new vector(dim);
	vector y = new vector(dim);
	for(int i=0;i<dim;i++)
		volume*=b[i]-a[i];
	double sum=0,sum2=0,sumy=0;
	for(int i=0;i<N;i++){
		if(Quasi){
			randomx(x,a,b,RND,RNDL,RNDH,print_points,Lattice:true);
			randomx(y,a,b,RND,RNDL,RNDH,print_points,Halton:true);
			}
		else{
			randomx(x,a,b,RND,RNDL,RNDH,print_points,Lattice:Lattice,Halton:Halton);
			}

		double fx=F(x);
		sum += fx;
		sum2 += fx*fx;
		if(Quasi){
			double fy=F(y);
			sumy += fy;
			}
		}
	if(Quasi){
		double meanL = sum/N;
		double meanH = sumy/N;
		return (meanL*volume, Abs(meanL*volume-meanH*volume));
		}
	else{
		double mean = sum/N;                             // <f_i>
		double sigma = Sqrt(sum2/N - mean*mean);    // sigma² = <(f_i)²> - <f_i>²
		double SIGMA = sigma/Sqrt(N);               // SIGMA² = <Q²> - <Q>²
		return (mean*volume, SIGMA*volume);
		}
	}

public static void randomx(vector x, vector a, vector b, Random RND, lattice RNDL, halton RNDH, bool print_points, bool Lattice=false, bool Halton=false){
	int dim = a.size;
	if(Lattice){
		vector y = x.copy();
		RNDL.next(y);
		for(int i=0;i<dim;i++)
			x[i] = a[i]+y[i]*(b[i]-a[i]);
		}
	if(Halton){
		vector y = x.copy();
		RNDH.next(y);
		for(int i=0;i<dim;i++)
			x[i] = a[i]+y[i]*(b[i]-a[i]);
		}
	if(!Lattice && !Halton){
		for(int i=0;i<dim;i++)
			x[i] = a[i]+RND.NextDouble()*(b[i]-a[i]);
		}

	if(print_points){
		StreamWriter pointWriter = new StreamWriter("sample_points.txt", true);
		for(int i=0; i<x.size; i++){
			pointWriter.Write($"{x[i]} ");
			}
		pointWriter.Write("\n");
		pointWriter.Close();
		}
	}

}
