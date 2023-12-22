
# wwise-integration by Lautaro Dichio

## Sound playtest.

[Marble Game With sound By Lautaro Dichio](https://youtu.be/Y8AyiaK1FLk)

## Documentación

Están establecidas las bases generales para implementar y ampliar el diseño de sonido general del juego.

Se priorizó la programación e implementación de audio. En una siguiente etapa se trabajaría sobre la creación de assets de audio y música idóneos para el juego, mejorar la mezcla y pulido del diseño sonoro. 

## Scripts implementados

Dentro de cada script se pueden encontrar documentacion sobre el funcionamiento de cada uno, pero igualmente, a continuacion se listan:

### AKLD_StateAreas 
 Define áreas para enviar valores de estado wwise para una rápida visualización y seguimiento dentro de la escena.  

### AKLD_CheckTerrainSwitch
Chequeo del terreno donde se encuentra la bola para cambiar los sonidos asociados a la misma. 

### AKLD_DistanciaEntreObjetos
Cálculo de la distancia entre dos objetos en un eje y envío del valor calculado como un RTPC (Real-Time Parameter Control) a Wwise.

### AKLD_EventArea
Crea Áreas que activan eventos wwise.

### AKLD_MovementRTPC
calcula la velocidad del jugador basado en rigidbody y crea rtpc para enviar a wwise.

### AKLD_PlayerSoundManager
Este script se encarga de controlar todos los eventos wwise relacionados con el movimiento de la bola. 

### JewelAudioManager
Este script se encarga de controlar los eventos de wwise relacionados con los Jewels.

### SpeedTriggerAudioManager
Reproducción del sonido de aumento de velocidad. 

### AKLD_SplashWater
Script para el sonido del impacto de la bola en el agua, por cuestiones de tiempo no se pudo terminar de resolver.

