all: Sin.svg SIR.svg Tc_2.svg Tc_6.svg

Sin.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "Sin.svg";\
	set key out;\
	set tics out;\
	set title "dy_0/dx=y_1, dy_1/dx=-y_0";\
	set xlabel "x";\
	set ylabel "y";\
	plot \
	 "$<" index 0 using 1:2 with points pt 7 ps 0.5 title "y_0" \
	,"$<" index 0 using 1:3 with points pt 5 ps 0.5 title "y_1" \
	,"$<" index 0 using ($$1):(0) w p pt 7 ps 0.1 not \
	,sin(x) title "sin"\
	,cos(x) title "cos"\
	'|gnuplot

SIR.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 1080,740 background "white" font "Arial,18";\
	set out "SIR.svg";\
	set key out;\
	set tics out;\
	set title "SIR model in Denmark, N=5.8e+6, Tc=4d, Tr=10d";\
	set xlabel "Time in days";\
	set ylabel "Number of people";\
	plot [][0:] \
	 "$<" index 1 using 1:2 with linespoints pt 7 ps 0.5 title "Susceptible" \
	,"$<" index 1 using 1:3 with linespoints pt 5 ps 0.5 title "Infectious" \
	,"$<" index 1 using 1:4 with linespoints pt 9 ps 0.5 title "Removed" \
	'|gnuplot

Tc_2.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 1080,740 background "white" font "Arial,18";\
	set out "Tc_2.svg";\
	set key out;\
	set tics out;\
	set title "SIR model in Denmark, N=5.8e+6, Tc=2d, Tr=10d";\
	set xlabel "Time in days";\
	set ylabel "Number of people";\
	plot [][0:] \
	 "$<" index 2 using 1:2 with linespoints pt 7 ps 0.5 title "Susceptible" \
	,"$<" index 2 using 1:3 with linespoints pt 5 ps 0.5 title "Infectious" \
	,"$<" index 2 using 1:4 with linespoints pt 9 ps 0.5 title "Removed" \
	'|gnuplot

Tc_6.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 1080,740 background "white" font "Arial,18";\
	set out "Tc_6.svg";\
	set key out;\
	set tics out;\
	set title "SIR model in Denmark, N=5.8e+6, Tc=6d, Tr=10d";\
	set xlabel "Time in days";\
	set ylabel "Number of people";\
	plot [][0:] \
	 "$<" index 3 using 1:2 with linespoints pt 7 ps 0.5 title "Susceptible" \
	,"$<" index 3 using 1:3 with linespoints pt 5 ps 0.5 title "Infectious" \
	,"$<" index 3 using 1:4 with linespoints pt 9 ps 0.5 title "Removed" \
	'|gnuplot


output.txt data.txt: main.exe
	mono $< > output.txt 2> data.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<


LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs rk23.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt *.svg

