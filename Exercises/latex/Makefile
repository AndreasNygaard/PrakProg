all:report.pdf

report.pdf: report.tex Plot.png
	pdflatex $<
	pdflatex $<

Plot.png: plot.gpi out.gamma.txt table.gamma.txt
	gnuplot $<

LIBDIR = ../../lib
SRC := $(LIBDIR)/matrix/vector.cs
SRC += $(LIBDIR)/integration/quado8.cs
math.dll: $(SRC) gamma.cs
	mcs $^ -target:library -out:$@

out.gamma.txt: main-gamma.exe
	mono $< > $@ 2> log.gamma.txt

main-gamma.exe: main-gamma.cs math.dll
	mcs $< -r:math.dll $<

table.gamma.txt: gamma-c-exe
	./$< > $@

gamma-c-exe: gamma.c
	gcc gamma.c -o $@

clean:
	$(RM) *.log *.aux *.pdf *.dvi *.png plot-exp.tex *-exe *.exe *.dll *.txt *.out

test:
	echo $(SRC)
