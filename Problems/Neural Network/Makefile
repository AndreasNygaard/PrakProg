all: Fit.svg Derivative.svg Antiderivative.svg output.txt

Fit.svg: data.txt Makefile
	echo '\
	set term svg size 640,480 background rgb "white";\
	set out "$@";\
	set key out;\
	set tics out;\
	set title "Neural Network Curve Fitting";\
	set xlabel "x";\
	set ylabel "y";\
	plot\
	 "$<" index 0 with points pointtype 6 title "data"\
	,"$<" index 1 with lines title "fit"\
	'|gnuplot

Derivative.svg: data.txt Makefile
	echo '\
	set term svg size 640,480 background rgb "white";\
	set out "$@";\
	set key out;\
	set tics out;\
	set title "Neural Network Derivative";\
	set xlabel "x";\
	set ylabel "y";\
	f(x) = exp(-x*x)*(-5*sin(5*x-1)-2*x*cos(5*x-1));\
	plot [-1:1][]\
	"$<" index 2 with lines lw 4 lt rgb "red" title "Estimated"\
	,f(x) with lines dt 2 lw 3 lt rgb "black" title "Analytic";\
	'|gnuplot

Antiderivative.svg: data.txt Makefile
	echo '\
	set term svg size 940,480 background rgb "white";\
	set out "$@";\
	set key out;\
	set tics out;\
	set title "Neural Network Antiderivative";\
	set xlabel "x";\
	set ylabel "y";\
	i = {0,1};\
	f(x) = -0.1877184794-0.045046858263+cos(1.0)*x+5.0/2.0*sin(1.0)*x**2-9.0/2.0*cos(1.0)*x**3-155.0/24.0*sin(1.0)*x**4+937.0/120.0*cos(1.0)*x**5+395.0/48.0*sin(1.0)*x**6-7799.0/1008.0*cos(1.0)*x**7-53215.0/8064.0*sin(1.0)*x**8+125087.0/24192.0*cos(1.0)*x**9+545549.0/145152.0*sin(1.0)*x**10-20393423.0/7983360.0*cos(1.0)*x**11-3478267.0/2128896.0*sin(1.0)*x**12+1231265381.0/1245404160.0*cos(1.0)*x**13+1982571053.0/3487131648.0*sin(1.0)*x**14-27192392077.0/87178291200.0*cos(1.0)*x**15-27417833143.0/167382319104.0*sin(1.0)*x**16+1174908885961.0/14227497123840.0*cos(1.0)*x**17+62190289289.0/1552090595328.0*sin(1.0)*x**18-91253890786099.0/4865804016353280.0*cos(1.0)*x**19-165135954461431.0/19463216065413120.0*sin(1.0)*x**20+844005190156393.0/227070854096486400.0*cos(1.0)*x**21;\
	plot [-1:1][]\
	"$<" index 3 with lines lw 4 lt rgb "red" title "Estimated"\
	,f(x) with lines dt 2 lw 3 lt rgb "black" title "Analytic (Taylor approx. to 21st order)";\
	'|gnuplot

#real((sqrt(pi)/4.0)*exp(-25.0/4.0) * exp((-1)*i)*erf(x-2.5*i) + exp(i)*erf(x+2.5*i))

output.txt data.txt: main.exe
	mono $< > output.txt 2>data.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs ../Minimization/minimize.cs network.cs min.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt

