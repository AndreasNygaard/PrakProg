all:report.pdf

report.pdf: report.tex Figure.eps
	pdflatex $<

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

data.txt: main.exe
	mono $< > $@

LIBDIR = ../../matlib
SRC := $(LIBDIR)/matrix/vector.cs
SRC += $(LIBDIR)/ode/ode.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	rm -f *.log *.aux *.pdf *.dvi data* *.eps *.exe *.dll
