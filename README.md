
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

(comentarios agregados con posterioridad a la entrega para la mejor comprensión del trabajo)
## Sonidos implementados

### Bola
Movimiento: La gestión de la reproducción de sonidos de movimiento se llevó a cabo mediante la invocación de métodos definidos en el script AKLD_PlayerSoundManager desde el script PlayerManager. 
La variación en el pitch y el volumen se calculó según la velocidad proporcionada por el componente Rigidbody. Este valor se tradujo a un parámetro RTPC de Wwise a través del script AKLD_MovementRTPC. 
Además, existen tres variaciones del sonido del movimiento (Dust/Sand - Grass - Metal)  segun el piso donde se encuentre la bola. El mismo se obtiene gracias al script AKLD_CheckTerrainSwitch, que chequea sobre que terreno se encuentra la bola y cambia un Switch de Wwise.

Impacto:
La gestión de la reproducción de sonidos de impacto sobre superficies se llevó a cabo mediante la invocación de métodos definidos en el script AKLD_PlayerSoundManager desde el script PlayerManager.
El volumen se calculó en base a datos proporcionados por la colisión.
Nuevamente cuenta cuenta con las mismas variaciones según la superficie, a la que se le agregó el agua. Por cuestiones de tiempo no se llegó a implementar este último. 

Salto: 
La gestión de la reproducción de sonidos de salto se llevó a cabo mediante la invocación de métodos definidos en el script AKLD_PlayerSoundManager desde el script PlayerManager.

Caída libre, alta velocidad:
Para el sonido de viento de caida libre o alta velocidad de la bola se hizo uso del mismo rtpc de velocidad utilizado en el movimiento, el cual cambia el volumen del viento en función de dicho valor de velocidad. La reproducción se realiza al comienzo del nivel con un componente AKEvent.

Dash hacia el diamante: 
La gestión de la reproducción de sonidos del dash hacia los diamantes (o ataque según el código) se llevó a cabo mediante la invocación de métodos definidos en el script JewelAudioManager desde el script Jewel. Se lo incluye en la categoria de la bola porque corresponde a un sonido correspondiente a la misma. 

### Diamante
Apuntado: La gestión de la reproducción del sonido de apuntado (cuando el diamante se recuadra) se llevó a cabo mediante la invocación de métodos definidos en el script JewelAudioManager desde el script Jewel. 

Desapuntado: Igual que el anterior, pero se reproduce cuando termina el apuntado.

Choque:La gestión de la reproducción del choque se llevó a cabo mediante la invocación de métodos definidos en el script JewelAudioManager desde el script Jewel.

### Aumento de velocidad
Aumento de velocidad: La gestión de la reproducción del choque se llevó a cabo mediante la invocación de métodos definidos en el script SpeedTriggerAudioManager desde el script SpeedTrigger.

### Música
Musica principal de ambos niveles: Se reproduce con el componente AKEvent de Wwise, pero se modifican los valores de la variable State de Wwise desde el script AKLD_StateAreas. Desde el mismo componente se pueden visualizar, con distintos colores, las zonas donde quien juega debera pasar para que se realicen los cambios en la música, accediendo de forma rápida a comprender el comportamiento musical.
La música se encuentra programada en Wwise mediante resecuenciación horizontal, pero simulando una remezcla vertical. Al comienzo del nivel se escucha una versión con pocos instrumentos, que se van sumando según el progreso del nivel. Al respawnear se vuelve a escuchar la versión con menos instrumentos. De igual forma, si volvemos a una zona anterior, se restarán instrumentos. 
Se cuentan con 5 niveles de intensidad musical. El primer nivel hace uso del 1ero, 3ro y 5to y en el segundo nivel, mucho más largo, se utiizan todos. 

### Bandera de meta
Música de meta: Se inicializa con un AKEvent para reproducirlo. Se escucha cuando la persona jugadora se encuenta cerca de la bandera de meta. Para ello se creó el script AKLD_DistanciaEntreObjetos, que calcula la distancia (funciona entre cualquier dos objetos porque resulta útil para ser utilizado luego en otras implementaciones futuras del juego) de la bola con la bandera y cambia los volumenes de reproducción de ambas musicas según una variable de tipo RTPC de Wwise. 
