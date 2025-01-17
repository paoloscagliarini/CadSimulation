<b>QubicaAMF SoftwareDesign Test</b>

<p>Descrizione dell'applicazione originale<br />
Il file Program.cs contiene tutto il codice necessario per creare una console application con .NET core, e simula un'applicazione CAD 'elementare'. Nella sua forma originale è possibile:<br />
<ul>
<li>creare quattro tipi di forme geometriche, ovvero square, circle, rectangle, triangle</li>
<li>visualizzare l'insieme della forme geometriche create (opzione 'l'), sotto forma di un report che ne riporta tipo e proprietà</li>
<li>visualizzare l'area totale (opzione 'a'), come somma delle singole aree</li>
</ul>
</p>
<p>Requisiti<br />
Adottando un processo iterativo, viene chiesto di:<br />
<ul>
<li>implementare la persistenza dei dati, su diversi media (file system e via http), e in diverse formati di serializzazione (custom e json)</li>
<li>rifattorizzare al meglio l'applicazione al fine di renderla modulare, ovvero separata in diversi moduli (progetti/librerie) che soddisfino il Dependency Inversion Principle</li>
<li>l'attività di sviluppo deve essere svolta creando un repo git (GitHub, Bitbucket), in modo tale da poter verificare l’approccio incrementale allo sviluppo</li>
<li>l'obbiettivo finale è poter avviare l'appliazione con diverse combinazioni di parametri di avvio, ovvero (esempi di command line):</li>
<ul>
<li>per salvare e leggere i dati in formato custom sul file c:\temp\shapes.txt: CadSimulation --path c:\temp\shapes.txt</li>
<li>per salvare e leggere i dati in formato json sul file c:\temp\shapes.txt: CadSimulation --path c:\temp\shapes.txt –-json</li>
<li>per salvare e leggere i dati in formato custom via http: CadSimulation --uri http://127.0.0.1:8787/shapes</li>
<li>per salvare e leggere i dati in formato json via http: CadSimulation --uri http://127.0.0.1:8787/shapes --json</li>
</ul>
</ul>
</p>
<p>Roadmap</br />
Implementare i requisiti di cui sopra attraverso le seguenti iterazioni (fare commit frequenti per poter analizzare il progress di sviluppo e refactoring)
<ul>
<li>Iterazione 0: build applicazione con codice originale e creazione di un repo git</li>
<ul>
<li>creare un progetto di tipo 'Console Application .NET Core' utilizzando il file Program.cs allegato. Utilizzare Visual Studio (eventualmente Community Edition) o Visual Studio Code versione del framework 8.0) o avviare l'applicazione e testarla per prenderne dimestichezza; le istruzioni del  menù dovrebbero essere auto esplicative per capirne il comportamento</li>
</ul>
<li>Iterazione 1: aggiungere il supporto alla persistenza su file system dei dati, adottando il formato custom specificato nel seguito</li>
<ul>
<li>Associare all'opzioni 'k' lo store dei dati in memoria e all'opzione 'w' il relativo fetch</li>
<li>Utilizzare il parametro di avvio --path "file path", per definire il path del file in cui  salvare e da cui leggere i dati</li>
<li>i dati devono essere salvati su file testuale, e:</li>
<ul>
<li>ogni riga rappresenta i dati di una forma geometrica</li>
<li>il primo carattere rappresenta un 'tag' che definisce la forma geometrica, secondo la seguente codifica:</li>
<ul>
<li>'S' per square</li>
<li>'C' per circle</li>
<li>'R' per rectangle</li>
<li>'T' per triangle</li>
<li>per square e circle il tag identificativo è seguito da un solo parametro, rispettivamente side e radius, mentre per rectangle e triangle il tag è seguito da una coppia di parametri, rispettivamente width, height e base, height</li>
<li>i valori dei parametri sono interi, non valori decimali</li>
<li>Nella sezione ‘esempi formati’, (*) riporta un esempio di contenuto del file in formato custom</li>
<ul>
<li>ignorare la gestione degli errori (es: se è richiesto di inserire un intero, assumere che l'utente lo inserisca)</li>
</ul>
</ul>
</ul>
</ul>
<li>Iterazione 2: aggiungere il supporto per la persistenza su file system in formato json</li>
<ul>
<li>se l'applicazione viene avviata col parametro --json, l'app deve utilizzare un formato json invece di quello custom</li>
<li>Nella sezione ‘esempi formati’, (**) riporta un esempio di contenuto in formato json</li>
</ul>
<li>Iterazione 3: aggiungere il supporto per la persistenza tramite servizio remoto http</li>
<ul>
<li>se l'applicazione viene avviata col parametro --url "uri" (es: --url http://127.0.0.1:8282/shapes), l'app si deve affidare ad un servizio http per le operazioni di persistenza, ovvero fare una POST per lo store e GET per la fetch.</li>
<li>se come parametro di avvio viene utilizzato sia --path che --uri, quest'ultimo viene ignorato (quindi l'app utilizza il file system per la persistenza)<&li>
<li>per la POST, il payload è la stringa che contiene le informazioni da persistere (in formato custom o json, a seconda dei parametri di avvio)
<li>con la GET, l'app recupera dal servizio remoto la stringa contenente le informazioni</li>
<li>note:</li>
<ul>
<li>utilizzare il file shapes_server.js come server nodejs, avviato in locale, per simulare il servizio remoto</li>
<li>la volatilità della persistenza è vincolata al processo node, si assume che questo sia sempre attivo</li>
</ul>
</ul>
<li>Iterazione 4: rifattorizzare l'applicazione per renderla modulare</li>
<ul>
<li>separare l'applicazione monolitica in diversi progetti, separando i 'moduli' (progetti) che contengono le logiche di business/dominio da quelli che contengono il codice di infrastruttura tecnologica, ovvero i low level details necessari per l'interazione con file system, console e network hint: addottare i dettami della hexagonal architecture</li>
<li>la dipendenza dei suddetti moduli deve rispettare il Dependency Inversion Principle, tramite l'adozione della Dependency Injection</li>
<li>il numero di file in cui vengono testate le condizioni di partenza, cioè i parametri di avvio, deve essere minimo, idealmente uno solo, ovvero il file che contiene il Main (in C# con la sintassi Top-Level statements, nel file Program.cs)</lI>
</ul>
