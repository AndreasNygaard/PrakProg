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
	for(int i=0;i<N-2;i++)c[i+1]=(p[i+1]-p[i]-c[i]*h[i])/h[i+1];
	c[N-2]/=2;
	for(int i=N-3;i>=0;i--)c[i]=(p[i+1]-p[i]-c[i+1]*h[i+1])/h[i];
	for(int i=0;i<N-1;i++)b[i]=p[i]-c[i]*h[i];
	int index=binsearch(x,z);
	double quad_z = y[index]+b[index]*(z-x[index])+c[index]*Pow(z-x[index],2);
	return quad_z;
}

public static double cinterp(vector x, vector y, double z){
	/* Calculates cubic interpolated value for the x-value 'z' */
	int N = x.size;
	vector p = new vector(N);
	vector d = new vector(N);
	vector c = new vector(N);
	vector b = new vector(N);
	vector h = new vector(N);
	vector D = new vector(N);
	vector Q = new vector(N-1);
	vector B = new vector(N);
	for(int i=0;i<N-1;i++)h[i]=x[i+1]-x[i];
	for(int i=0;i<N-1;i++)p[i]=(y[i+1]-y[i])/h[i];
	// building the tridiagonal system:
	D[0]=2;
	for(int i=0;i<N-2;i++)D[i+1]=2*h[i]/h[i+1]+2;
	D[N-1]=2;
	Q[0]=1;
	for(int i=0;i<N-2;i++)Q[i+1]=h[i]/h[i+1];
	for(int i=0;i<N-2;i++)B[i+1]=3*(p[i]+p[i+1]*h[i]/h[i+1]);
	B[0]=3*p[0];
	B[N-1]=3*p[N-2];
	//Gauss elimination:
	for(int i=1;i<N;i++){
	D[i]-=Q[i-1]/D[i-1];
	B[i]-=B[i-1]/D[i-1];
	}
	b[N-1]=B[N-1]/D[N-1];
	// back−substitution:
	for(int i=N-2;i>=0;i--)b[i]=(B[i]-Q[i]*b[i+1])/D[i];
	for(int i=0;i<N-1;i++){
	c[i]=(-2*b[i]-b[i+1]+3*p[i])/h[i];
	d[i]=(b[i]+b[i+1]-2*p[i])/h[i]/h[i];
	}
	int index=binsearch(x,z);
	double cub_z = y[index]+b[index]*(z-x[index])+c[index]*Pow(z-x[index],2)+d[index]*Pow(z-x[index],3);
	return cub_z;
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

public static double qintegral(vector x, vector y, double z){
	/* Calculates integral of quadratic interpolated graph */
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
	double integral = 0;
	for(int i=0;i<index;i++)/*calculates integral between every two points*/
	integral += y[i]*(x[i+1]-x[i])-b[i]*x[i]*(x[i+1]-x[i])+c[i]*Pow(x[i],2)*(x[i+1]-x[i])
		+(-2.0*c[i]*x[i]+b[i])*(Pow(x[i+1],2)-Pow(x[i],2))*0.5+c[i]*(Pow(x[i+1],3)-Pow(x[i],3))/3.0;
	integral += y[index]*(z-x[index])-b[index]*x[index]*(z-x[index])+c[index]*Pow(x[index],2)*(z-x[index])
		+(-2.0*c[index]*x[index]+b[index])*(Pow(z,2)-Pow(x[index],2))*0.5+c[index]*(Pow(z,3)-Pow(x[index],3))/3.0;
	return integral;
}

public static double cintegral(vector x, vector y, double z){
	/* Calculates integral of cubic interpolated graph */
	int N = x.size;
	vector p = new vector(N);
	vector d = new vector(N);
	vector c = new vector(N);
	vector b = new vector(N);
	vector h = new vector(N);
	vector D = new vector(N);
	vector Q = new vector(N-1);
	vector B = new vector(N);
	for(int i=0;i<N-1;i++)h[i]=x[i+1]-x[i];
	for(int i=0;i<N-1;i++)p[i]=(y[i+1]-y[i])/h[i];
	// building the tridiagonal system:
	D[0]=2;
	for(int i=0;i<N-2;i++)D[i+1]=2*h[i]/h[i+1]+2;
	D[N-1]=2;
	Q[0]=1;
	for(int i=0;i<N-2;i++)Q[i+1]=h[i]/h[i+1];
	for(int i=0;i<N-2;i++)B[i+1]=3*(p[i]+p[i+1]*h[i]/h[i+1]);
	B[0]=3*p[0];
	B[N-1]=3*p[N-2];
	//Gauss elimination:
	for(int i=1;i<N;i++){
	D[i]-=Q[i-1]/D[i-1];
	B[i]-=B[i-1]/D[i-1];
	}
	b[N-1]=B[N-1]/D[N-1];
	// back−substitution:
	for(int i=N-2;i>=0;i--)b[i]=(B[i]-Q[i]*b[i+1])/D[i];
	for(int i=0;i<N-1;i++){
	c[i]=(-2*b[i]-b[i+1]+3*p[i])/h[i];
	d[i]=(b[i]+b[i+1]-2*p[i])/h[i]/h[i];
	}
	int index=binsearch(x,z);
	double integral = 0;
	for(int i=0;i<index;i++)/*calculates integral between every two points*/
	integral += (y[i]-b[i]*x[i]+c[i]*Pow(x[i],2)-d[i]*Pow(x[i],3))*(x[i+1]-x[i])
		+(3.0*d[i]*Pow(x[i],2)-2.0*c[i]*x[i]+b[i])*(Pow(x[i+1],2)-Pow(x[i],2))*0.5
		+(-3.0*d[i]*x[i]+c[i])*(Pow(x[i+1],3)-Pow(x[i],3))/3.0
		+d[i]*(Pow(x[i+1],4)-Pow(x[i],4))/4.0;
	integral += (y[index]-b[index]*x[index]+c[index]*Pow(x[index],2)-d[index]*Pow(x[index],3))*(z-x[index])
		+(3.0*d[index]*Pow(x[index],2)-2.0*c[index]*x[index]+b[index])*(Pow(z,2)-Pow(x[index],2))*0.5
		+(-3.0*d[index]*x[index]+c[index])*(Pow(z,3)-Pow(x[index],3))/3.0
		+d[index]*(Pow(z,4)-Pow(x[index],4))/4.0;
	return integral;
}

public static double qdiff(vector x, vector y, double z){
	/* Calculates integral of quadratic interpolated graph */
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
	double diff = b[index]+2.0*c[index]*(z-x[index]);
	return diff;
}

public static double cdiff(vector x, vector y, double z){
	/* Calculates cubic interpolated value for the x-value 'z' */
	int N = x.size;
	vector p = new vector(N);
	vector d = new vector(N);
	vector c = new vector(N);
	vector b = new vector(N);
	vector h = new vector(N);
	vector D = new vector(N);
	vector Q = new vector(N-1);
	vector B = new vector(N);
	for(int i=0;i<N-1;i++)h[i]=x[i+1]-x[i];
	for(int i=0;i<N-1;i++)p[i]=(y[i+1]-y[i])/h[i];
	// building the tridiagonal system:
	D[0]=2;
	for(int i=0;i<N-2;i++)D[i+1]=2*h[i]/h[i+1]+2;
	D[N-1]=2;
	Q[0]=1;
	for(int i=0;i<N-2;i++)Q[i+1]=h[i]/h[i+1];
	for(int i=0;i<N-2;i++)B[i+1]=3*(p[i]+p[i+1]*h[i]/h[i+1]);
	B[0]=3*p[0];
	B[N-1]=3*p[N-2];
	//Gauss elimination:
	for(int i=1;i<N;i++){
	D[i]-=Q[i-1]/D[i-1];
	B[i]-=B[i-1]/D[i-1];
	}
	b[N-1]=B[N-1]/D[N-1];
	// back−substitution:
	for(int i=N-2;i>=0;i--)b[i]=(B[i]-Q[i]*b[i+1])/D[i];
	for(int i=0;i<N-1;i++){
	c[i]=(-2*b[i]-b[i+1]+3*p[i])/h[i];
	d[i]=(b[i]+b[i+1]-2*p[i])/h[i]/h[i];
	}
	int index=binsearch(x,z);
	double diff = b[index]+2.0*c[index]*(z-x[index])+3.0*d[index]*Pow(z-x[index],2);
	return diff;
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
