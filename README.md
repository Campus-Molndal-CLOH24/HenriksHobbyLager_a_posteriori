# HenriksHobbyLager

# Arbetsflöde

## Branch-struktur
- **temp/{namn}**: Börja alltid här. Används för utveckling och testning av kod.
- **dev/{namn}**: Hit pushar ni kod som fungerar och är redo att integreras med resten.
- **test**: Gemensam branch för integration och sista testning innan det går till main.
- **dev/test**: Om vi behöver fixa något i test.
- **main**: Produktionsklar och inlämningsfärdig kod.

## Arbetsflöde
1. Börja i din **temp/{namn}**.
2. När koden fungerar, merge:a till **dev/{namn}**.
3. Skapa en pull request från **dev/{namn}** till **test**.
4. Kör gemensamma tester i **test**.
5. Merge:a till **main** när allt är klart och testat.

## Regler för pull requests
- Beskriv vad ändringen gör.
---------

#TODO

README.md ska innehålla:
1. Kort beskrivning av projektet
2. Installationsinstruktioner
3. Hur man kör programmet
4. Eventuella konfigurationsinställningar
5. Lista över implementerade patterns
6. Kort beskrivning av databasstrukturen
