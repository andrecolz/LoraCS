> [!IMPORTANT]  
> L'applicazione funziona su tutti i computer recenti dotati di windows, per far funzionare il firmware è necessario un ESP32 o altre board, esso però funzionerà solo con i moduli dotati di chip LLCC68.

# A cosa serve?
Questo programma di messaggistica consente di interfacciarsi con un ESP32 e un modulo lora dotato di chip LLCC68, questo modulo ci permetterà di comunicare con altri utenti aventi lo stesso programma.

# Vantaggi
- args[0] ---> Comunicazione offline senza la necessità di internet
- args[1] ---> Distanza di copertura molto ampia da 1 a più di 10km (in base all'antenna e al posizionamento di essa)
- args[2] ---> Basso costo di realizzazione (meno di 20 euro)

# Svantaggi 
- args[0] ---> Le comunicazioni avvengono in chiaro senza cifratura (implementazione futura)
- args[1] ---> Non è possibile creare gruppi 
- args[2] ---> La rete su cui si basa è peer-to-peer e non mesh
