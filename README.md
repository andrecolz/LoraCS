> [!IMPORTANT]  
> L'applicazione funziona su tutti i computer recenti dotati di windows, per far funzionare il firmware è necessario un ESP32 o altre board, esso però funzionerà solo con i moduli dotati di chip LLCC68.

# A cosa serve?
Questo programma di messaggistica consente di interfacciarsi con un ESP32 e un modulo lora dotato di chip LLCC68, questo modulo ci permetterà di comunicare con altri utenti aventi lo stesso programma.

# Funzionamento APP
L'app funzionerà solo se connessa al ESP32, la prima volta che verrà avviata verranno chiesti:
- nome -> il nome che apparirà al destinatario
- ADDL -> varia da 1 a 255 
- ADDH -> varia da 1 a 255
- CHAN -> su che canale si desidera effettuare la comunicazione
ADDL e ADDH è come se rappresentassero il nostro indirizzo, questo indirizzo non è univoco infatti può essere scelto anche da altre persone, per evitare problemi è opportunuo non scegliere indirizzi semplici.

# Come aggiungere un contatto
Per aggiungere un contatto basterà cliccare su "ADD", si aprirà una schermata dove verranno chieste le seguenti informazioni ADDL, ADDH, CHAN, ovviamente inseriremo le informazioni che il nostro contatto ha impostato nella sua applicazione.
Una volta fatto verrà aggiunto il nostro contatto, l'interfaccia semplice e intuitiva ci consente di capire subito il funzionamento del programma, ora siamo pronti per inviare messaggi!

# Vantaggi
- Comunicazione offline senza la necessità di internet
- Distanza di copertura molto ampia da 1 a più di 10km (in base all'antenna e al posizionamento di essa)
- Basso costo di realizzazione (meno di 20 euro)

# Svantaggi 
- Le comunicazioni avvengono in chiaro senza cifratura (implementazione futura)
- Non è possibile creare gruppi 
- La rete su cui si basa è peer-to-peer e non mesh
- Danneggiamento dei pacchetti (non sempre i messaggi arrivano a destinazione se la distanza è elevata)
- Non ho ancora creato un'app per android

# La board da me realizzata
<img src="https://i.ibb.co/YyVDNQ3/20240514-163601.jpg" alt="20240514-163601" border="0">
Ho realizzato due di queste board per testare il funzionamento, nel mio caso i mie ESP32 erano dotati di display anche se non è necessario.
