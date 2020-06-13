using System;
using static System.Math;
using static System.Double;

public static class integrate{

public static (double,double) adaptive(Func<double,double> f, double a, double b, double acc=1e-3, double eps=1e-3, int lim=99, double f2=NaN, double f3=NaN){
	double f1=f(a+(b-a)/6), f4=f(a+5*(b-a)/6);
	if( IsNaN(f2) ){ f2=f(a+2*(b-a)/6); f3=f(a+4*(b-a)/6);}
	double Q=(2*f1+f2+f3+2*f4)/6*(b-a);
	double q=(f1+f2+f3+f4)/4*(b-a);
	double err=Abs(Q-q)/1.7;
	if(lim==0){
		Console.Error.WriteLine($"limit reached: a={a} b={b}");
		return (Q,err);
		}
	if(lim<0){
		Console.Error.WriteLine($"limit reached: a={a} b={b}");
		return (Q,err);
		}
	if(err<acc+eps*Abs(Q)){
		return (Q,err);
		}
	else{
		(double Q1, double err1)=adaptive(f,a,(a+b)/2,acc/Sqrt(2),eps,lim-1,f1,f2);
		(double Q2, double err2)=adaptive(f,(a+b)/2,b,acc/Sqrt(2),eps,lim-1,f3,f4);
		return (Q1+Q2,Sqrt(Pow(err1,2)+Pow(err2,2)));
		}
	}

public static (double,double) Clenshaw_Curtis(Func<double,double> f, double a, double b, double acc=1e-3, double eps=1e-3, int lim=99){
	Func<double,double> F=delegate(double t){return f((a+b+(b-a)*Cos(t))/2.0)*Sin(t)*(b-a)/2;};
	return adaptive(F,0,PI,acc,eps,lim);
	}

public static (double,double) adapt_inf(Func<double,double> f, double a, double b, double acc=1e-3, double eps=1e-3, int lim=99){
	if(a>b){(double Q ,double err)=adapt_inf(f,b,a,acc,eps,lim); return (-Q,err);};
	if(IsNegativeInfinity(a) && !IsInfinity(b))
		return adapt_inf(t=>f(b-(1-t)/t)/t/t,0,1,acc,eps,lim);
	if(!IsInfinity(a) && IsPositiveInfinity(b))
		return adapt_inf(t=>f(a+(1-t)/t)/t/t,0,1,acc,eps,lim);
	if(IsNegativeInfinity(a) && IsPositiveInfinity(b)){
		(double Q1, double err1)=adapt_inf(f,a,0,acc,eps);
		(double Q2, double err2)=adapt_inf(f,0,b,acc,eps,lim);
		return (Q1+Q2,Sqrt(Pow(err1,2)+Pow(err2,2)));
		};
	return adaptive(t=>f(a+(b-a)*(3*t*t-2*t*t*t))*(b-a)*6*(t-t*t) ,0,1,acc,eps,lim);
	}




// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
public static double o8av(Func<double,double> f, double a, double b, double acc=1e-6, double eps=1e-6){
	/// Variable substitutions to treat infinite limits
	/// and singularities at end-points.
	if(a>b) return -o8av(f,b,a,acc,eps);
	if(IsNegativeInfinity(a) && !IsInfinity(b))
		return o8av(t=>f(b-(1-t)/t)/t/t,0,1,acc,eps);
	if(!IsInfinity(a) && IsPositiveInfinity(b))
		return o8av(t=>f(a+(1-t)/t)/t/t,0,1,acc,eps);
	if(IsNegativeInfinity(a) && IsPositiveInfinity(b))
		return o8av(f,a,0,acc,eps)+o8av(f,0,b,acc,eps);
	return o8a(t=>f(a+(b-a)*(3*t*t-2*t*t*t))*(b-a)*6*(t-t*t) ,0,1,acc,eps);
	}//o8av

	public static double o8a
	(Func<double,double> f,double a,double b,
	double acc=1e-6,double eps=1e-6,
	double f2=NaN,double f3=NaN, double f6=NaN,double f7=NaN,
	int nrec=0,int limit=100)
	{
	/// Open 8-point Aadaptive integrator with reuse of points.
	double h=b-a,sqr2=Sqrt(2);
	double f1=f(a+1*h/12),f4=f(a+5*h/12),f5=f(a+7*h/12),f8=f(a+11*h/12);
	if(IsNaN(f2))
		{f2=f(a+2*h/12);f3=f(a+4*h/12);f6=f(a+8*h/12);f7=f(a+10*h/12);nrec=0;}
	const double
	w1=4738.0/19845,w2=-59.0/567,w3=5869.0/13230,w4=-74.0/945, 
	u1=208.0/945,u2=-7.0/135,u3=209.0/630,u4=0,
	w5=w4,w6=w3,w7=w2,w8=w1,
	u5=u4,u6=u3,u7=u2,u8=u1;
	double integral= (w1*f1+w2*f2+w3*f3+w4*f4+w5*f5+w6*f6+w7*f7+w8*f8)*h;
	double approx  = (u1*f1+u2*f2+u3*f3+u4*f4+u5*f5+u6*f6+u7*f7+u8*f8)*h;
	double error=Abs(integral-approx)/2;
	double tolerance=acc+eps*Abs(integral);
	if(error<tolerance) return integral;
	else if(++nrec>limit) return NaN;
	else return o8a(f,a,(a+b)/2,acc/sqr2,eps,f1,f2,f3,f4,nrec,limit)+
			o8a(f,(a+b)/2,b,acc/sqr2,eps,f5,f6,f7,f8,nrec,limit);
	}//o8a


}
