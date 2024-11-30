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

Henrik bad om att få förbättrad kod och framförallt ett bättre sätt att lagra datan om sina produkter på. 
Projektet fungerar genom att läsa en textfil, config.txt, för att förstå om programmet ska kopplas upp mot en SQL-databas eller en NoSQL-databas.
Efter detta fungerar Henriks meny i princip på samma sätt som när han sparade all data i en konstant öppen lista, skillnaden är att han kan äntligen stänga av sin dator!


2. Installationsinstruktioner




3. Hur man kör programmet

Starta applikationen, kör igång programmet och följ menyn!

4. Eventuella konfigurationsinställningar

Navigera till C:\DärDuSparar\VisualStudioRepos\HenriksHobbyLager_a_posteriori\HenriksHobbyLager\bin\Debug\net8.0
och öppna filen config som ett vanligt textdokument (i Windows med programmet Anteckningar t.ex), därefter ändra efter = vid "type=", skriv SQL för en SQLite-databas, eller NoSQL för en MongoDB. 
Stäng programmet och tryck på "spara" när prompten kommer upp. 

5. Lista över implementerade patterns

+ Factory Pattern
  Används för att skapa rätt typ av databas, antingen SQLite eller MongoDB. DatabaseFactory hanterar detta och gör det enkelt att byta mellan olika databaser. Det bidrar till en flexibel och skalbar lösning.
+ Repository Pattern
  Gör att all hantering av data, som att lägga till, hämta och ta bort produkter, sker via en tydlig struktur. IRepository och ProductFacade används för detta. Det förenklar kodunderhållet och återanvändning av logik.
+ Dependency Injection
  Gör att databasen (IDatabase) skickas in i andra klasser, som Menu, istället för att de själva skapar den. Det gör koden enklare att ändra och testa. Dessutom blir det lättare att byta databas om det behövs.
+ Facade Pattern
  ProductFacade samlar alla produktoperationer i en enda klass. Det gör att andra delar av programmet inte behöver veta hur produkterna hanteras i databasen. Detta skapar en tydligare struktur i koden.
+ Singleton Pattern
  Ser till att bara en databasinstans skapas under körning. Detta görs indirekt via DatabaseFactory. Det förhindrar onödiga dupliceringar av databasanslutningar.
+ Strategy Pattern
  Används för att välja om SQLite eller MongoDB ska användas, beroende på vad som är inställt i konfigurationen. Detta val hanteras av DatabaseFactory. Det gör systemet flexibelt för olika behov.

6. Kort beskrivning av databasstrukturen
Tabellen för produkter innehåller egenskaper som Id, Name, Price, Stock och Category. Databasen kan vara antingen en relationsdatabas (SQLite) eller en dokumentdatabas (MongoDB), beroende på vilken typ som väljs vid körning. Operationer som att lägga till, uppdatera, söka och ta bort produkter hanteras via ett gränssnitt (IDatabase).
Programmet är byggt så man säkerställer att all produktdata hanteras konsekvent oavsett databastyp.
