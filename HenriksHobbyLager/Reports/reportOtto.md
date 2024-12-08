## Hur fungerade gruppens arbete?

Gruppens arbete fungerade bra, men jag tror det kunde blivit mer strukturerat och effektivt om vi hade haft en mer konkret plan för hela gruppen. Det känns även som att det skulle vara gynnsamt med en tydligare "ledare" i gruppen. 
Eftersom den här uppgiften inte var jättestor fungerade upplägget vi hade med att koda på två olika håll, men om det blir större projekt i framtiden tror jag det kommer vara viktigare att köra på en bra planering och tydlig uppdelning. 

## Beskriv gruppens databasimplementation

Vi använde oss av en ConfigReader för att låta användaren bestämma om hen vill använda en SQL eller NoSQL-databas. I en config.txt-fil skriver användaren om det ska vara SQL eller NoSQL, vilken connection string ska användas och efter det är det egentligen bara att köra programmet. 
## Vilka SOLID-principer implementerade ni och hur?

SRP implementeras genom att alla klasser endast har ett ansvarsområde, till exempel ansvarar de olika databasrepository-klasserna för CRUD i sin motsvarande databas. 

OCP följs eftersom det är möjligt att lägga till stöd för nya databassorter utan att störa funktionaliteten för den redan befintliga koden.

LSP följs för att vilken som helst av repository-klasserna kan användas utan att programmets funktionalitet blir annorlunda. Detta på grund av arv och polymorfism.

ISP implementeras på så sätt att inga klasser tvingas implementera funktioner som inte används. Till exempel innehåller IRepository endast CRUD-operationer som används av både SQLite och MongoDB.

DIP är korrekt i programmet eftersom ProductService är beroende av IRepository och inte specifika klasser som SqliteProductRepository eller MongoDbProductRepository. 

## Vilka patterns använde ni och varför?

Vi har några olika patterns i programmet: 
- Repository pattern
- Dependency injection 
- Factory pattern
- Strategy pattern

Vi använde de här mönstren för att göra programmet bland annat mer flexibelt och underhållbart. Dependency injection gör programmet flexibelt eftersom applikationen använder ett speciellt mönster för CRUD-operationer, vilket gör det möjligt att lägga till nya databastyper utan att ändra på någon befintlig kod. Strategy pattern låter oss ändra mellan SQL eller NoSQL utan att ändra på någon kod, dock måste något ändras i config.txt.

## Vilka tekniska utmaningar stötte ni på och hur löste ni dem?

Den största utmaningen för mig var när tillägget av en MongoDB-databas kom. Jag hade mer eller mindre skrivit klart programmet, och fick egentligen börja om från början. 
Det var en liten uppförsbacke i början, men programmet är mycket bättre nu efter omstruktureringen. Jag lärde mig väldigt mycket av att göra om koden. Det mest värdefulla som har kommit från den här uppgiften är att verkligen få se hur OOP implementeras, med koncept som Factory Pattern och Dependency Injection. 
Det skulle säkert vara möjligt att skriva programmet utan dessa, och bara skapa två instanser av databaser, en SQL och en NoSQL. Men jag blev väldigt nöjd med resultatet och att det funkade så bra.

## Hur planerade du ditt arbete?

Jag planerade mitt arbete genom att först och främst gå igenom uppgiften och försöka förstå vilka mål som ska mötas. Efter det började jag helt enkelt jobba med koden. Skrev en kort TODO-lista i VS och följde den. 


## Vilka dela gjorde du?

Vi båda gjorde hela koden på skilda håll. Alltså gjorde jag alla delar, men det gjorde även min gruppkamrat. Det var intressant att se hur olika våra lösningar var. Koden som lämnas in är i stor del inte den koden jag har skrivit, och är helt ärligt mycket snyggare än koden jag skrev. Min lösning fungerade, men inte lika elegant och SOLID som programmet som skickas in. 

## Vilka utmaningar stötte du på och hur löste du dem?

Eftersom jag är fortfarande relativt ny till kodning, speciellt c# och extra speciellt objektorienterad programmering, var det svårt att veta hur man ska hantera en större gruppuppgift. Under projektets gång har jag lärt mig en del om hur annorlunda två kodares kod kan se ut, hur det är att jobba fram kod med en annan person och hur lärorikt det är att ta del av andras arbete.

## Vad skulle du göra annorlunda nästa gång?

Nästa gång skulle jag lagt upp en bättre och tydligare plan för hur arbetet ska utföras, och sätta delmål längs med projektets gång. 
Jag kommer lägga mer tid och fokus på delmål nästa gång, för jag tror att det kan leda till en slutprodukt som jag känner mig mer nöjd med, inte för att jag inte är nöjd med hur programmet har blivit nu, dock känns det som att jag kunde ha gjort mer. 

Och ta betalt av Henke.
