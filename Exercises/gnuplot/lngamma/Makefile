all: lnGamma.svg

lnGamma.svg: lngamma.gpi out.lngamma.txt
	gnuplot $<

out.lngamma.txt: main-lngamma.exe
	mono $< > $@ 2> log.lngamma.txt

main-lngamma.exe: main-lngamma.cs math.dll
	mcs $< -r:math.dll $<

#LIBDIR=../../lib
math.dll: lngamma.cs #\
#	$(LIBDIR)/math.cs \
#	$(LIBDIR)/integration/quadc7.cs \
#	$(LIBDIR)/integration/quado8.cs \
#	$(LIBDIR)/integration/quado4.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll out* *.svg *.png lngamma.gpi log* table* lngamma-c-exe

table.lngamma.txt: lngamma-c-exe
	./$< > $@

lngamma-c-exe: lngamma.c
	gcc lngamma.c -o $@

lngamma.gpi: Makefile table.lngamma.txt
	cat /dev/null > $@
	echo 'set encoding utf8' >> $@
	echo 'set title "lnGamma-function: computed vs. tabulated"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "lnGamma.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot [-5:10] \' >> $@
	echo ' "out.lngamma.txt" with lines title "ln(Γ(x))" \' >> $@
	echo ',"table.lngamma.txt" with points pt 6 title "tabulated"' >> $@
