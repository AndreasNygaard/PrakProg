using System;
using static System.Math;
using static System.Double;

public static class minimize{

public static (vector,double) qnewton(
		Func<vector,double> f, 	/* objective function */
		vector xstart,		/* starting point */
		double acc=1e-3, 	/* accuracy */
		double eps=1e-7		/* small number epsilon */
		){

	vector x = xstart.copy();
	double fx=f(x);
	vector gx=gradient(f,x);
	matrix B=matrix.id(x.size);
	int nsteps=0;
	while(nsteps<999){
		nsteps++;
		vector Dx=-B*gx;
		if(Dx.norm()<eps*x.norm()){
			break;
			}
		if(gx.norm()<acc){
			break;
			}
		vector z;
		double fz,lambda=1;
		while(true){// backtracking linesearch
			z=x+Dx*lambda;
			fz=f(z);
			if(fz<fx){
				break;
				}
			if(lambda<eps){
				B.setid();
				break;
				}
			lambda/=2;
			}
		vector s=z-x;
		vector gz=gradient(f,z);
		vector y=gz-gx;
		vector u=s-B*y;
		double uTy=u%y;
		if(Abs(uTy)>1e-6){
			B.update(u,u,1/uTy); // rank-1 update
			}
		x=z;
		gx=gz;
		fx=fz;
		}
	return (x,nsteps);
	}

public static vector gradient(
		Func<vector,double> f,	/* objective function */
		vector x,		/* vector of variables */
		double eps=1e-7		/* small number epsilon */
		){
	vector grad=new vector(x.size);
	double fx=f(x);
	for(int i=0;i<x.size;i++){
		double dx=Abs(x[i])*eps;
		if(Abs(x[i])<Sqrt(eps)) dx=eps;
		x[i]+=dx;
		grad[i]=(f(x)-fx)/dx;
		x[i]-=dx;
	}
	return grad;
	}

public static (vector,int) downhill_simplex(Func<vector,double> F , matrix simplex , int d , double simplex_size_goal){

	int hi=0,lo=0, k=0;
	vector centroid = new vector(d);
	vector F_value= new vector(d+1);
	vector p1 = new vector(d);
	vector p2 = new vector(d);
	simplex_initiate(F,simplex,F_value,d,hi,lo,centroid);
	while (size(simplex,d)>simplex_size_goal){
		(hi,lo,centroid) = simplex_update(simplex,F_value,d);
		reflection(simplex[hi],centroid,d,p1);
		double fre=F(p1);
		if(fre<F_value[lo]){ // reflection looks good - try expansion
			expansion(simplex[hi],centroid,d,p2);
			double fex=F(p2);
			if(fex<fre){ // accept expansion
				for(int i=0;i<d;i++)
					simplex[hi][i]=p2[i];
				F_value[hi]=fex;
				}
			else{ // reject expansion and accept reflection
				for(int i=0;i<d;i++)
					simplex[hi][i]=p1[i];
				F_value[hi]=fre;
				}
			}
		else{ // reflection wasn’t good
			if(fre<F_value[hi]){ // ok, accept reflection
				for(int i=0;i<d;i++){
					simplex[hi][i]=p1[i];
					F_value[hi]=fre;
					}
				}
			else{ // try contraction
				contraction(simplex[hi],centroid,d,p1);
				double f_co=F(p1);
				if(f_co<F_value[hi]){ // accept contraction
					for(int i=0;i<d;i++){
						simplex[hi][i]=p1[i];
						F_value[hi]=f_co;
						}
					}
				else{ // do reduction
					reduction(simplex,d,lo);
					simplex_initiate_k(F,simplex,F_value,d,hi,lo,centroid);
					}
				}
			}
		k++;
		}
	return (centroid,k);
	}

public static (int,int,vector) simplex_update(matrix simplex, vector f_values, int d){
	int hi=0;
	int lo=0;
	for(int i=1; i<d+1; i++){
		if(f_values[i] < f_values[lo]){
			lo = i;
			}
		if(f_values[i] > f_values[hi]){
			hi = i;
			}
		}

	vector centroid = calcCentroid(simplex, hi);
	return (hi,lo,centroid);
	}


public static void simplex_initiate(Func<vector,double> fun, matrix simplex, vector f_values, int d, int hi ,int lo, vector centroid){
	for(int k=0;k<d+1;k++)
		f_values[k]=fun(simplex[k]);
	}

public static void simplex_initiate_k(Func<vector,double> fun, matrix simplex, vector f_values, int d, int hi ,int lo, vector centroid){
	for(int k=0;k<d+1;k++)
		if(k!=lo)
			f_values[k]=fun(simplex[k]);
	}

public static void reflection(vector highest, vector centroid, int dim, vector reflected){
	for(int i=0;i<dim;i++)
		reflected[i]=2*centroid[i]-highest[i];
	}

public static void expansion(vector highest, vector centroid, int dim, vector expanded){
	for(int i=0;i<dim;i++)
		expanded[i]=3*centroid[i]-2*highest[i];
	}

public static void contraction(vector highest, vector centroid, int dim, vector contracted){
	for(int i=0;i<dim;i++)
		contracted[i]=0.5*centroid[i]+0.5*highest[i];
	}

public static void reduction(matrix simplex,int dim,int lo){
	for(int k=0;k<dim+1;k++)
		if(k!=lo)
			for(int i=0;i<dim;i++)
				simplex[k][i]=0.5*(simplex[k][i]+simplex[lo][i]);
	}

public static double distance(vector a ,vector b,int dim){
	double s=0;
	for(int i=0;i<dim;i++)
		s+=Pow(b[i]-a[i],2);
	return Sqrt(s);
	}

public static double size(matrix simplex,int dim){
	double s=0;
	for(int k=1;k<dim+1;k++){
		double dist=distance(simplex[0],simplex[k],dim);
		if(dist>s)s=dist;
		}
	return s;
	}

public static vector calcCentroid(matrix simplex, int hi){
	int n = simplex.size2;
	vector centroid = new vector(n-1);
	for(int i=0; i<n; i++){
		if(i!=hi){
			centroid += simplex[i];
			}
		}
	centroid = centroid/(n-1);

	return centroid;
	}

}
