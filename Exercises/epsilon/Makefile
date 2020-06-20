CSFLAGS = -optimize-

out.txt : main.exe
	mono $< > $@

main.exe : main.cs IsEqual.dll
	mcs $(CSFLAGS) -reference:IsEqual.dll $<

IsEqual.dll : IsEqual.cs
	mcs -target:library $<

clean:
	rm -f out.txt main.exe IsEqual.dll
