all: output.txt

output.txt: main.exe
	mono $< > output.txt 2>log

main.exe: main.cs math.dll
	mcs -r:math.dll $<

math.dll: integrate.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt

