all: Error.svg

Error.svg: error.gpi out.error.txt
	gnuplot $<

out.error.txt: main-error.exe
	mono $< > $@ 2> log.error.txt

main-error.exe: main-error.cs math.dll
	mcs $< -r:math.dll $<

#LIBDIR=../../lib
math.dll: error.cs #\
#	$(LIBDIR)/math.cs \
#	$(LIBDIR)/integration/quadc7.cs \
#	$(LIBDIR)/integration/quado8.cs \
#	$(LIBDIR)/integration/quado4.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll out* *.svg *.png error.gpi log* table* error-c-exe

table.error.txt: error-c-exe
	./$< > $@

error-c-exe: error.c
	gcc error.c -o $@

error.gpi: Makefile table.error.txt
	cat /dev/null > $@
	echo 'set encoding utf8' >> $@
	echo 'set title "Error-function: computed vs. tabulated"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "Error.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot [:5][-5:5] \' >> $@
	echo ' "out.error.txt" with lines title "Î“(x)" \' >> $@
	echo ',"table.error.txt" with points pt 6 title "tabulated"' >> $@
