QubicaAMF SoftwareDesign Test

Descrizione dell'applicazione originale
Il file Program.cs contiene tutto il codice necessario per creare una console application con 
.NET core, e simula un'applicazione CAD 'elementare'. Nella sua forma originale è possibile:
• creare quattro tipi di forme geometriche, ovvero square, circle, rectangle, triangle
• visualizzare l'insieme della forme geometriche create (opzione 'l'), sotto forma di un 
report che ne riporta tipo e proprietà
• visualizzare l'area totale (opzione 'a'), come somma delle singole aree
Requisiti
Adottando un processo iterativo, viene chiesto di:
• implementare la persistenza dei dati, su diversi media (file system e via http), e in 
diverse formati di serializzazione (custom e json)
• rifattorizzare al meglio l'applicazione al fine di renderla modulare, ovvero separata in 
diversi moduli (progetti/librerie) che soddisfino il Dependency Inversion Principle
• l'attività di sviluppo deve essere svolta creando un repo git (GitHub, Bitbucket), in modo 
tale da poter verificare l’approccio incrementale allo sviluppo
• l'obbiettivo finale è poter avviare l'appliazione con diverse combinazioni di parametri di 
avvio, ovvero (esempi di command line):
o per salvare e leggere i dati in formato custom sul file c:\temp\shapes.txt:
> CadSimulation --path c:\temp\shapes.txt
o per salvare e leggere i dati in formato json sul file c:\temp\shapes.txt:
> CadSimulation --path c:\temp\shapes.txt –-json
o per salvare e leggere i dati in formato custom via http
> CadSimulation --uri http://127.0.0.1:8787/shapes
o per salvare e leggere i dati in formato json via http
> CadSimulation --uri http://127.0.0.1:8787/shapes --json
Roadmap
Implementare i requisiti di cui sopra attraverso le seguenti iterazioni (fare commit frequenti per 
poter analizzare il progress di sviluppo e refactoring)
• Iterazione 0: build applicazione con codice originale e creazione di un repo git
o creare un progetto di tipo 'Console Application .NET Core' utilizzando il file 
Program.cs allegato. Utilizzare Visual Studio (eventualmente Community 
Edition) o Visual Studio Code (versione del framework 8.0)
o avviare l'applicazione e testarla per prenderne dimestichezza; le istruzioni del 
menù dovrebbero essere auto esplicative per capirne il comportamento
• Iterazione 1: aggiungere il supporto alla persistenza su file system dei dati, adottando il 
formato custom specificato nel seguito
o Associare all'opzioni 'k' lo store dei dati in memoria e all'opzione 'w' il relativo 
fetch
o Utilizzare il parametro di avvio --path "file path", per definire il path del file in cui 
salvare e da cui leggere i dati
o i dati devono essere salvati su file testuale, e:
▪ ogni riga rappresenta i dati di una forma geometrica
▪ il primo carattere rappresenta un 'tag' che definisce la forma geometrica, 
secondo la seguente codifica:
• 'S' per square
• 'C' per circle
• 'R' per rectangle
• 'T' per triangle
▪ per square e circle il tag identificativo è seguito da un solo parametro, 
rispettivamente side e radius, mentre per rectangle e triangle il tag è 
seguito da una coppia di parametri, rispettivamente width, height e base, 
height
▪ i valori dei parametri sono interi, non valori decimali
▪ Nella sezione ‘esempi formati’, (*) riporta un esempio di contenuto del 
file in formato custom
o ignorare la gestione degli errori (es: se è richiesto di inserire un intero, assumere 
che l'utente lo inserisca)
• Iterazione 2: aggiungere il supporto per la persistenza su file system in formato json
o se l'applicazione viene avviata col parametro --json, l'app deve utilizzare un 
formato json invece di quello custom
o Nella sezione ‘esempi formati’, (**) riporta un esempio di contenuto in formato 
json
• Iterazione 3: aggiungere il supporto per la persistenza tramite servizio remoto http
o se l'applicazione viene avviata col parametro --url "uri" (es: --url 
http://127.0.0.1:8282/shapes), l'app si deve affidare ad un servizio http per le 
operazioni di persistenza, ovvero fare una POST per lo store e GET per la fetch.
o se come parametro di avvio viene utilizzato sia --path che --uri, quest'ultimo 
viene ignorato (quindi l'app utilizza il file system per la persistenza)
o per la POST, il payload è la stringa che contiene le informazioni da persistere (in 
formato custom o json, a seconda dei parametri di avvio)
o con la GET, l'app recupera dal servizio remoto la stringa contenente le 
informazioni
o note:
▪ utilizzare il file shapes_server.js come server nodejs, avviato in locale, 
per simulare il servizio remoto
▪ la volatilità della persistenza è vincolata al processo node, si assume 
che questo sia sempre attivo
• Iterazione 4: rifattorizzare l'applicazione per renderla modulare
o separare l'applicazione monolitica in diversi progetti, separando i 'moduli' 
(progetti) che contengono le logiche di business/dominio da quelli che 
contengono il codice di infrastruttura tecnologica, ovvero i low level details
necessari per l'interazione con file system, console e network
hint: addottare i dettami della hexagonal architecture
o la dipendenza dei suddetti moduli deve rispettare il Dependency Inversion 
Principle, tramite l'adozione della Dependency Injection
o il numero di file in cui vengono testate le condizioni di partenza, cioè i parametri 
di avvio, deve essere minimo, idealmente uno solo, ovvero il file che contiene il 
Main (in C# con la sintassi Top-Level statements, nel file Program.cs)
