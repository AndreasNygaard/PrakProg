using System;
using static System.Console;
using static System.Math;
static class main{
static void Main(){
	WriteLine("______ Assignment A ______\n");
	WriteLine("We use the linear interpolator to interpolate between points of \na cosine. The interpolation is seen in the figure Linterp.svg\nThe figure also shows the estimated antiderivative.\n");
	int N=17;
	double x_min=-4;
	double x_max=4;
	double Dx=(x_max-x_min)/(N-1);
	vector x_tab = new vector(N);
	vector y_tab = new vector(N);
	for(int i=0;i<N;i++){
		x_tab[i]=x_min+i*Dx;
		y_tab[i]=Cos(x_tab[i]);
		Error.WriteLine("{0} {1}",x_tab[i],y_tab[i]);
		}
	Error.Write("\n\n");
	double dx=1.0/32;
	for(double x=x_min;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.linterp(x_tab,y_tab,x));
	Error.Write("\n\n");
	for(double x=x_min;x<=x_max;x+=dx)
		// We know that the antiderivative is zero at
		// x=x_min, so we add a constant to fit the
		// exact antiderivative in the plot
		Error.WriteLine("{0} {1}",x,interp.lintegral(x_tab,y_tab,x)+Sin(x_min));
	Error.Write("\n\n");

	double z=PI;
	double integral = interp.lintegral(x_tab,y_tab,z);
	WriteLine($"We esimate the integral from x=-4 to x=π of the cosine to {integral}");
	WriteLine($"The exact value of the integral is ∫[-4,π]dx cos(x) = sin(4) = {Sin(4)}\n");
	Write("\n\n\n");





	WriteLine("______ Assignment B ______\n");
	WriteLine("We use the quadratic interpolator to interpolate between points of a linear \nand a quadratic function: f(x) = 3*x + 2  and  f(x) = 4*x^2 + 7*x - 2 ");
	WriteLine("The corresponding plots can be seen in the figure Qinterp.svg\nWe can see that antiderivatives are estimated well, while the derivative\nonly shows the correct tendencies.\n");
	N=12;
	Dx=(x_max-x_min)/(N-1);
	x_tab = new vector(N);
	y_tab = new vector(N);


	// The linear function
	for(int i=0;i<N;i++){
		x_tab[i]=x_min+i*Dx;
		y_tab[i]=3*x_tab[i]+2;
		Error.WriteLine("{0} {1}",x_tab[i],y_tab[i]);
		}
	Error.Write("\n\n");
	for(double x=x_min;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.qinterp(x_tab,y_tab,x));
	Error.Write("\n\n");
	for(double x=x_min;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.qintegral(x_tab,y_tab,x)+3.0/2.0*Pow(x_min,2)+2*x_min);
	Error.Write("\n\n");
		for(double x=x_min+dx*0.5;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.qdiff(x_tab,y_tab,x));
	Error.Write("\n\n");

	z=2;
	integral = interp.lintegral(x_tab,y_tab,z);
	WriteLine($"We esimate the integral from x=-4 to x=2 of the linear function to {integral}");
	WriteLine($"The exact value of the integral is ∫[-4,2]dx 3*x+2 = {-6}\n");


	// The quadratic function
	for(int i=0;i<N;i++){
		x_tab[i]=x_min+i*Dx;
		y_tab[i]=4*Pow(x_tab[i],2)+7*x_tab[i]-2;
		Error.WriteLine("{0} {1}",x_tab[i],y_tab[i]);
		}
	Error.Write("\n\n");
	for(double x=x_min;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.qinterp(x_tab,y_tab,x));
	Error.Write("\n\n");
	for(double x=x_min;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.qintegral(x_tab,y_tab,x)+4.0/3.0*Pow(x_min,3)+7.0/2.0*Pow(x_min,2)-2*x_min);
	Error.Write("\n\n");
	for(double x=x_min;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.qdiff(x_tab,y_tab,x));
	Error.Write("\n\n");

        integral = interp.lintegral(x_tab,y_tab,z);
        WriteLine($"We esimate the integral from x=-4 to x=2 of the quadratic function to {integral}");
        WriteLine($"The exact value of the integral is ∫[-4,2]dx 4*x^2+7*x-2 = {42}\n");
	Write("\n\n\n");





	WriteLine("______ Assignment C ______\n");
	WriteLine("We use the cubic interpolator to interpolate between points of a function: \nf(x) = cos(3*x - 2) * (x^3 - 50*x)\n");
	WriteLine("The corresponding plots can be seen in the figure Cinterp.svg along with a \nspline from Gnuplot. We can see that the cubic spline from Gnuplot agrees with my own.\n");

        N=25;
	Dx=(x_max-x_min)/(N-1);
	x_tab = new vector(N);
	y_tab = new vector(N);

	for(int i=0;i<N;i++){
		x_tab[i]=x_min+i*Dx;
		y_tab[i]=Cos(3*x_tab[i]-2)*(Pow(x_tab[i],3)-50*x_tab[i]);
		Error.WriteLine("{0} {1}",x_tab[i],y_tab[i]);
		}
	Error.Write("\n\n");
	for(double x=x_min;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.cinterp(x_tab,y_tab,x));
	Error.Write("\n\n");
	for(double x=x_min;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.cintegral(x_tab,y_tab,x)-45.82858806);
	Error.Write("\n\n");
		for(double x=x_min+dx*0.5;x<=x_max;x+=dx)
		Error.WriteLine("{0} {1}",x,interp.cdiff(x_tab,y_tab,x));
	Error.Write("\n\n");

	z=0;
	integral = interp.lintegral(x_tab,y_tab,z);
	WriteLine($"We esimate the integral from x=-4 to x=0 of the function to {integral}");
	WriteLine($"The exact value of the integral is ∫[-4,2]dx cos(3*x-2)*(x^3-50*x) = {48.17134062}\n");


	}
}
