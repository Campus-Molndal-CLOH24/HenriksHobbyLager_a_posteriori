# Individuell Rapport 

Svara på frågorna nedan och lämna in det som en del av din inlämning.

## Hur fungerade gruppens arbete?
Jag och och Otto bestämde oss för i början för att vi båda ville göra hela uppgiften för oss själva då det är bra träning. 
Vi gav varandra tips och exempel på hur den ena hade löst nånting osv. Vi körde kommunikation och uppdateringar genom Discord chatt och voice och commits på Git.

Vi satta våra egna deadlines på oss själva. Det fungerade bra, Jag är mer än nöjd med min egna och Ottos insats.

Slutgiltiga versionen av koden är en vidare utveckling av Ottos kod grund som vi båda har sen har utvecklat, testat, strukturerat.

## Beskriv gruppens databasimplementation
Databasimplementation bygger på en flexibel design som tillåter användning av både SQLite och MongoDB.
Detta uppnåddes genom att implementera Repository Pattern, där vi skapade separata klasser för varje databas, till exempel SqliteProductRepository och MongoDbProductRepository
Databashanteringen konfigurerades via config.txt-fil och hanteras i klassen DatabaseConfiguration.
CRUD-operationer sköttes i respektive repository med hjälp av metoder från EFCore och MongoDb.Driver som Add, Update, Delete, och GetAll.

## Vilka SOLID-principer implementerade ni och hur?
Single Responsibility Principle (SRP): Varje klass hade ett tydligt ansvar.
Dependency Inversion Principle (DIP): Vi använde interfaces som IRepository och IProductService för att abstrahera beroenden. Affärslogiken använde abstraktioner istället för specifika implementationer.
Open/Closed Principle (OCP): Användning av Repository Pattern och Facade Pattern gjorde att koden var öppen för utökning och stängd för ändringar.

## Vilka patterns använde ni och varför?
Repository Pattern: För att separera databaslogik från affärslogik
Dependency Injection: Genom att injicera beroenden via konstruktorer kunde vi enkelt byta ut repositories och services.
Factory Pattern: Användes i DatabaseConfiguration för att skapa rätt repository beroende på användarens val.

## Vilka tekniska utmaningar stötte ni på och hur löste ni dem?
En utmaning var DatabaseConfiguration, med hur strukturen skulle se ut och hur den skulle bete sig och vad som hämtar vad från andra classer.

## Hur planerade du ditt arbete?
Följde backlog, Störde mig på en grej, löste den, med den lösningen uppstod nya problem, löste dem osv.

## Vilka delar gjorde du?
Vi båda arbetade med alla delar av projektet. Vi strukturerade om och förbättrade varandras kod, vilket gör det svårt att exakt peka ut vem som gjorde vad.

## Vilka utmaningar stötte du på och hur löste du dem?
Förstå MongoDB:s datamodell och syntax. Löste det genom att läsa dokumentation, kolla youtube, ChatGPT för att se syntax.

## Vad skulle du göra annorlunda nästa gång?
Ta betalt av Henke
Ta reda på mer innan man bara börjar skriva massa kod och ändra. mycket MYCKET trail and error.
Läs dokumentation!!!