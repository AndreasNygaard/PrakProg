all: Linterp.svg Qinterp.svg Cinterp.svg output.txt

Linterp.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 1024,600 background "white" font "Arial,18";\
	set out "$@";\
	set multiplot layout 1,2 title "Linear Interpolation and antiderivative of Cos(x)";\
	set key out;\
	set xtics 1;\
	set grid;\
	set key center bottom;\
	set rmargin 0;\
	f(x) = cos(x);\
	g(x) = sin(x);\
	plot [][-1:1]\
	 "$<" index 0 with points pointtype 7 title "points"\
	,"$<" index 1 with lines lw 4 lt rgb "red" title "linear interpolation"\
	, f(x) with lines dt 2 lw 3 lt rgb "black" title "exact cosine";\
	set key center bottom;\
	set rmargin;\
	plot [][-1:1]\
	 "$<" index 2 with lines lw 4 lt rgb "red" title "estimated antiderivative"\
	,g(x) with lines dt 2 lw 3 lt rgb "black" title "exact antiderivative";\
	unset multiplot;\
	'| gnuplot

Qinterp.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 1500,1000 background "white" font "Arial,18";\
	set out "$@";\
	set multiplot layout 2,3 title "Quadratic Interpolation, derivative and antiderivative of Linear and quadratic functions";\
	set key out;\
	set xtics 1;\
	set grid;\
	set key center bottom;\
	set rmargin 0;\
	f(x)  = 3*x+2;\
	g(x)  = 4*x**2+7*x-2;\
	F(x)  = 1.5*x**2+2*x;\
	G(x)  = 1.3333*x**3+3.5*x**2-2*x;\
	df(x) = 3;\
	dg(x) = 8*x+7;\
	plot [-4:4][]\
	 "$<" index 3 with points pointtype 7 title "points"\
	,"$<" index 4 with lines lw 4 lt rgb "red" title "quadratic interpolation"\
	, f(x) with lines dt 2 lw 3 lt rgb "black" title "exact: 3·x + 2";\
	set key center bottom;\
	set rmargin;\
	plot [-4:4][]\
	 "$<" index 5 with lines lw 4 lt rgb "red" title "estimated antiderivative"\
	,F(x) with lines dt 2 lw 3 lt rgb "black" title "exact antiderivative";\
	set key center bottom;\
	set rmargin;\
	plot [-4:4][]\
	 "$<" index 6 with lines lw 4 lt rgb "red" title "estimated derivative"\
	,df(x) with lines dt 2 lw 3 lt rgb "black" title "exact derivative";\
	set key center bottom;\
	set rmargin;\
	plot [-4:4][]\
	 "$<" index 7 with points pointtype 7 title "points"\
	,"$<" index 8 with lines lw 4 lt rgb "red" title "quadratic interpolation"\
	, g(x) with lines dt 2 lw 3 lt rgb "black" title "exact: 4·x^2 + 7·x - 2";\
	set key center bottom;\
	set rmargin;\
	plot [-4:4][]\
	 "$<" index 9 with lines lw 4 lt rgb "red" title "estimated antiderivative"\
	,G(x) with lines dt 2 lw 3 lt rgb "black" title "exact antiderivative";\
	set key center bottom;\
	set rmargin;\
	plot [-4:4][]\
	 "$<" index 10 with lines lw 4 lt rgb "red" title "estimated derivative"\
	,dg(x) with lines dt 2 lw 3 lt rgb "black" title "exact derivative";\
	unset multiplot;\
	'| gnuplot


Cinterp.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 1500,1300 background "white" font "Arial,18";\
	set out "$@";\
	set multiplot layout 2,3 title "Cubic Interpolation, derivative and antiderivative of cos(3x-2)·(x^3-50x)";\
	set key out;\
	set xtics 1;\
	set grid;\
	set key center bottom;\
	set size 0.3,0.4;\
	set origin 0.0,0.54;\
	set rmargin 0;\
	f(x)  = cos(3*x-2)*(x**3-50*x);\
	df(x) = (3*x**2-50)*cos(3*x-2)-3*(x**3-50*x)*sin(3*x-2);\
	F(x)  = 1.0/81.0*(3*x-2)**3*sin(3*x-2)+1.0/27.0*cos(3*x-2)*(3*x-2)**2-148.0/27.0*cos(3*x-2)-148.0/27.0*(3*x-2)*sin(3*x-2)+2.0/27.0*(3*x-2)**2*sin(3*x-2)-904.0/81.0*sin(3*x-2)+4.0/27.0*cos(3*x-2)*(3*x-2);\
	plot [-4:4][]\
	 "$<" index 11 with points pointtype 7 title "points"\
	,"$<" index 12 with lines lw 4 lt rgb "red" title "quadratic interpolation"\
	, f(x) with lines dt 2 lw 3 lt rgb "black" title "exact function";\
	set key center bottom;\
	set size 0.3,0.4;\
	set origin 0.33,0.54;\
	set rmargin;\
	plot [-4:4][]\
	 "$<" index 13 with lines lw 4 lt rgb "red" title "estimated antiderivative"\
	,F(x) with lines dt 2 lw 3 lt rgb "black" title "exact antiderivative";\
	set key center bottom;\
	set size 0.3,0.4;\
	set origin 0.66,0.54;\
	set rmargin;\
	plot [-4:4][]\
	 "$<" index 14 with lines lw 4 lt rgb "red" title "estimated derivative"\
	,df(x) with lines dt 2 lw 3 lt rgb "black" title "exact derivative";\
	set key center bottom;\
	set size 0.6,0.53;\
	set origin 0.2,0.0;\
	set rmargin;\
	plot [-4:4][]\
	 "$<" index 12 with lines lw 8 dt 4 lt rgb "red" title "quadratic interpolation"\
	, f(x) with lines dt 2 lw 6 lt rgb "black" title "exact function"\
	,"$<" index 11 with points pointtype 7 lc rgb "#9424d4" title "points"\
	,"$<" index 11 smooth csplines lw 3 lt rgb "green" title "Gnuplot spline";\
	unset multiplot;\
	'| gnuplot


output.txt data.txt: main.exe
	mono $< > output.txt 2> data.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs interp.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt *.svg
