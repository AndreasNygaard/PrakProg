all: out.txt

out.txt: main.exe input.txt
	mono $< $$(cat input.txt) > out.txt

main.exe: main.cs
	mcs $<

clean:
	rm -f main.exe out.txt
