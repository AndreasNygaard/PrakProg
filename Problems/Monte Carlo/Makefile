all: Error.svg Strat.svg output.txt

Error.svg: data.txt Makefile
	rm -f fit.log
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key right bottom;\
	set tics out;\
	set log x;\
	set log y;\
	set title "Error as a function of N";\
	set xlabel "N";\
	set ylabel "Error";\
	f(x) = a/sqrt(x)+b;\
	fit f(x) "$<" using 1:2 via a,b;\
	plot\
	 "$<" using 1:2 with lines lw 4 lt rgb "cyan" title "Estimated Error" \
	,"$<" using 1:3 with lines lw 2 lt rgb "red" title "Actual error" \
	,f(x) with lines lw 2 dt 2 lt rgb "black" title sprintf("O(1/√N) fit: %.f/√N+%.3f",a,b);\
	'| gnuplot

Strat.svg: strat_points.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set title "Stratified sampling:";\
	set size ratio 1;\
	set xlabel "x";\
	set ylabel "y";\
	set xtics 0.5;\
	set ytics 0.5;\
	plot "$<" using 1:2 notitle with points ps 0.07 pt 7 lt rgb "black";\
	'|gnuplot

output.txt data.txt strat_points.txt: main.exe
	rm -f strat_points.txt
	mono $< > output.txt 2>data.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs mc.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt *.log *.svg

