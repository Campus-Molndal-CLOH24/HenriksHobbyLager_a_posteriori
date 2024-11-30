## Arbetsflöde

**Regler för commit-meddelanden**

Använd följande format för dina commit-meddelanden:
+ `feat:` för nya funktioner.
+  `fix:` för bugfixar.
+  `refactor:` för omkodning/omstrukturering.
+  `docs:` för ändringar i dokumentation.
Exempel: docs: uppdaterade README.md


### Branch-struktur
- **temp/{name}**  
  Börja alltid här. Använd denna branch för att utveckla och testa dina ändringar tills de fungerar som tänkt.  
  _Se detta som din feature-branch._
  
- **dev/{name}**  
  När koden fungerar i **temp/{name}**, merge:a och pusha till din personliga **dev/{name}**-branch. Vid ytterligare ändringar, arbeta alltid i **temp/{name}** tills det fungerar och merge:a sedan tillbaka till **dev/{name}**.

- **test**  
  Gemensam branch för integration och sista testning. När alla är nöjda med sina ändringar i sina **dev/{name}**-brancher, merge:as de hit för gemensamma tester.

- **dev/test**  
  Om vi behöver göra ändringar i **test**, arbeta här. När ändringarna fungerar, merge:a tillbaka till **test**.

- **main**  
  Här ligger den produktionsklara och inlämningsfärdiga koden. Bara stabil och helt testad kod från **test** merge:as hit.

---

### Kom igång

1. Klona repositoryt:
	```bash
	git clone https://github.com/Campus-Molndal-CLOH24/HenriksHobbyLager_a_posteriori.git
   
3. Gå in i projektmappen:  

		cd HenriksHobbyLager_a_posteriori
  
5. Byt till din temp/{name} branch:  

		git checkout temp/{name}

6. Börja arbeta! Följ instruktionerna nedan när du är redo att committa och pusha.

### När du är klar med din temp/{name}

1. Se till att du är i din temp/{name} branch:  

		git checkout temp/{name}
   
2. Lägg till och committa dina ändringar:  

		git add .
		git commit -m "Ditt commit-meddelande här"
   
4. Byt till din dev/{name} branch:  

		git checkout dev/{name}
   
5. Merge:a ändringarna från temp/{name} till dev/{name}:  

		git merge temp/{name}

6. Pusha din dev/{name} branch till remote:  

		git push origin dev/{name}

Pull Requests (PR)

1.	När du är klar i din dev/{name} branch, skapa en pull request till test.
Lägg till en tydlig beskrivning av vad din kod gör och eventuella tester du gjort.
2.	Låt minst en annan gruppmedlem granska din pull request innan den merge:as.
3.	Merge:a aldrig direkt till test eller main utan en pull request.

## Vanliga Git-kommandon

Hämta senaste ändringarna från remote:

		git pull origin "den-branch-du-vill-hämta"

Visa status för lokala ändringar:

		git status

Visa senaste commits:

		git log --oneline
