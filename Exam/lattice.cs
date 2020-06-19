using static System.Math;
public class lattice{
private readonly int dim;
private int n;
private readonly double[] a;
double frac(double x){return x-(int)x;}
public lattice(int dim,double s=PI){
	this.dim=dim;
	this.n=0;
	this.a=new double[dim];
	for(int i=0;i<dim;i++)a[i]=frac(Sqrt(s+i));
	}
public void next(vector x){
	n++;
	for(int i=0;i<dim;i++)x[i]=frac(n*a[i]);
	}
}
