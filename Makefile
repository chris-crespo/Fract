.PHONY: run test

run:
	rm -rf src/UI.Console/bin/
	rm -rf src/UI.Console/obj/
	dotnet run --project src/UI.Console/

test: 
	dotnet test
