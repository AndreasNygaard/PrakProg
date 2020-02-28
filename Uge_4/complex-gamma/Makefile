CSFLAGS = -optimize+ -platform:anycpu
#all: Complex-Gamma.svg

Complex-Gamma.svg: complex-gamma.gpi out.complex-gamma.txt
	gnuplot $<

out.complex-gamma.txt: main-complex-gamma.exe
	mono $< > $@ 2> log.complex-gamma.txt

main-complex-gamma.exe: main-complex-gamma.cs math.dll cmath.dll
	mcs -r:math.dll -r:cmath.dll $<

math.dll: complex-gamma.cs cmath.dll
	mcs -t:library -r:cmath.dll -out:$@ $<

LIBDIR = ../../lib/complex
cmath.dll: $(LIBDIR)/cmath.cs $(LIBDIR)/complex.cs
	mcs $(CSFLAGS) -target:library -out:cmath.dll $^

clean:
	rm -f *.exe *.dll out* *.svg *.png complex-gamma.gpi log* table* complex-gamma-c-exe

table.complex-gamma.txt: complex-gamma-c-exe
	./$< > $@

complex-gamma-c-exe: complex-gamma.c
	gcc complex-gamma.c -o $@

complex-gamma.gpi: Makefile table.complex-gamma.txt
	cat /dev/null > $@
	echo 'set encoding utf8' >> $@
	echo 'set title "Complex Gamma-function"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "Complex-Gamma.svg"' >> $@
	echo 'set xlabel "Re(z)"' >> $@
	echo 'set ylabel "Im(z)"' >> $@
	echo 'set zlabel "|Gamma(z)|"'
#	echo 'set xzeroaxis' >> $@
#	echo 'set yzeroaxis' >> $@
#	echo 'set zzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set ticslevel 0' >> $@
	echo 'set key off' >> $@
	echo 'set dgrid3d 150,150 qnorm 5' >> $@
	echo 'set pm3d clip1in' >> $@
	echo 'set pm3d noborder' >> $@
	echo 'set cbrange [*:6.5]' >> $@
	echo 'set view ,330' >> $@
#	echo 'set style fill transparent solid 0.5' >> $@
	echo 'splot [:][:][0:6.5] "out.complex-gamma.txt" u 1:2:\' >> $@
	echo '( $$3<6.5 ? $$3 : 6.5 ) w pm3d' >> $@ 
