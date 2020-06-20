all: A.svg out.txt

A.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key right;\
	set tics out;\
	set title "Solution to y´=y·(1-y) with y(0)=0.5";\
	set xlabel "x";\
	set ylabel "y";\
	plot [][]\
	 "$<" index 0 with linespoints pt 7 ps 0.5 notitle;\
	'| gnuplot


out.txt data.txt: main.exe
	rm -f data.txt
	mono $< > out.txt 2>err.out

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/
math.dll: $(LIBDIR)/ode/driver.cs $(LIBDIR)/ode/rkstep23.cs $(LIBDIR)/matrix/vector.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f main.exe out.txt err.out math.dll data.txt *.svg
