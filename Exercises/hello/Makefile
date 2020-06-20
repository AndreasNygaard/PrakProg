CSFLAGS = -warn:2 -optimize-

all: out.txt

out.txt : main.exe
	mono $< > $@

main.exe : main.cs
	mcs $(CSFLAGS) $<

clean:
	rm -f out.txt main.exe
