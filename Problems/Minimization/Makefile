all: Higgs.svg output.txt

Higgs.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key right;\
	set tics out;\
	set title "Higgs Production Signal";\
	set xlabel "Energy [GeV]";\
	set ylabel "Signal [arb. u.]";\
	plot [100:160][]\
	 "$<" index 1 with lines lw 3 lt 4 title "Fit" \
	,"$<" index 0 using 1:2:3 with errorbars title "Data";\
	'| gnuplot

output.txt data.txt: main.exe
	mono $< > output.txt 2>data.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs minimize.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt *.svg

