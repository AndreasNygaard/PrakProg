using System;
using static System.Math;
using static System.Double;
using static System.Console;
using System.IO;

public static class mc{

public static (double,double) plainmc(Func<vector,double> F, vector a, vector b, int N, bool print_points, Random RND=null){
	if(RND==null)RND=new Random();
	double volume=1;
	int dim = a.size;
	for(int i=0;i<dim;i++)
		volume*=b[i]-a[i];
	double sum=0,sum2=0;
	for(int i=0;i<N;i++){
		double fx=F(randomx(a,b,RND,print_points));
		sum += fx;
		sum2 += fx*fx;
		}
	double mean = sum/N;                             // <f_i>
	double sigma = Sqrt(sum2/N - mean*mean);    // sigma² = <(f_i)²> - <f_i>²
	double SIGMA = sigma/Sqrt(N);               // SIGMA² = <Q²> - <Q>²
	return (mean*volume, SIGMA*volume);
	}

public static vector randomx(vector a, vector b, Random RND, bool print_points){
	int dim = a.size;
	vector result = new vector(dim);
	for(int i=0;i<dim;i++){
		result[i] = a[i]+RND.NextDouble()*(b[i]-a[i]);
		}

	if(print_points){
		StreamWriter pointWriter = new StreamWriter("strat_points.txt", true);
		for(int i=0; i<dim; i++){
			pointWriter.Write($"{result[i]} ");
			}
		pointWriter.Write("\n");

		pointWriter.Close();
		}

	return result;
	}



public static (double,double) stratmc1(Func<vector,double> f, vector a, vector b, int N, bool print_points, Random RND=null){
	if(RND==null)RND=new Random(1);
	int dim=a.size;
	int min_points=8*dim;
	int min_new_points=4*min_points;

	double volume=1;
	for(int i=0;i<dim;i++)
		volume*=(b[i]-a[i]);
	vector x =new vector(dim);

	if(N<=2*min_points){//plain MC
		int n1=N/2;
		int n2=N-n1;
		(double r1, double e1)=plainmc(f,a,b,n1,print_points,RND);
		(double r2, double e2)=plainmc(f,a,b,n2,print_points,RND);
		double mcinteg=(n1*r1+n2*r2)/(n1+n2);
		double mcerror=Abs(r1-r2)/4;
		return (mcinteg,mcerror);
		}

	int trial_points=(int)Max(min_points,trial_fraction*N);
	int new_points=N-trial_points;

	var aL=new vector(dim);
	var aR=new vector(dim);
	var vL=new vector(dim);
	var vR=new vector(dim);
	var nL=new int[dim];
	var nR=new int[dim];
	var sum_f_L=new vector(dim);
	var sum_f2_L=new vector(dim);
	var sum_f_R=new vector(dim);
	var sum_f2_R=new vector(dim);

	for(int n=0;n<trial_points;n++){// trial run
		double fx=f(randomx(a,b,RND,print_points));
		for(int i=0;i<dim;i++){ // sub-sums
			if(x[i]<(a[i]+b[i])/2){
				nL[i]++;
				sum_f_L[i]+=fx;
				sum_f2_L[i]+=fx*fx;
				}
			else{
				nR[i]++;
				sum_f_R[i]+=fx;
				sum_f2_R[i]+=fx*fx;
				}
			}
		}

	double vmax=0;int i_bisect=RND.Next(0,dim);
	for(int i=0;i<dim;i++){
		if(nL[i]>0){
			aL[i]=sum_f_L[i]/nL[i];
			vL[i]=sum_f2_L[i]/nL[i]-Pow(aL[i],2);
			}
		if(nR[i]>0){
			aR[i]=sum_f_R[i]/nR[i];
			vR[i]=sum_f2_R[i]/nR[i]-Pow(aR[i],2);
			}
		double measure=Abs(aL[i]-aR[i]);
		if(measure>vmax){vmax=measure;i_bisect=i;}
		}

	double weight_L=vL[i_bisect];
	double weight_R=vR[i_bisect];
	if(weight_L==0 && weight_R==0)weight_L=weight_R=1;

	int new_points_L=min_points+(int)Round((new_points-2*min_points)*weight_L/(weight_L+weight_R));

	int new_points_R=min_points+(int)Round((new_points-2*min_points)*weight_R/(weight_L+weight_R));

	if(new_points<=min_new_points){
		new_points_L=new_points/2;
		new_points_R=new_points-new_points_L;
		}

	vector aa = a.copy();
	aa[i_bisect]=(a[i_bisect]+b[i_bisect])/2;
	vector bb = b.copy();
	bb[i_bisect]=(a[i_bisect]+b[i_bisect])/2;

	(double result_L0, double result_L1) = stratmc1(f,a,bb,new_points_L,print_points,RND);
	(double result_R0, double result_R1) = stratmc1(f,aa,b,new_points_R,print_points,RND);

	double new_integ=result_L0+result_R0;
	double new_error=Sqrt(Pow(result_L1,2)+Pow(result_R1,2));

	double trial_average=(sum_f_L[i_bisect]+sum_f_R[i_bisect])/trial_points;
	double trial_integ=trial_average*volume;


	double trial_error=Abs(trial_integ-new_integ)/4;

	double integ=(trial_integ*trial_points+new_integ*new_points)/
			(trial_points+new_points);
	double error2=(Pow(new_error*new_points,2)+Pow(trial_error*trial_points,2))/
			Pow(new_points+trial_points,2);
	double error=Sqrt(error2);

	return (integ,error);

	}

public static double trial_fraction=0.1;


public static void randomx_strat(vector x, double[] a, double[] b, Random RND){
	for(int i=0;i<x.size;i++) x[i]=a[i]+RND.NextDouble()*(b[i]-a[i]);
	}

public static double[] stratmc(Func<vector,double> f, double[] a, double[] b, int npoints, Random RND=null){
	if(RND==null)RND=new Random(1);
	int dim=a.Length;
	int min_points=8*dim;
	int min_new_points=4*min_points;

	double volume=1;
	for(int i=0;i<dim;i++)
		volume*=(b[i]-a[i]);
	var x =new vector(dim);

	if(npoints<=2*min_points){//plain MC
		int n1=npoints/2;
		int n2=npoints-n1;
		vector av = new vector(a);
		vector bv = new vector(b);
		(double r1, double e1)=plainmc(f,av,bv,n1,false,RND);
		(double r2, double e2)=plainmc(f,av,bv,n2,false,RND);

		double mcinteg=(n1*r1+n2*r2)/(n1+n2);
		double mcerror=Abs(r1-r2)/4;
		return new double[] {mcinteg,mcerror};
		}

	int trial_points=(int)Max(min_points,trial_fraction*npoints);
	int new_points=npoints-trial_points;

	var aL=new vector(dim);
	var aR=new vector(dim);
	var vL=new vector(dim);
	var vR=new vector(dim);
	var nL=new int[dim];
	var nR=new int[dim];
	var sum_f_L=new vector(dim);
	var sum_f2_L=new vector(dim);
	var sum_f_R=new vector(dim);
	var sum_f2_R=new vector(dim);

	for(int n=0;n<trial_points;n++){// trial run
		randomx_strat(x,a,b,RND);
		double fx=f(x);
		for(int i=0;i<dim;i++){ // sub-sums
			if(x[i]<(a[i]+b[i])/2){
				nL[i]++;
				sum_f_L[i]+=fx;
				sum_f2_L[i]+=fx*fx;
				}
			else{
				nR[i]++;
				sum_f_R[i]+=fx;
				sum_f2_R[i]+=fx*fx;
				}
			}
		}

	double vmax=0;int i_bisect=RND.Next(0,dim);
	for(int i=0;i<dim;i++){
		if(nL[i]>0){
			aL[i]=sum_f_L[i]/nL[i];
			vL[i]=sum_f2_L[i]/nL[i]-Pow(aL[i],2);
			}
		if(nR[i]>0){
			aR[i]=sum_f_R[i]/nR[i];
			vR[i]=sum_f2_R[i]/nR[i]-Pow(aR[i],2);
			}
		double measure=Abs(aL[i]-aR[i]);
		if(measure>vmax){
			vmax=measure;
			i_bisect=i;
			}
		}

	double weight_L=vL[i_bisect];
	double weight_R=vR[i_bisect];
	if(weight_L==0 && weight_R==0)
		weight_L=weight_R=1;

	int new_points_L=min_points
			+(int)Round((new_points-2*min_points)*weight_L/(weight_L+weight_R));

	int new_points_R=min_points
			+(int)Round((new_points-2*min_points)*weight_R/(weight_L+weight_R));

	if(new_points<=min_new_points){
		new_points_L=new_points/2;
		new_points_R=new_points-new_points_L;
		}

	var aa = (double[])a.Clone();
	aa[i_bisect]=(a[i_bisect]+b[i_bisect])/2;
	var bb = (double[])b.Clone();
	bb[i_bisect]=(a[i_bisect]+b[i_bisect])/2;

	double[] result_L = stratmc(f,a,bb,new_points_L,RND);
	double[] result_R = stratmc(f,aa,b,new_points_R,RND);

	double new_integ=result_L[0]+result_R[0];
	double new_error=Sqrt(Pow(result_L[1],2)+Pow(result_R[1],2));

	double trial_average=(sum_f_L[i_bisect]+sum_f_R[i_bisect])/trial_points;
	double trial_integ=trial_average*volume;

	double trial_error=Abs(trial_integ-new_integ)/4;

	double integ=(trial_integ*trial_points+new_integ*new_points)/
			(trial_points+new_points);
	double error2=(Pow(new_error*new_points,2)+Pow(trial_error*trial_points,2))/
			Pow(new_points+trial_points,2);
	double error=Sqrt(error2);

	return new double[] {integ,error};
	}


}
