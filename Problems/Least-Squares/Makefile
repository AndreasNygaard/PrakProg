all: output.txt A.svg C.svg

A.svg: data.txt Makefile
	rm -f fit.log
	echo '\
	set term svg font "Arial,20";\
	set out "$@";\
	set title "Rutherford and Soddy experiment";\
	set xlabel "Time in Days";\
	set ylabel "Percent Activity";\
	a=100;b=0.2;c=1;\
	f(x)=a*exp(-b*x)+c;\
	fit f(x) "$<" u 1:2:3 yerrors via a,b,c;\
	plot "$<" with errorbars title "experimental data"\
	,"$<" index 1 with lines title "exponential fit"\
	,f(x) with lines title "gnuplot fit"\
	' | gnuplot 2> term.out
	rm -f term.out

C.svg: data.txt Makefile
	echo '\
	set term svg font "Arial,20";\
	set out "$@";\
	set title "Rutherford and Soddy experiment";\
	set log y;\
	set xlabel "Time in Days";\
	set ylabel "Percent Activity";\
	plot "$<" with errorbars title "experimental data"\
	,"$<" index 1 with lines title "best fit - y(t)=a·exp(-k·t)"\
	,"$<" index 2 with lines title "a+da"\
	,"$<" index 3 with lines title "a-da"\
	,"$<" index 4 with lines title "k+dk"\
	,"$<" index 5 with lines title "k-dk"\
	' | gnuplot

output.txt: main.exe
	mono $< > output.txt 2> data.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs ../Linear-Equations/qrDecomp.cs ols_fit.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt *.svg *.log
