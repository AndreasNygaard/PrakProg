all: Gamma.svg

Gamma.svg: gamma.gpi out.gamma.txt
	gnuplot $<

out.gamma.txt: main-gamma.exe
	mono $< > $@ 2> log.gamma.txt

main-gamma.exe: main-gamma.cs math.dll
	mcs $< -r:math.dll $<

#LIBDIR=../../lib
math.dll: gamma.cs #\
#	$(LIBDIR)/math.cs \
#	$(LIBDIR)/integration/quadc7.cs \
#	$(LIBDIR)/integration/quado8.cs \
#	$(LIBDIR)/integration/quado4.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll out* *.svg *.png gamma.gpi log* table* gamma-c-exe

table.gamma.txt: gamma-c-exe
	./$< > $@

gamma-c-exe: gamma.c
	gcc gamma.c -o $@

gamma.gpi: Makefile table.gamma.txt
	cat /dev/null > $@
	echo 'set encoding utf8' >> $@
	echo 'set title "Gamma-function: computed vs. tabulated"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "Gamma.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot [:5][-5:5] \' >> $@
	echo ' "out.gamma.txt" with lines title "Γ(x)" \' >> $@
	echo ',"table.gamma.txt" with points pt 6 title "tabulated"' >> $@
