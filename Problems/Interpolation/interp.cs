using static System.Math;
public static partial class interp{

public static double linterp(vector x, vector y, double z){
	/* Calculates linear interpolated value for the x-value 'z' */
	int index=binsearch(x,z);
	double Delta_x=x[index+1]-x[index];
	double Delta_y=y[index+1]-y[index];
	double p=Delta_y/Delta_x;
	double lin_z=y[index]+p*(z-x[index]);
        return lin_z;
}

public static double qinterp(vector x, vector y, double z){
        /* Calculates quadratic interpolated value for the x-value 'z' */
	int N = x.size;
	vector p = new vector(N-1);
	vector c = new vector(N-1);
	vector b = new vector(N-1);
        vector h = new vector(N-1);
	for(int i=0;i<N-1;i++){
	h[i]=x[i+1]-x[i];
	p[i]=(y[i+1]-y[i])/h[i];
	}
	c[0]=0;
	for(int i=0;i<N-2;i++)
	c[i+1]=(p[i+1]-p[i]-c[i]*h[i])/h[i+1];
	c[N-2]/=2;
	for(int i=N-3;i>=0;i--)
	c[i]=(p[i+1]-p[i]-c[i+1]*h[i+1])/h[i];
	for(int i=0;i<N-1;i++)
	b[i]=p[i]-c[i]*h[i];
	int index=binsearch(x,z);
	double quad_z = y[index]+b[index]*(z-x[index])+c[index]*Pow(z-x[index],2);
	return quad_z;
}

public static double lintegral(vector x, vector y, double z){
        /* Calculates integral of linear interpolated graph */
	int index=binsearch(x,z);
	double integral = 0;
	for(int i=0;i<index;i++)/*calculates integral between every two points*/
	integral += (x[i+1]-x[i])*y[i]+0.5*(x[i+1]-x[i])*(y[i+1]-y[i]);
	integral += (z-x[index])*y[index]+0.5*(z-x[index])*(linterp(x,y,z)-y[index]);
	return integral;
}

public static int binsearch(vector x, double z)
	{/* locates the interval for z by bisection */ 
	int i=0, j=x.size-1;
	while(j-i>1){
		int mid=(i+j)/2;
		if(z>x[mid]) i=mid; else j=mid;
		}
	return i;
	}
}
