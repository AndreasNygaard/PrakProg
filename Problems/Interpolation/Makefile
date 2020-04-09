all: Linterp.svg

Linterp.svg: linterp.gpi output.txt
	gnuplot $<

integrals.exe: integrals.cs math.dll
	mcs -r:math.dll $<

output.txt: main.exe
	mono $< > $@ 2> table.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs interp.cs
	mcs -t:library -out:$@ $^

#table.txt: table.exe
#	mono $< > $@

#table.exe: table.cs
#	mcs $<

linterp.gpi: Makefile table.txt
	cat /dev/null > $@
	echo 'set encoding utf8' >> $@
	echo 'set title "Linear Interpolation"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "Linterp.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key top right' >> $@
	echo 'plot \' >> $@
	echo ' "output.txt" with lines title "Interpolation" \' >> $@
	echo ',"table.txt" with points pt 6 title "Cos(x)"' >> $@

clean:
	rm -f *.exe *.dll *.txt *.gpi *.svg
