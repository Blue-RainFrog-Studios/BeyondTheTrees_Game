# GAME DESIGN DOCUMENT: BEYOND THE TREES
# BEYOND THE TREES
## Blue RainFrog Studios

### Grupo D
### Alejandro Cavero, Álvaro Lozano, Daniel Capilla, Daniel Gárate, Javier Barriga, Luis Mateos

### ÍNDICE
1. INTRODUCCIÓN
    * Concepto del juego	
    * Historia	
    * Género	
    * Estilo visual	
    * Pensamiento computacional
    * Jugabilidad
    * Economía
    * Plataforma
    * Requisitos mínimos

2. MECÁNICAS	
    * Movimiento	
    * Ataque	
    * Inventario	
    * Equipamiento	
      * Armas	
    * Pociones	
    * Objetos
      * Ejemplos Objetos	
3. DISEÑO	
    * Personaje	
    * Niveles
       * Referencias visuales
    * Enemigos
    * Jefes
    * Campamento Base
       * Casa
       * Tablón de misiones
       * Tienda de Objetos
       * Puesto del curtidor
       * Cabaña esotérica
       * Carro Ambulante   
4. ARTE	
  * Interfaces	
    * Flujo de Pantallas	
    * Menú Principal	
    * Créditos	
    * Opciones	
    * Login	
    * En Juego
    * Inventario	
    * Victoria/Derrota	
  * Bocetos	
  * Arte conceptual
     * Turn arround personaje
  * Sprites	
  * Banda Sonora
    
5. ALCANCE	
    * Público objetivo
       * ¿Quién va a comprar el juego?
       * ¿Quién va a jugar el juego?
    * Publicidad y redes sociales	
    * Modelo de Negocio
       * Mapa de Empatía
       * ToolBox
       * Model Canvas
    * Planes de monetización
      
6. ANÁLISIS MDA
   * Mecánicas
   * Dinámicas
   * Estéticas
     
7. POST MORTEM
   * Grupal
      * ¿Qué ha ido bien?
      * Flujo de correcciones
      * Feedback externo
      * ¿Qué se podría haber mejorado?   
   * Individuales
     
8. CAMBIOS	
   

### INTRODUCCIÓN
#### Nota de esta version del GDD
En esta version del documento faltan imágenes que si están presentes en el documento original, este documento está disposible para examinar al final del README. Lamentamos las molestias
#### Concepto del juego
Beyond the trees es un Roguelike de ambientación fantástica. El objetivo del juego es llegar a lo más profundo del bosque para descubrir qué esconde este.
Para conseguirlo, nuestro jugador se deberá valer de sus armas y equipamiento.
Este, deberá descubrir los secretos del bosque, descubrir las estratégias que elaboran las criaturas que lo habitan, valerse de su ingenio para resolver puzzles y gestionar sus recursos de forma eficiente para llegar hasta lo más profundo.
El bosque cambiará con cada expedición y cuanto más te adentres en él tanto los enemigos como el escenario se volverán más tenebrosos y oscuros.

#### Historia
El mundo de Beyond the Trees está caracterizado por el enorme bosque de Evergreen, un gigantesco bosque que nunca se ha conseguido atravesar y aquellos que lo han intentado nunca han regresado, por ello,  toda la civilización conocida se ha construido alrededor del bosque del cual los  habitantes de este mundo extraen todos los recursos que necesitan.
El bosque se lleva explorando años pero todavía nadie ha llegado al fondo del bosque, se saben pocas cosas. El bosque está habitado por todo tipo de criaturas, animales salvajes, fantasmas y monstruos. El bosque tiene tres capas conocidas, cuanto más profunda es la capa más fuertes y siniestros se vuelven los monstruos, esto se debe a la llamada maldición de Nahum, el que se dice que fue el creador del bosque. Dicha maldición también provoca que los objetos que se encuentran en el bosque adquieran propiedades mágicas que fortalecen a aquellos que se hacen con ellos, sin embargo, al salir del bosque esta magia se desvanece y hace que esos objetos vuelvan a ser normales. 
Los habitantes de este mundo son los lamins, animales antropomórficos que han hecho de su modo de vida el adentrarse en el bosque para extraer los recursos para levantar su civilización. 
Dentro de esta sociedad hay multitud de trabajos pero el más vital es el de explorador, son los valientes lamins que se adentran al bosque para cazar y hacerse con los recursos necesarios para los asentamientos. Con el tiempo se han formado distintos gremios de exploradores, cada uno con su particular estilo de lucha para enfrentarse a las criaturas del bosque. Hay varias razones para convertirse en explorador: proveer a las familias, dinero, gloria. Por ello cada año llegan a cada asentamiento nuevos candidatos a explorador con ganas de ganarse un nombre adentrándose en el bosque para desentrañar sus secretos.
#### Género
Roguelike con mapa lineal y toques RPG (estadísticas y equipamiento)
#### Estilo visual
Se ha escogido un estilo “cartoon” desenfadado con trazo grueso, similar a juegos como Isaac o Brotato.

![r1][r1]

https://store.steampowered.com/app/113200/The_Binding_of_Isaac/
https://store.steampowered.com/app/1942280/Brotato/

Influencias estéticas de series como Más allá del Jardín u Hora de Aventuras para crear un mundo de fantasía no realista pero si reconocible.

![r2][r2]

https://www.filmaffinity.com/es/film896033.html
https://en.wikipedia.org/wiki/Adventure_Time

Cada enemigo contará con tres versiones distintas, para cada capa del bosque. Los enemigos en la capa 1 tendrán un aspecto normal, lobos y monstruos comunes; en la capa 2 estos monstruos se verán más feroces, colmillos más afilados, pelaje más erizado; y en la capa 3 estos tendrán elementos de terror, partes del cuerpo mordidas, pupilas en blanco, etc…

El cambio a medida que te adentras en el bosque también se verá en el escenario, volviéndose cada vez más tétrico y oscuro con elementos más tenebrosos. Se puede ver en este ejemplo de la serie Hora de Aventuras.

#### Pensamiento computacional

Se deberán monitorizar y evaluar la capacidad de pensamiento computacional de los jugadores, para un posterior tratado de los datos.
Destrezas de pensamiento computacional y cómo se pueden medir:

1. **Abstracción**. Hacer que los enemigos tengan distintos tipos de debilidades y que sea necesario usar un tipo de arma concreto para poder acabar con ellos de forma eficiente. Se puede medir la cantidad de golpes efectivos e inefectivos que se han hecho y utilizarlo como una métrica para saber si el jugador reconoce los patrones de diseño de los personajes. Esto está pensado para ser implementado en futuras versiones del videojuego

2. **Pensamiento algorítmico y reconocimiento de patrones**. Los enemigos serán capaces de hacer formaciones entre ellos, que harán que haya una forma óptima de poder limpiar cada nivel durante cada expedición. Se puede hacer una pila y determinar el orden en el que el jugador acaba con los enemigos y si es el adecuado o evaluarlo.

3. **Descomposición**. Se diseñan salas que además de enemigos cuentan con puzzles y se mide el porcentaje de jugadores que han completado el puzzle antes de matar a todos los enemigos (o en el orden que se haya diseñado). El objetivo es medir si el jugador es capaz de descomponer los problemas y solucionarlos uno a uno o intenta hacer todo al mismo tiempo.

4. **Evaluación**. Se guarda el número de objetos que se han recogido durante la partida, los objetos con los que se acaba la partida, la suma de las estadísticas de esos objetos, la mayor suma de estadísticas que se podría haber conseguido y el número de espacios vacíos en el inventario al acabar la partida. Con esto se puede medir cómo de bien optimiza los recursos el jugador y se puede mostrar al jugador cual podría haber sido la mejor opción, se implementarán partidas con desafíos que especificarán al jugador objetivos como ganar el máximo de oro u obtener el máximo daño posible, si al final de la partida el resultado de el algoritmo creado coincide con el orden del jugador se le recompensará. Con eso se medirá como de bien se ajusta el jugador en función del objetivo. También se va a hacer un seguimiento de la mejora del jugador durante todo su paso por el videojuego, para esto se va a medir el tiempo que tarda el jugador en ganar la partida, daño recibido y oro recibido durante esos desafíos.

5. **Análisis de datos**. El jugador debe gestionar adecuadamente las divisas y sus recursos para obtener nuevo equipo y poder progresar más rápidamente.

6. **Generalización**. Implementación de niveles con puzles donde el jugador tendrá que reconocer objetos mediante su color y tomar decisiones en base a los conocimientos previamente adquiridos en las anteriores partidas. En el campamento (zona previa a las partidas), al jugador se le explicará lo básico y a medida que vaya descubriendo esos patrones, estos se irán haciendo más difíciles.

### Jugabilidad
El bucle jugable general consistirá en los siguientes pasos:
1. Expedición, el jugador entra en el bosque e intenta llegar lo más profundo posible para conseguir la mayor cantidad de recursos. El jugador puede abandonar el bosque al terminar un nivel, en ese caso conservará todos los recursos que haya conseguido de la expedición,  o ser derrotado, en ese caso perderá todos los recursos adquiridos durante la partida.
2. Al terminar la expedición todos los recursos del jugador se convertirán en oro. El jugador volverá al campamento base donde podrá usar el oro para comprar mejoras, objetos y decoración para el campamento.
3. El jugador se acerca a la entrada del bosque para empezar una nueva expedición.

El bucle jugable durante las Expediciones sigue los siguientes pasos.
1. El jugador entra en el primer nivel de la primera capa del bosque.
2. Empieza el combate contra los enemigos del nivel, combate que debería completarse de forma óptima teniendo en cuenta los enemigos que conforman la sala y la forma de esta. El juego evaluará  en una escala de  C->B->A->S->S+, en función de la puntuación se le dará la cantidad y calidad de objetos correspondiente. Los objetos que encuentre se guardarán en el inventario. Este es limitado por lo que el jugador deberá decidir qué objetos
llevar y cuales no.
3. El jugador puede salir del bosque para volver al Campamento Base o puede pasar al siguiente nivel de la capa del bosque en la que este.
5. Si sigue avanzando durante 10 niveles se enfrentará a un jefe de la capa, si lo vence podrá continuar a la siguiente capa y repetir el proceso con enemigos más poderosos para conseguir mejores materiales.

### Economía
El juego cuenta con una única divisa, el oro. El oro se puede obtener al completar expediciones, durante la partida. Sirve para desbloquear equipamiento y objetos en el campamento haciendo que el jugador los pueda encontrar en las expediciones. Además el jugador contará con una tienda por cada capa del bosque durante la expedición. En esta tienda el jugador podrá comprar objetos para usarlos durante la expedición o vender objetos para ganar dinero. El jugador deberá calcular cual es la mejor combinación de compraventa de objetos para sacar el máximo partido a sus recursos.
### Plataforma
El videojuego se va a subir a la plataforma de Itch.io y a la Play Store. En un futuro se contempla subirlo a plataformas como Steam y Epic Games en función del recibimiento del videojuego.
### Requisitos Mínimos
Pese a que no se cuentan con herramientas para calcular el rendimiento del videojuego en los dispositivos, se estima:
- Sistema operativo: Windows 7/8/10 o macOS
- Procesador: Un procesador de doble núcleo a 2.0 GHz o superior
- Memoria RAM: 2 GB de RAM o más
- Tarjeta gráfica: Integrada
- Espacio en disco duro: 1-3 GB
Estas estimaciones se han hecho en función a videojuegos con el mismo estilo al nuestro como el Brotato o The Binding of Isaac.
## MECÁNICAS
### Movimiento
El movimiento del juego será en 8 direcciones.
Según la cantidad de objetos que lleve el jugador la velocidad podrá aumentar o reducir.

### Ataque
El jugador podrá atacar independientemente de la dirección a la que se esté moviendo, se ataca moviendo el joystick en la dirección en la que se quiera atacar, en PC se ataca con las flechas del teclado en 4 direcciones. 
Las propiedades de ataque están determinadas por el arma equipada, este podrá ser cuerpo a cuerpo o a distancia.

### Inventario
El inventario es un espacio dividido en cuadrículas, el inventario puede contener todos los objetos del juego, estos objetos pueden colocarse y organizarse en el inventario. Si sobresalen objetos del inventario, dependiendo de cuántas casillas sobresalen, el personaje se verá ralentizado un 10% por cada casilla de objeto que sobresalga del inventario.
El inventario puede variar en función de las mejoras adquiridas en el Campamento Base o mediante clases proporcionadas por los gremios.
El inventario tiene una serie de huecos limitados, una vez se han llenado esos huecos  se podrán llevar algunos objetos extra pero estos contarán como sobrecarga y llevarlos implicará una reducción de la velocidad de movimiento, no se podrán llevar más objetos una vez superados los huecos de sobrecarga. Por ejemplo, el jugador tiene 10 huecos de inventario, 7 son normales y 3 de sobrecarga, si el jugador lleva 7 objetos tiene el límite antes de tener sobrecarga, si lleva 8 objetos obtendrá una penalización de sobrecarga, reduciendo su velocidad de movimiento un 10% y si lleva 9 recibirá una penalización de 20%, el jugador no puede llevar más de 10 objetos.

### Equipamiento
Armas
Las armas determinan cómo ataca el personaje y el daño que hace. Hay dos tipos principales de armas a distancia y cuerpo a cuerpo.

|                     | Arco      | Ballesta  | Cuchillos arrojadizos | Espada corta | Espada larga | Puñal   |
|---------------------|-----------|-----------|-----------------------|--------------|--------------|---------|
| Tipo de ataque      | Distancia | Distancia | Distancia             | CaC          | CaC          | CaC     |
| Hitbox              | Mediana   | Mediana   | Pequeña               | Mediana      | Mediana      | Pequeña |
| Daño                | Mediano   | Alto      | Bajo                  | Mediano      | Alto         | Bajo    |
| Velocidad de ataque | Mediano   | Lento     | Rápido                | Mediano      | Lento        | Rápido  |

### Pociones
Las pociones servirán para curar la vida, al consumirlas el jugador se deberá quedar quieto brevemente y hacer una animación para poder curarse. 
Para usar una poción se deberá hacer desde el menú de inventario, una vez usado se recuperará una cantidad de vida acorde a la calidad de las pociones.
Las pociones no se podrán usar durante el combate, solo cuando se haya acabado con los enemigos de la sala o el jugador esté en una sala de tienda.

### Objetos
Los objetos pueden aparecer al terminar  una sala y servirán para aumentar estadísticas y en algunos casos alterarán aspectos del personaje como el tipo de daño que hace o los estados que puede infligir, en versiones posteriores se implementarán más atributos que puedan dar los objetos, como sangrado, veneno, quemaduras…
Los objetos tienen 4 **rarezas**: común, raro, épico, legendario. La rareza de los objetos determinará las stats que da al jugador y el precio de venta, además, los objetos de rareza superiores le podrán dar al jugador más de una estadística. 


Más adelante en el juego, cuando el jugador desbloqueé la capacidad de ver auras, podrá ver el aura de los objetos en el bosque, este aura afecta al precio de venta y su daño y determina el estado del objeto en 5 categorías: Maltrecho, - 5% de su precio ; sin aura, tiene el precio de venta y estadísticas normal ; buen estado, + 10% de su precio de venta y estadísticas; perfecto estado, +15% de precio de venta y estadísticas; purificado, +20% de su precio de venta, estadísticas y un efecto sumado aleatorio. Estos porcentajes son orientativos y cambiarán cuando se haya testeado si impactan mucho o poco el gameplay.
Enlace a la hoja de cálculo con los objetos: https://docs.google.com/spreadsheets/d/1nqJ6MIjXAtS5HyprhY6oJlgkHAHZMD0HF10bQ-MYcy4/edit?usp=sharing


A = Ataque, D=Defensa, VA=Velocidad de Ataque, VM=Velocidad de Movimiento, R=Rareza y P = Precio 


| Nombre           | Descripción                                                                                                                                                                             | A  | D  | VA | VM | R          | P  |
|------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----|----|----|----|------------|----|
| Daga usada       | Ha perdido mucho de su filo pero todavía cumple su función, se puede vender por un poco de oro                                                                                          |  3 |  0 |  0 |  0 | Común      |  5 |
| Daga pulida      | Esta daga ha sido afilada y retocada para cumplir mejor su trabajo. Se puede vender por un buen precio                                                                                  |  7 |  0 |  0 |  0 | Raro       | 10 |
| Daga Solar       | Un objeto casi mitológico creado con un rayo del Sol. un coleccionista pagaría un gran precio por este objeto                                                                           | 13 |  0 |  0 |  0 | Épico      | 15 |
| Daga del Eclipse | Las leyendas más siniestras hablan de una daga que solo se puede forjar tras un gran sacrificio. Su precio es altísimo, tanto como el poder que otorga.                                 | 20 |  0 |  0 |  0 | Legendario | 30 |
|                  |                                                                                                                                                                                         |    |    |    |    |            |    |
| Escama vieja     | Sea lo que sea que llevase esta escama ya no está entre nosotros, te dará defensa y se puede vender por un poco de dinero                                                               |  0 |  5 |  0 |  0 | Común      |  5 |
| Escama           | Una escama bien conservada y casi fresca. Te dará una buena cantidad de defensa y se puede vender por un buen dinero                                                                    |  0 |  7 |  0 |  0 | Raro       | 10 |
| Escama pulida    | Una escama de una calidad excepcional que refleja la luz. Se puede vender por una cantidad muy buena de oro                                                                             |  0 | 12 |  0 |  0 | Épico      | 15 |
| Escama de dragón | Una escama de una de las bestias más nobles, su brillo y resistencia son excepcionales. Otorga mucha defensa y se puede vender por una pequeña fortuna                                  |  0 | 16 |  0 |  0 | Legendario | 30 |
|                  |                                                                                                                                                                                         |    |    |    |    |            |    |
| Pluma dañada     | Una pluma casi deshecha. Se podría vender por algo de dinero y te hará atacar un poco más rápido                                                                                        |  0 |  0 |  2 |  0 | Común      |  5 |
| Pluma de paloma  | Una pluma que aunque sea poco noble no es inutil. Se puede vender por un poco de oro y te dará más velocidad de ataque                                                                  |  0 |  0 |  3 |  0 | Raro       | 10 |
| Pluma de Halcón  | Una pluma de un animal noble, su calidad es excelente. Se puede vender por una buena cantidad de oro y te dará bastante velocidad de ataque                                             |  0 |  0 |  4 |  0 | Épico      | 15 |
| Pluma de Grifo   | Una pluma de un animal mitológico a la gente le costará creerse que la has encontrado. Se puede vender por bastante dinero y te dará mucha más velocidad de ataque                      |  0 |  0 |  5 |  0 | Legendario | 30 |
|                  |                                                                                                                                                                                         |    |    |    |  1 |            |    |
| Capa usada       | Una capa que ya tiene un poco de trote pero puede servir bien. Se puede vender por un poco de oro y te dará un poco de velocidad de movimiento                                          |  0 |  0 |  0 |  4 | Común      |  5 |
| Capa             | Una capa normal y corriente. Se puede vender por una cantidad normal de oro y te dará velocidad de movimiento                                                                           |  0 |  0 |  0 |  6 | Raro       | 10 |
| Capa del cazador | Una capa que perteneció a un veterano cazador, muy valorada por coleccionistas. Se puede vender por bastante oro y dará bastante velocidad de movimiento                                |  0 |  0 |  0 |  8 | Épico      | 15 |
| Capa del viento  | Una capa que aparece en antiguas leyendas, se dice que allá dónde esté el viento sopla a espaldas de tu portador. Se puede vender por mucho oro y te dará mucha velocidad de movimiento |  0 |  0 |  0 | 11 | Legendario | 30 |

Items con debufos:

| Nombre           | Descripción                                                                                                                                                                             | A  | D  | VA | VM | R          | P  |
|------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----|----|----|----|------------|----|
| Escudo       | Un gran escudo que perteneció a un robusto pero lento caballero en un pasado.|  0 |  12 |  0 |  -7 | Debufo | 10 |
| Esfera dorada | Una esfera dorada con un alto valor económico a la vez que un gran peso.|  0 |  0  |  0 |  0  |Debufo | 50 |
| Martillo       | Este martillo fue empuñado en un pasado por un caballero que también se adentró en el bosque al igual que Kero.|10 |  0 |  -3 |  0 | Debufo      | 10 |


Daga Solar

![Daga solar][DagaSolar]


Daga Lunar

![Daga lunar][DagaLunar]


Daga Usada

![Daga Usada][dagaUsada]


Daga Pulida

![Daga Pulida][dagaPulida]


Escama Vieja

![Escama Vieja][escamaVieja]


Escama 

![Escama][escama]


Escama Pulida

![Escama Pulida][escamaPulida]


Escama del Dragón

![Escama del Dragón][escamaDragón]


Tarro con moscas

![Tarro con Moscas][moscas]


Tarro con Abejas

![Tarro con Abejas][abejas]


Tarro con Libélula

![Tarro con Libélula][libélula]


Capa Usada

![Capa Usada][capaUsada]


Capa

![Capa][capa]


Capa del Cazador

![Capa del Cazador][capaCazador]


Capa del Viento

![Capa del Viento][capaViento]


Pluma Dañada

![Pluma Dañada][plumaDañada]


Pluma de Búho

![Pluma de Búho][plumaBúho]


Pluma de Grifo

![Pluma de Grifo][plumaGrifo]


Pluma de Fénix

![Pluma de Fénix][plumaFénix]


Kero coin (moneda del juego)

![Kero Coin][keroCoin]

## DISEÑO
### Personaje
Kero es el protagonista.
El personaje busca ser desenfadado y familiar. Un personaje simple del que poder encariñarse y ver como va cambiando a medida que el jugador desarrolla su manera de jugar.


### Niveles
El mapa del videojuego está generado aleatoriamente mediante un algoritmo, este mapa consiste en una serie de salas a las que llamaremos niveles organizadas en tres pisos. Estos pisos cuentan con 7-10 niveles por cada piso. Una vez pasado un nivel, el jugador no podrá volver atrás. A medida que se vaya pasando de piso, la estética y dificultad cambiará para balancear la curva de dificultad.


Los niveles se dividen en 4 categorías: niveles de desafío, niveles de jefe, niveles con tienda y niveles de puzzle. Cada uno de estos devuelve una recompensa diferente e involucra a un tipo de enemigo distinto.


- Niveles de desafío: estos niveles son los más comunes entre todos. Se componen de enemigos parcialmente aleatorios (ya que controlaremos la aleatoriedad) y obstáculos que dificultan al jugador. 
   - Objetivo: el único objetivo de esta sala es matar a todos los enemigos, cuando esto se haga se abrirá la puerta. 
   - Recompensa: este nivel no cuenta con una recompensa asegurada aunque los enemigos tienen posibilidad de soltar objetos cuando mueren.
   - Enemigos: cualquier tipo de enemigo
- Niveles de jefe: son los más difíciles de completar porque cuentan con enemigos mucho más complicados. Estos niveles no cuentan o tienen muy pocos obstáculos porque la dificultad residirá en el enemigo.
   - Objetivo: matar a todos los enemigos presentes en la sala (las salas no tienen solo por qué contar con un solo enemigo fuerte, este puede estar acompañado de enemigos comunes para aumentar la dificultad.
   - Recompensa: el nivel de jefe cuenta con las mejores recompensas entre todas las salas. Estas son objetos de mucha rareza que serán muy valiosos para el jugador.
   - Enemigos: jefes y enemigos comunes o modificados.
- Niveles de tienda: estos niveles no tienen enemigos en ellos, simplemente cuentan con una pequeña tienda con un tendero donde el jugador puede comprar vida, objetos y mejoras para el personaje.
   - Objetivo: ninguno en específico, el jugador no tiene por qué comprar ni vender nada y puede salir cuando desee
   - Recompensa: los objetos y mejoras proporcionadas por el tendero
   - Enemigos: ninguno
- Niveles de puzzle: estos niveles son los más complejos. Contaremos con 4 tipos de niveles de puzzle y cada uno de ellos tendrá diferentes distribuciones de enemigos y obstáculos. Los cuatro tipos son:
   - King of the trees: dentro del nivel aparecerá una zona destacada de la sala donde, si el jugador permanece 5 segundos se completará la zona. Una vez completada la zona, esta cambiará de lugar, el jugador tendrá 30 segundos para completar 4 zonas de 5 segundos cada una. Las recompensas variarán en función de cuantas zonas lleves completas. Una vez terminada la cuarta zona los enemigos que continúen vivos morirán y las puertas del nivel se abrirán, dando por terminado el nivel.
      - Objetivo: completar 4 zonas destacadas.
      - Recompensa:  la primera zona recompensa con 1 moneda, la segunda con 5 monedas, la tercera con 10 monedas y la cuarta con un objeto.
      - Enemigos: perdidos, ardillas y fantasmas.
      - Duración: 30 segundos, una vez terminados la zona destacada desaparecerá y para terminar la sala deberá matar a los enemigos.
      - Diseño de audio: para este nivel se implementará un sonido de tictac de reloj que se irá haciendo más rápido a medida que se vaya acabando el tiempo del nivel.
      - Pensamiento computacional: este nivel se enfoca en las destrezas de descomposición, generalización y pensamiento algorítmico. El jugador deberá usar la deducción para, mediante pistas visuales, llegar a la solución del problema. Para las pistas visuales se ha pensado que la zona destacada brille en un color más llamativo. Cuando al jugador se le estén acabando los 30 segundos la zona destacada empezará a emitir pulsos de luz cada vez más rápidos. Cuando el tiempo se acabe la zona actual se apagará. Una vez que el jugador complete la primera zona, deberá hacer uso de la generalización para resolver el nuevo problema, la zona destacada nueva, basándose en las soluciones anteriores. Una vez terminada una zona el jugador deberá usar el pensamiento algorítmico para crear una ruta que le haga llegar lo antes posible y más seguro al objetivo.
      - Mapa y ejemplos visuales:
         -  Ejemplo mapa:
            -  Enemigos: triángulo
            - Zona transitable: verde
            - Zona no transitable: azul
            - Jugador: estrella
            - Zona destacada: círculo
            - Camino jugador: morado
              
	      ![bn1][bn1]

         - Ejemplo primera zona:
           
	      ![bn2][bn2]

         - Ejemplo segunda zona:

              ![bn3][bn3]

         - Ejemplo situación segunda zona: el jugador deberá defenderse de los enemigos que se le acerquen

	      ![bn4][bn4]

         - Ejemplo de cómo profundizar el nivel: A medida que el jugador vaya jugando partidas, el nivel se le hará más y más familiar. Para evitar eso se ha decidido implementar en un futuro una variación del nivel donde haya diferentes zonas a la vez. Cada zona tendrá un icono, este icono representará el tipo de objeto que soltará la zona (armadura, velocidad o daño). El jugador deberá decidir cuál es el objeto que le interesa más y ajustarse a él, fortaleciendo las destrezas de evaluación.

	      ![bn5][bn5]

   - Recoge las bellotas: en el nivel aparecerán un número de bellotas (entre 4-5 bellotas). El jugador deberá recoger las bellotas y dejarlas en un pedestal de piedra. Los enemigos podrán coger las bellotas y no se podrán recuperar. Si el jugador no es suficientemente rápido en matar a los enemigos con bellotas dentro estos se comerán las bellotas, impidiendo que el jugador obtenga su recompensa. Si el jugador consigue poner las bellotas en el pedestal se le curará el 50% de la vida actual.
      - Objetivo: recoger las bellotas y dejarlas en el pedestal
      - Recompensa: una vez dejadas las bellotas en el pedestal, este te proporcionará vida
      - Enemigos: ardillas, duendes y duendes mágicos
      - Diseño de audio: cuando los enemigos se estén comiendo las bellotas se escuchará un sonido de masticar. Cuando se hayan comido la bellota se escuchará un sonido de tragar para indicar al jugador que ya no puede obtener la recompensa. 
      - Pensamiento computacional: este nivel estará centrado en las destrezas de pensamiento algorítmico, abstracción y descomposición. El jugador deberá deducir el funcionamiento del nivel, para ello se le proporcionarán pistas visuales como marcas con forma de bellota en el pedestal que le indicarán al jugador cuantas bellotas le quedan por coger. El diseño del pedestal estará puesto para llevar al jugador a echar las bellotas en el.

		![pili][pili]

      - Una vez entendido el funcionamiento del nivel el jugador deberá hacer un camino óptimo en poco tiempo y modificarlo en función del comportamiento de los enemigos. Para eso usará las herramientas de: reconocimiento de elementos clave en un problema de la abstracción, el jugador deberá entender en cada momento cual es la bellota en la que centrarse;  la creación de algoritmos o secuencias del pensamiento algorítmico, para la creación de un camino óptimo en el que los enemigos no se coman la bellota; la descomposición de problemas en otros más pequeños de la destreza de descomposición, el jugador deberá abstraerse del problema principal de conseguir poner las bellotas en el altar y centrarse en salvar la bellota más necesitada.
      - Mapas y ejemplos visuales:
         - Mapa ejemplo:
            - Enemigos: triángulo
            - Zona transitable: verde
            - Zona no transitable: negro
            - Jugador: estrella
            - Bellotas: círculo
            - Pedestal: corazón
            - Camino jugador: morado
	    
	    ![bn5][bn6]
     
- Mapa con camino óptimo: el camino está escogido para que los enemigos no se coman ninguna bellota

	![bn5][bn7]
  
- Mapa con camino fallido: para cuando el jugador llegue a la X morada el enemigo se habrá comido la bellota de abajo a la izquierda

	![bn5][bn8]

   - La calavera dorada: en el nivel se encontrará una calavera dorada tirada en algún lugar de la sala y un mayor número de enemigos que de costumbre. Cuando el jugador coja la calavera dorada todos los enemigos morirán. Por cada enemigo que mate la calavera (que no haya muerto antes de cogerla) se soltará una moneda. La primera vez que el jugador se encuentre con este nivel, no sabrá el funcionamiento de este por lo que se ha decidido que para que sea más fácil para el jugador descubrir sobre la calavera, se añadirán pistas visuales que lleven la atención a esta. Cuando el jugador mate a un enemigo, la calavera empezará a romperse (esto se representará con grietas y un sonido). Pasará lo mismo cuando el jugador mate a un segundo enemigo antes de coger la calavera. Finalmente, cuando el jugador mate a un tercer enemigo la calavera se romperá en pedazos. A partir de ese momento el jugador no podrá usar más la calavera.
      - Objetivo: coger la calavera lo antes posible, si el jugador mata a 3 enemigos la calavera se romperá por lo que el objetivo cambiará a matar a todos los enemigos.
      - Recompensa: una moneda por cada enemigo que mate la calavera
      - Enemigos: perdidos, lobos, duendes o magos
      - Diseño de audio: cada vez que el jugador mate a un enemigo mientras la calavera siga viva se escuchará un crujido metálico. Si se rompe la calavera sonará un estallido metálico. Si el jugador obtiene la calavera antes de que se rompa sonará un sonido de campana.
      - Pensamiento computacional: para este nivel se han tenido en cuenta las destrezas de pensamiento algorítmico y descomposición. Cuando el jugador llega por primera vez al nivel tendrá que deducir el funcionamiento de este mediante los eventos que suceden. Lo más posible es que en las primeras ocasiones el jugador no entienda la solución del problema, por ello se añadirán pistas si el jugador pasa por la sala 3 veces sin resolver el problema. Las pistas se harán mediante conversaciones con NPCs en el campamento.  Una vez que el jugador entienda el funcionamiento del nivel, se le medirá el pensamiento algorítmico necesario para la creación de rutas donde el jugador sea capaz de llegar a la calavera con el menor número de enemigos muertos posibles.
      - Mapa y ejemplos visuales:
         - Ejemplo de mapa
            - Enemigos: triángulo
            - Zona transitable: verde y amarillo 
            - Zona no transitable(río): azul
            - Jugador: estrella
            - Camino jugador: morado

	    ![bn9][bn9]

- Ejemplo camino óptimo del jugador

	 ![bn10][bn10]

- Ejemplo camino que rompe la calavera

	 ![bn16][bn16]

- Efectos visuales de la calavera rompiéndose

	 ![calavera][calavera]
         ![bn11][bn11]

   - Apaga el incendio: en este nivel el jugador se encontrará con una serie de fuegos ardiendo, estos fuegos tienen recompensas de interés debajo (oro y objetos). Para obtenerlos el jugador debe apagar lo más rápidamente los fuegos. Las recompensas de debajo de los fuegos tienen un color que el jugador puede ver, previamente se habrá enseñado al jugador que cada color de objeto significa una rareza. Las rarezas siempre tienen las mismas estadísticas por lo que el jugador siempre puede saber cual es la recompensa mayor. Para apagar un fuego el jugador deberá pegarlo tres veces. El jugador no podrá apagar todos los fuegos por lo que deberá escoger qué objeto es más valioso o más arriesgado coger. También habrá enemigos que el jugador deberá matar o esquivar. El nivel no se acabará hasta que todos los enemigos mueran. 
      - Objetivo: salvar los objetos y matar a los enemigos
      - Recompensa: objetos u oro
      - Enemigos: fantasmas y magos
      - Tiempo: los fuegos quemarán el objeto en 5-7 segundos
      - Diseño de audio: para ese nivel se implementará un sonido de fuego que, a medida que se vayan quemando los objetos, se intensificará hasta hacer un sonido de quemado.
      - Pensamiento computacional: este nivel se ha pensado en torno a la destreza de la abstracción y evaluación. Para pasarse el nivel el jugador deberá reconocer los elementos clave del problema ,los objetos que se están quemando, y resolverlo de la forma más óptima haciendo uso de los sistemas de representación previamente establecidos, en este caso los colores de los objetos. Para este nivel se han diseñado varias distribuciones de los elementos para que los objetos de mayor rareza no sean siempre los que cuentan con el camino más beneficioso para el jugador. Si el jugador quiere obtener la mejor combinación de recompensas deberá planear su estrategia en pocos segundos creando un camino rápido y con la menor cantidad de obstáculos posible. Para ello el jugador debe usar destrezas del pensamiento computacional como la evaluación donde el ajuste de las decisiones según el objetivo será clave para no perder los objetos.
      - Mapa con recorridos:
         - Ejemplo de mapa:
            - Objetos quemandose: círculos
            - Enemigos: triángulo
            - Zona transitable: verde y amarillo
            - Zona no transitable(río): azul
            - Jugador: estrella
            - Camino escogido: morado

	    ![bn12][bn12]

- Ejemplo de camino óptimo: este camino es el que menos enemigos tiene, el que mejores estadísticas da y el que menos posibilidades tiene de que desaparezcan los objetos.

  ![bn13][bn13]

- Ejemplo  de camino poco óptimo: en este camino hay más riesgo de que el objeto se queme, además hay más enemigos y las estadísticas de los objetos recibidos son menores a la del objeto anterior.

  ![bn14][bn14]

- Ejemplo de camino poco óptimo: este camino es más seguro que el anterior pero la recompensa sigue siendo menor que la del primero.

   ![bn15][bn15]


- Ejemplo de cómo profundizar el nivel: una vez que el jugador se haya acostumbrado al nivel, el jugador será introducido a una nueva mecánica para dar complejidad a los niveles. Esta mecánica será conocida como las auras y estas aumentarán el daño de los objetos en función del color de las auras. Por ejemplo, en la imagen de abajo se puede observar que el objeto común tiene un aura, por lo que se tendrá que sumar el daño del aura al daño del objeto. Esto cambiará la forma óptima de pasarse un nivel previamente diseñado.

   ![bn16][bn16]


#### Referencias visuales
Chrono Trigger. Sala que se asemeja visualmente a un posible nivel, un claro rodeado por árboles con matojos y rocas que nos pueden servir como obstáculos. 

Legend of Zelda Link to the past. Puede ser para referenciar una sala de enemigos y/o puzzle. Por ejemplo no le dejan pasar los guardias rojos si no mata a los enemigos y si pone los jarrones en un orden abrirá la puerta derecha o izquierda.

The binding of  Isaac. Una sala donde hay que acabar con los enemigos, hasta que no lo hagas no te deja salir a la siguiente sala.


### Enemigos
Por ahora estos son de perseguir o huir. Iremos desarrollando estas IA a medida que avancemos con la asignatura de Desarrollo de Personajes.

*Fantasma*
- Vida: 1 (el valor va aumentando en 1 en base a la sala)
- Tipo de movimiento: Persigue lentamente al jugador (su velocidad aumenta según se avanza en el juego).
- Velocidad de movimiento: Un 10% inferior a la velocidad de movimiento del jugador.
- Ataque: Quita poco a poco vida al jugador.
- Debilidad: El enemigo es débil a todos los ataques
- ![Concept Fantasma][fantasma]

*Lobo*
- Vida: 10 (el valor va aumentando en 1 en base a la sala)
- Tipo de movimiento: Se mantiene alejado del jugador.
- Velocidad de movimiento:Un 15% superior a la velocidad de movimiento del jugador.
- Ataque: Realiza un dash hacia el jugador.
- Debilidad: Si impacta contra una pared al  realizar el dash se queda aturdido durante 2 segundos.
- Variante superior: no se aturde al impactar contra las paredes.

*Duendecillo*
- Vida: 6 (el valor va aumentando en 1 en base a la sala)
- Tipo de movimiento: Se teletransporta detrás del jugador con tiempo de espera entre cada movimiento.
- Velocidad de movimiento:Igual a la del jugador.
- Ataque: Se teletransporta detrás del jugador y a los dos segundos ataca con un cuchillo, poco rango.
- Debilidad: Entre un teletransporte y otro está indefenso.

![f1][duende]

*Duendecillo mago*
- Vida: 5 (el valor va aumentando en 1 en base a la sala)
- Tipo de movimiento: Busca alejarse del jugador.
- Velocidad de movimiento:Un 10% menos que la del jugador.
- Ataque: Lanza una bola de energía que desaparece delante suya y aparece detrás del jugador en dirección suya.
- Debilidad: Lento disparando y mientras huye no puede atacar.

*Carne de cañón (ardillas por ejemplo)*
- Vida: 3 (el valor va aumentando en 1 en base a la sala)
- Tipo de movimiento: Se mueve hacia al jugador
- Velocidad de movimiento:Un 20% menos que la del jugador.
- Ataque: Hace daño por contacto con el jugador
- Debilidad: Poco resistentes.

*Los perdidos (probablemente murciélagos o similar volador)*
- Vida: 1 
- Tipo de movimiento: Aleatorio (como si rebotaran en las paredes)
- Velocidad de movimiento: Igual a la del jugador
- Ataque: Hace daño por contacto con el jugador, cada vez que te golpea uno el siguiente ataque de otro te hace más daño así infinitamente.
- Debilidad: Poco resistentes (oneshot).
- Murciélago Fase 1
  
  ![Concept Murcielago Fase 1][murcielago]

- Murciélago Fase 2

  ![Concept Murcielago Fase 2][murcielago1]
  
- Murciélago Fase 3
  
  ![Concept Murcielago Fase 3][murcielago2]
  
### Jefes
*David el gnomo*
- Vida: 100 (aparece al final de la primera área)
- Tipo de movimiento: Mantiene la distancia pero si el jugador se acerca va a por él.
- Velocidad de movimiento:Un 20% menos que la del jugador.
- Ataques:
   - Ataque 1: Golpea el suelo y crea una onda expansiva.
   - Ataque 2: Derechazo al jugador si se encuentra cerca.
   - Ataque 3: Lanza rocas al aire que caen en la zona en la que esté el jugador.
- Habilidad (Soy diez veces más fuerte que tu): Cuando le queda un 20% de vida aumenta su velocidad en un 30% y hace un 25% más de daño durante 10 segundos (puede ser que sume el daño del jugador al suyo), después debe descansar durante 5 segundos.
- Debilidad: sus golpes son fáciles de esquivar.

*El encontrado*
- Vida: 200 (aparece al final de la segunda área)
- Tipo de movimiento: Se va moviendo por la sala.
- Velocidad de movimiento: Un 30% más lento que el jugador.
- Ataques:
   - Ataque 1: Lanza ácido que se queda en el suelo durante un tiempo determinado, el jugador recibe daño al pasar por encima.
   - Ataque 2: Invoca a los perdidos, estos son especiales en vez de aumentar el daño infinito cada vez que golpean al jugador el encontrado.
   - Ataque 3: Acelera y realiza tres ataques directos seguidos a por el jugador
- Habilidad (Sin límites): Su vida no tiene límites es decir con la curación de su segunda habilidad puede sobrepasar sus 200 de vida inicial y cuanto menos vida tenga más se cura.
- Debilidad: A determinar.

*Nahum*
- Vida: 300 (Jefe final)
- Primera fase:
- Tipo de movimiento: parado
- Ataques: a determinar
- Segunda fase:
- Tipo de movimiento: camina
- Ataques: a determinar


### Campamento Base
El Campamento Base será el lugar al que el jugador regresa tras las Expediciones para conseguir nuevo equipamiento y desbloquear objetos para su próxima expedición.
Deberá tener un aspecto acogedor y cada puesto tiene que tener una identidad reconocible.


#### Tablón de misiones
En esta zona el jugador tendrá disponible misiones de diferentes gremios. Las misiones tendrán objetivos específicos para la partida, como intentar terminar lo más rápido la partida o con el máximo de dinero posible. Estas misiones darán como recompensa subidas de estadísticas para las partidas (subida de vida total, subida de ataque, subida de defensa). Cuando el jugador consiga todas las misiones de un gremio desbloqueará la clase de ese gremio, por ejemplo, si el jugador completa todas las misiones del gremio de los corredores podrá entrar al bosque con las estadísticas de un corredor.
- Pensamiento computacional: El tablón de misiones es la mejor herramienta que se tiene para medir el pensamiento computacional. Al empezar una misión de gremio se podrá medir exactamente qué objetivo tiene el jugador para la partida, por ejemplo, en una misión donde el jugador debe el jugador deba conseguir el máximo oro posible se podrá implementar un algoritmo de mochila para calcular si el jugador podría haber obtenido una mayor recompensa con los objetos que se le han proporcionado (en este caso nos servirá para medir cómo de bien evalúa el jugador las situaciones y cómo se adapta en función del objetivo). Para versiones futuras, se ha pensado implementar un sistema de puntuación donde el jugador podrá competir contra él mismo para conseguir optimizar al máximo la misión.


#### Tienda de objetos
La tienda objetos permite desbloquear objetos para las expediciones. Estos objetos se desbloquean con el oro obtenido. Los objetos se desbloquean por tandas, es decir no puedes desbloquear objetos de rareza superior hasta que has comprado todos los de la rareza actual.


#### Puesto del curtidor
En esta tienda el jugador puede comprar mejoras para su inventario a cambio de oro. Solo estarán disponibles nuevas mejoras cuando se haya progresado en algunas misiones.
Es un lugar austero, unos pocos bancos de trabajo y un árbol de los que cuelgan pieles curtidas y bolsas de cuero.


#### Cabaña esotérica
La cabaña esotérica vende pociones y permite elaborar mejores versiones de estas a cambio de oro y completando desafíos.
La cabaña tiene un aspecto sombrío con un gran caldero en el costado derecho del mismo, tarros con ingredientes y matraces de todo tipo para hacer pociones. 


#### Carro Ambulante
Este carro es el que te encuentras en los niveles de tienda durante la expedición. No puedes comprar nada con el carro, pero, puedes donar dinero al carro. Cuando consigas alcanzar una meta de donaciones al carro este tendrá mejores y más objetos disponibles en los niveles de tienda.


## ARTE
### Interfaces
#### Flujo de Pantallas
El flujo de las pantallas que se van a describir a continuación es el siguiente:

![f1][f1]

#### Menú Principal
La interfaz de usuario del menú principal será sencilla y minimalista. Estará compuesta de un fondo con el personaje inicial “Kero” el nombre del videojuego “Beyond the trees” con su correspondiente tipografía y un escenario con temática de bosque encantado, y tres botones, el botón de jugar, el de opciones y el de créditos.

#### Créditos
La interfaz de créditos es accesible a partir del botón correspondiente en el menú principal.
Esta pantalla muestra los nombres de todos los desarrolladores implicados en el proyecto y un mail del contacto de la empresa. Además incluirá un botón de volver al menú principal.

#### Opciones
La pantalla de opciones tiene las siguientes preferencias: modo de pantalla completa en el que el jugador podrá marcar la casilla según como quiera visualizar el juego, modificar volumen de la música y por último, el jugador puede seleccionar la calidad gráfica que tendrá en videojuego. El botón de volver también está presente para volver al menú principal.

#### Login
Tras dar al botón de jugar, aparecerá la pantalla de login en la que el usuario deberá introducir sus datos. El jugador podrá elegir su género y deberá introducir un nombre de usuario. Una vez hecho todo, el botón de empezar llevará al inicio del juego  al igual que la interfaz anterior, esta tiene el botón de volver para regresar al menú principal.
#### En Juego
La interfaz in-game de Beyond the Trees opta por ser afable y con temática fantástica.
Esta interfaz contará con una barra de vida de color rojo indicando la vitalidad del personaje. Un contador de tiempo que indica el tiempo que lleva jugando el jugador y el dinero que muestra todo el oro con el que cuenta el jugador.
#### Inventario
La interfaz de inventario seguirá la misma estética que las otras interfaces, una con temática fantástica y de bosque encantado. En el inventario se puede ver los objetos que tiene actualmente el jugador, la cantidad de ellos, una imagen de estos junto a sus stats y una breve descripción.
#### Pausa
En el menú de pausa se puede pausar el estado del juego y acceder de nuevo al menú de opciones. También se podrá continuar con la partida o bien volver al menú principal.
#### Victoria/Derrota
Cuando el jugador derrota al jefe final, aparece la pantalla de victoria informandole que ha ganado. Por el contrario si el jugador es abatido por los enemigos aparecerá la pantalla de derrota.


### Bocetos
Como primeros bocetos se tiene el del personaje en papel. La idea es crear algo simple y fácilmente reconocible. A la derecha se han añadido posibles personalizaciones del personaje cambiándole los ojos.

### Arte conceptual
El concept art del personaje es una versión aún más simplificada que el boceto inicial. Lo que se busca es que cualquiera pueda dibujar o reconocer al personaje principal. Esto está pensado para poder llegar al mayor público posible.

![f1][b1]

#### Turn-around personaje
![Kero, el héroe de Evergreen][conceptPJ]
#### NPC Búho
![buho][Buho]
#### Assets
![puente][puentes]

![arbusto][arbusto]

![hierbajo][hierbajo]

### Banda Sonora 
La banda sonora busca ser algo que acompañe al jugador durante la partida, en ningún momento se buscará que la banda sonora tome protagonismo. Un ejemplo de banda sonora parecida al que se tiene pensado para el juego es la música de The binding of Isaac.
https://www.youtube.com/watch?v=H9jqfH6Yp7w&list=OLAK5uy_nirxVnCtTeTtYMS9h1itNIK-cvZUufIhQ
## ALCANCE 
El objetivo principal es desarrollar un juego base con un conjunto de niveles ya hechos al que mediante DLCs, se le pueden añadir muchos más niveles.
### Público objetivo
#### Quien va a comprar el juego
Profesores, instituciones educativas, editoriales y el gobierno. Padres e instituciones educativas tienen que sentirse atraídos hacia la idea del juego. Tenemos dos principales atractivos para este grupo, una estética familiar que no les parezca demasiado disparatada o inadecuada para los niños; y el más importante un juego capaz de medir y mejorar el pensamiento computacional de los niños, una faceta clave del currículo escolar.
#### Quién va a jugar el juego
El juego está dirigido a niños de entre 3 y 12 años haciendo especial énfasis al grupo de 8 a 12. Con partidas rápidas que permiten invertir una pequeña cantidad de tiempo, perfectas para jugar entre descansos, con el fin de obtener resultados analíticos.
![Mapa Empatía][mapaEmpatia]

### Publicidad y redes sociales
Para publicitar el juego se va a dividir el proceso en dos etapas:
- Desarrollo: Mientras el juego esté en proceso de desarrollo se va a hacer una campaña por las redes sociales donde se pretenderá crear una comunidad de personas que sigan el proceso de creación del videojuego, para ello
 se ha de hacer publicaciones constantes en redes sociales. Se ha optado por las siguientes redes sociales para promocionar el videojuego: Twitter, Youtube y Tiktok. Se han elegido estas redes sociales debido a su facilidad para la exposición al público, ya que nuestro objetivo en esta fase es que llegue al máximo de personas posibles.

- Juego terminado: Una vez que tengamos el videojuego desarrollado, nuestro objetivo será que a los posibles compradores les llegue el producto. Para ello se ha decidido contactar con editoriales e instituciones educativas, esto se hará acudiendo presencialmente a escuelas y ferias como “AULA” donde se presentará el producto como una herramienta para aprender de forma inconsciente y se presentarán las métricas elegidas previamente. También se contactará con posibles clientes mediante métodos a distancia como correos electrónicos.

### Modelo de Negocio

Usaremos un modelo Freemium (Free + Premium) ofreciendo el juego de manera totalmente gratuita a los usuarios, teniendo estos la opción de ver un análisis de datos completo si pagan la versión premium.

### Planes de monetización
Para un futuro F2P con futuros dlcs de pago.

### Model Canvas
![Model Canvas][modelCanvas]


### ToolBox
![Tool Boxr][toolBox]

## ANÁLISIS MDA
### Mecánicas
#### Inventario
El jugador deberá escoger bien los objetos que coge ya que cuenta con un número limitado de espacios para guardar objetos. Cuando el jugador tira un objeto este desaparece por lo que el jugador deberá tener mucho cuidado a la hora de escoger sus objetos ya que los cambios que haga serán irreversibles.
#### Objetos
Los objetos proporcionan mejoras estadísticas al jugador. Hay diferentes tipos de objetos y de diferentes rarezas que dan al jugador diferentes combinaciones para adecuarse a las necesidades de la partida.
#### Clases
El jugador podrá escoger al principio de la partida que tipo de clase querrá usar, cada clase cuenta con unas estadísticas diferentes y dos tipos de ataque, a distancia y cuerpo a cuerpo.
#### Generación aleatoria
El juego escoge aleatoriamente entre salas ya creadas, entre esas salas se encuentran las de tipo puzle, tipo tienda y tipo jefe. Por cada piso habrá una de jefe asegurada y mínimo una de los otros tipos.

### Dinámicas
#### Toma de decisiones estratégicas en el Inventario
Los jugadores se enfrentan a decisiones tácticas al gestionar su inventario limitado. Deben equilibrar entre objetos de ataque, objetos curativos y otros elementos clave. La pérdida irreversible de objetos agrega un componente de riesgo a cada elección, fomentando la planificación cuidadosa por parte del jugador.
#### Búsqueda y Valoración de objetos
La búsqueda de objetos se convierte en una parte esencial del juego, ya que los jugadores buscan constantemente mejoras estadísticas y objetos raros. La dinámica se centra en la emoción de descubrir un objeto valioso y en la evaluación estratégica de cómo éstos afectarán su estilo de juego.
#### Adaptación a la Clase Elegida
Las dinámicas cambian según la clase seleccionada al inicio del juego. Cada clase ofrece un enfoque único en el combate, ya sea a distancia o cuerpo a cuerpo, y las estadísticas específicas de la clase afectan directamente el estilo de juego del jugador. La elección de la clase impacta en la estrategia y en cómo se enfrentan a los desafíos. Esto provoca rejugabilidad y frescura en cada partida. 
#### Exploración de Salas Generadas Aleatoriamente
La generación aleatoria de salas crea una experiencia diferente en cada partida. Los jugadores se enfrentan a la incertidumbre mientras exploran, sin conocer de antemano qué desafíos encontrarán. La presencia garantizada de salas de jefe y la posibilidad de encontrar salas de puzles o tiendas agregan variabilidad a la progresión del juego.
#### Estrategias Puzle, Tienda y Jefe
Las dinámicas varían en función del tipo de sala encontrada. Las salas de puzles desafían la mente del jugador, las salas de tienda ofrecen oportunidades estratégicas para mejorar el equipo y las salas de jefe requieren tácticas distintas a las utilizadas en las salas comunes de enemigos. Los jugadores deben adaptar sus estrategias a medida que enfrentan diferentes desafíos en cada tipo de sala.
#### Progresión a lo Largo de los Pisos
La dinámica del juego cambia a medida que los jugadores avanzan a través de los pisos, enfrentándose a enemigos más difíciles y adquiriendo recompensas más valiosas. La progresión está marcada por la acumulación de recursos y la toma de decisiones estratégicas que afectarán la dificultad y la recompensa en etapas posteriores del juego. Cuanto más avance el jugador más fuertes se volverán los enemigos por lo que la curva de dificultad avanzará progresivamente. Juntado con la generación aleatoria de salas, las clases y objetos dan una experiencia única en cada partida al jugador.

### Estética I

Estas son las siguientes estéticas principales en Beyond The Trees:

#### Desafío
El juego se caracteriza por el desafío que presenta a los jugadores, este desafío se presenta a través de puzzles que llevarán al jugador a pensar y valorar cual es el curso a seguir más óptimo para ellos.
El desafío también se presenta a través de los bosses del juego los cuales van a provocar que el jugador deba cambiar en parte la estrategia que llevaba siguiendo hasta este momento.
	
#### Sumisión

En el mundo del juego, la tarea de eliminar enemigos sala por sala se convierte en una sumisión intrínseca, una actividad que se realiza como un cautivador pasatiempo.

El objetivo fundamental del juego es alcanzar los niveles finales, una hazaña que se presenta como un desafío considerable debido a la abundancia y complejidad de estos niveles. 

Cada nivel presenta desafíos concretos que requieren la atención estratégica y táctica del jugador. La aplicación de conocimientos empíricos confiere a algunos jugadores una ventaja, marcando la diferencia entre la victoria y la derrota.
Tomemos, por ejemplo, el desafío denominado "Apaga el incendio". Jugadores menos experimentados podrían verse abrumados al no tomar la mejor decisión al entrar. 

En contraste, los jugadores más expertos podrán resolver esta sala con un simple vistazo, subrayando la importancia de la experiencia en el juego.
La sumisión se ve profundamente influenciada por los ítems. El peso de las decisiones en torno a portar un ítem u otro puede tener consecuencias significativas, alterando la dinámica del juego y ofreciendo oportunidades estratégicas.

#### Sensación
Quizás, la estética de sensación es la menos predominante de las 3 principales pero sigue siendo muy importante. Al estar presente en todo momento el desafío (tener que derrotar a todos los enemigos para avanzar y no poder salir), sumado a la generación aleatoria de personajes, el juego te genera una sensación de intriga al no saber qué enemigos te enfrentarás en la siguiente sala y sobre todo, la sensación de estar en alerta cuando combates contra los adversarios. 

### Estética II

Las estéticas secundarias no son tan abundantes como las principales pero siguen estando presentes a lo largo de todo el videojuego:

#### Descubrimiento
A lo largo de una partida te suelen aparecer un conjunto de puzzles, pero no todos, según vas jugandolos te encontrarás con nuevos.

Según el paso de niveles vas encontrándote con enemigos con propiedades y comportamientos distintos y los enemigos finales que son únicos por nivel.
#### Coleccionismo
En Beyond The Trees hay muchos objetos que no aparecen en la primera iteración del jugador con el videojuego. Tiene que volver a jugar y con el oro obtenido desbloquear nuevos ítems. En definitiva, hay que ganar y volver a jugar para poder desbloquear todos los objetos.


## POST MORTEM
### Grupal
#### ¿Qué ha ido bien?
El desarrollo de nuestro juego fue un viaje impulsado por la sincronización y la innovación. Desde el principio, implementamos una estrategia de trabajo en paralelo, permitiendo que los equipos de desarrollo se centrarán en sus áreas específicas sin perder de vista la visión global, o reparto de trabajo mientras algunos hacían enemigos otros mejoraron los escenarios y puzles. 

Durante la primera entrega al ser muy repartido y no estar junto no era necesaria mucha comunicación sin embargo en la segunda al necesitar luz verde de algunos departamentos, las 'dailies' se convirtieron en nuestro pilar, sesiones diarias que no solo mantuvieron a todos informados, sino que fomentaron una comunicación abierta y fluida entre los equipos de recursos humanos y gestión humana.

La velocidad de las tareas fue un desafío que abordamos con determinación, estableciendo procesos eficientes que no comprometieron la calidad del producto final, somos muy rápidos con el lead time y nos permite una agilidad de decisión entre tareas. Se propusieron muchos puzzles y desde el departamento de programadores se pensaba que sería imposible abordar tal tarea, sin embargo al final salió adelante.

En retrospectiva, nuestra capacidad para equilibrar la gestión humana, la comunicación efectiva y la ejecución técnica en Unity fue clave para lograr un juego que no solo cumplió con nuestras expectativas, sino que también trascendió con ideas innovadoras. Este proceso no solo fue un logro en términos de desarrollo, sino también un testimonio del poder del trabajo en equipo y la dedicación para convertir ideas en realidades jugables.

#### Flujo de correciones
Los puzzles tuvieron un buen desempeño en general, cumplimos con nuestras promesas en su mayoría, pero varios de ellos carecen de la fuerza y el detalle que esperábamos. Aunque anticipamos un impacto mayor, parece que no capturaron la atención del jugador promedio como habíamos previsto.

Una de las decisiones acertadas fue organizar equipos pequeños de dos o cuatro personas, lo que agilizó enormemente nuestras tareas. Javi en la entrega final estaba casi en 4 ramas(enemigos, arte de enemigos, generación escenario, items etc…)

Distribuir recursos significativos al desarrollo de los enemigos fue una elección acertada debido a la complejidad de esta sección. Sin embargo, esta priorización dejó otras áreas, como la tienda, en manos exclusivas de Luis, confiando bastante en su gestión.
Inicialmente, utilizamos ampliamente Trello, lo que fue un aspecto positivo en la primera etapa. Aunque en la segunda entrega no recurrimos tanto a esta herramienta, compensamos esta ausencia con reuniones diarias más frecuentes.
En cuanto al GDD, en algún momento nos dejamos llevar por la emoción y añadimos mucho contenido sin evaluar su viabilidad realista.

#### Feedback Externo
Beyond the Trees lo han probado un total de 7 personas, que entienden de videojuegos. Empezando por los comentarios positivos, nos han comentado que el estilo visual es muy bonito y concuerdan los personajes con los escenarios. Siguiendo con el apartado artístico la música ha sido la acertada y le ha gustado a la mayoría.
En cuanto a los aspectos negativos, lo que más han señalado ha sido la dificultad que se presenta en los primeros niveles de Beyond the Trees, al ser un juego que genera enemigos de manera aleatoria, el jugador se puede encontrar con 5 enemigos y es casi imposible. Falta de retroalimentación, en las animaciones de los enemigos (cuando atacan), en el jugador (cuando recibe daño) y al recoger objetos (los recoges sin más). Hay botones que no tienen la misma temática y eso queda un poco raro. Un problema que ha afectado a los usuarios es que los objetos son algo caros y tienes que jugar mucho para poder comprarte uno. La poca variedad de enemigos y de salas hace que el juego sea repetitivo. Para finalizar, el disparo tanto en móvil como en ordenador hay que pulsar repetitivamente la pantalla o la tecla y eso lo hace incómodo.


#### ¿Qué se podría haber mejorado?
Somos conscientes de varios desafíos que enfrentamos en el desarrollo. Los tiempos fueron un factor crucial; algunas tareas carecían de la fuerza necesaria. Además, la transición entre tareas resultó lenta, ya que requería la aprobación de otros equipos. Esto llevó a que varias tareas se quedaran sin completar, principalmente debido a nuestra falta de agilidad. 

Aunque nos organizamos eficientemente al principio, tareas enormes como la generación de escenarios, enemigos, jefes y la tienda representaron desafíos significativos. La interdependencia entre estos conceptos complejos a menudo obstaculizaban el avance, ya que no podíamos probar elementos como escenarios sin tener los enemigos o jefes correspondientes preparados.

En cuanto a la intención inicial de utilizar una API proporcionada por el profesor para el comportamiento de personajes, tuvo impactos mixtos. Si bien facilitó la integración de muchos jefes y algunos enemigos, al ser una versión experimental, causó numerosos problemas en la construcción del juego y la adaptación de otros enemigos. A pesar de estos contratiempos, esta API sigue siendo valiosa, ya que resulta cómoda y contribuye significativamente al aprendizaje relacionado con el comportamiento de personajes en nuestra asignatura.

### Individual
#### Persona 1
**¿Que ha ido bien?**
Hemos trabajado de forma paralela de forma correcta, sin detener el trabajo en casi ningún momento.
Las mecánicas seleccionadas son interesantes y entretenidas y se ha conseguido que lleguen a un nivel cercano al deseado.
**Flujo de correcciones**
Inicialmente se buscaba crear una gran variedad de salas y enemigos para evitar que resultara muy repetitivo, finalmente por falta de tiempo se ha creado una pool muy pequeña de puzzles y salas genéricas para intentar conseguir un producto final aceptable.
Una decisión incorrecta fue intentar que compartieran un mismo script los enemigos porque ha impedido que se implementarán algunos enemigos.
**Feedback o retroalimentación externo**
En el poco feedback que tenemos se nos indica que hay algunos enemigos cabrones y que te están esperando en las puertas.
**¿Qué se podría haber mejorado?**
Se podrían haber mejorado temas de plazos, tipo juntar las cosas antes y ver cómo funcionan en conjunto y  no esperar a los últimos días.
Compartimentar el control de los enemigos.

#### Persona 2
¿Que ha ido bien?
En general velocidad de tarea y resolución de conflictos. Si no recuerdo mal nuestro lead time no era más de 3 días teniendo más agilidad en cambiar de tareas y scopes (esto no funciona o se entiende mal, bien pues dejalo haz esto o junta esto otro). Buena idea usar tutoriales para que no tenga excesos problemas más adelante(dio problemas)
Flujo de correcciones
A un nivel realista, al principio se prometen muchos puzzles y otras mecánicas que no sabíamos si llegarían a buen puerto. Somos soñadores algunos de nosotros pero por lo general los programadores están más en tierra y explican la realidad posible de entrega.
Decisiones acertadas, repartimos en micro grupos a menudo de dos o cuatro, con muchísima comunicación, la tarea en sí una vez entregada la termina la que empezó, si haces el código de una ardilla también terminas el arte.
por el lado negativo, siendo lo más positivo del aspecto, no todos nosotros somos super genios de programación y el inconveniente que mencione de hacer microgrupos de la parte positiva su negativa seria demasiada libertad, por ejemplo poca optimizacion de codigo, como generamos salas(no es dinámico, no son prefabs, etc), cómo gestionamos el POO de los enemigos (no abstracto).
¿Qué se podría haber mejorado?
Bueno para empezar como mencionado anteriormente, ser mejores programadores, pero bueno somos estudiantes, aunque en cuarto ya, triste sea que sea quizás nuestra primera entrega de un juego.
Una dirección artística. No ser mejor dibujante, sino saber encajar y cohesionar un todo.
Por mi parte no me voy a quejar porque estrictamente hablando no por parte del arte de este juego, ya que tome la decision de separarme para hacer yo algo por mi cuenta.

#### Persona 3
¿Que ha ido bien?
Lo que mejor ha ido para mi, ha sido la gestión de todo lo que tiene que ver con el proyecto, tanto la gestión humana (reparto de tareas), como la del tiempo.
La elección de la API ha sido clave para el comportamiento de enemigos y la de dar la última semana para la elaboración de puzzles fue clave.

Flujo de correcciones
Como he dicho lo de la última semana de puzzles, adelantando así la finalización del flujo principal del juego (generación de escenarios, enemigos, básicos, stats, inventario, flujo de pantallas, jugador).
Por otro lado, creo que fue, para mí, una decisión errónea la de hacer la mayoría de los enemigos para la beta (los bosses eran necesarios) mientras que podriamos a ver asignado a otra persona más en la mecánica de la tienda para que hubiese quedado más pulida (al final está bien).

¿Qué se podría haber mejorado?
En general el tiempo. Entregamos el juego a 20 min y estuvimos un putisimo dia entero en Discord pero bueno.

#### Persona 4
¿Que ha ido bien?
Dailys y comunicación: Una de las cosas que más orden han aportado al equipo son las reuniones daily, son las que realmente se ve en que está trabajando el equipo y sobretodo las opiniones y reacciones de lo que están haciendo.
Entorno de programación: Unity es sin duda la herramienta predilecta para estudios indie, es una herramienta lo suficientemente potente para hacer un juego bueno y tiene una cantidad absurda de documentación y tutoriales. Es muy user friendly y aunque más de una vez se ha crasheado nunca ha sido una complicación mayor.
	
Flujo de correcciones
Organización: Pese que al principio la organización se planteó muy estrictamente, a mediados del desarrollo todo fue bastante caótico ya que nadie miraba trello o la información de tareas estaba en diferentes sitios, en muchos momentos algunos del equipo no sabían qué hacer mientras otros estaban hasta arriba de trabajo. Afortunadamente tomamos un rumbo mucho más personal haciendo las dailys en clase más profesionales y todo pillo un rumbo más adecuado
GDD desorbitado: Al principio del GDD teníamos muchas ideas que eran imposibles de implementar en 2 meses. Afortunadamente a mediados del desarrollo se decidió que cosas deben de estar implementadas para la beta y cuales se descartaron indefinidamente

¿Qué se podría haber mejorado?
API comportamiento: En un principio del proyecto se decidió que los enemigos se hiciesen más adelante con la API de la asignatura de comportamiento de personajes debido a procedimiento a dia de hoy no tendriamos ningun enemigo terminado al 100%
Equipo: En el equipo ha hecho falta una persona que hiciera arte. Mucho.

#### Persona 5
¿Que ha ido bien?
Los objetivos básicos que nos hemos marcado se han cumplido.
Nos hemos sabido coordinar bien y debido a que no éramos herméticos con nuestras partes nos hemos podido ayudar en cada cosa que hacíamos lo cual ha evitado que estemos demasiado tiempo solucionando un problema.
Las bases del juego son firmes, hemos conseguido crear unos cimientos muy fuertes sobre los que construir el juego.
El tratado de datos a pesar de que llevo tiempo se ha convertido en el inicio de persistencia de memoria y un acceso sencillo y rápido a la información del juego.

Flujo de correcciones
Debería haber sido más decidido a la hora de coger tareas, la duda a la hora de con qué tarea seguir o con que podía ayudar.
La tienda y los datos fue un vaivén de decisiones, no elegí bien el video tutorial, no supe que rumbo coger y tuve que virar después de haber perdido 5 días de trabajo, haber intentado implementar un paquete, en general trabajo que al final no sirvió de nada, por suerte se cambió el rumbo y el resultado es dinámico, fácilmente escalable y de paso se introdujo la memoria persistente

Feedback o retroalimentación externo
La gente no lee el tutorial.

¿Qué se podría haber mejorado?
El flujo de trabajo, en el sentido de que se ordenen mejor las dependencia de las tareas.
Las decisiones a la hora de dónde buscar referencias.

#### Persona 6
Qué ha ido bien
Nos hemos organizado bastante bien, la repartición de tareas ha hecho que en todo momento todo el mundo tuviese algo que hacer. Somos un equipo trabajado, los tiempos los hemos gestionado bien y hay buen ambiente (tanto de trabajo como de compañerismo).


Flujo de correcciones
Ser más constantes, creo que encargar tareas a cada uno fue acertado pero creo también que a veces éramos grupos grandes de personas y al final había algunos que nos quedamos mirando como otros trabajaban cuando podríamos haber avanzado más por nuestra cuenta. Creo que algunos van SOBRADÍSIMOS en algunos roles que desempeñan otros que no lo están tanto y creo que sí que se podría avanzar más rápido en ciertas partes si repartimos las tareas mejor (que cada uno haga las tareas que se le dan mejor).

Feedback externo
Por lo general hay gente que se encuentra un poco perdida (no sabe qué hacer en ciertos momentos o no saben qué hacen ciertos elementos) o piensa que algunas cosas son injustas.
	
Qué se podría haber mejorado
Haber puesto fechas a las cosas antes, haber repartido mejor algunas tareas (tanto por habilidad para hacerlas como por gente que se queda mirando).

## CAMBIOS
El documento de diseño se encuentra en la versión 1.1, este apartado servirá para comunicar los cambios hechos en futuras versiones.

### Cambios en la version beta
Para esta version se han tenido que descartar algunas cosas debido a que daban problemas inicialmente.
#### Campamento base
##### Puesto del curtidor
No se ha implementado el puesto del curtidor que permití aumentar el tamaño del inventario
##### Tienda de pociones
No se ha implementado la t ienda de pociones que permitía comprar pociones antes de una run, estas aparece en la primera sala de cada piso para que el jjugador tenga una cura en cada una
##### Puesto de misiones
No se ha implementado esta funcion que recompensaba al jugador por jugar según se indicaba en las misiones que seleccionaba.

#### Enemigos
No se han implementado todos los enemigos ni los boses, esto se debe a que la herramienta para hacer la IA de estos se nos explicó la semana antes de la entrega de esta versión.

### Items:
Se han cambiado las pociones por insecto. Se han añadido las imágenes de los items que faltaban de la tabla del apartado items.

Se ha añadido el análisis MDA del videojuego.

## Cambios en la version gold master
### Items:
Se han metido items que además de sumar estadísticas las restan.

### Puzle ardilla:
Si una ardilla se come una bellota y se mata, está ya no la soltará, el jugador la habrá perdido para siempre y no podrá completar el puzle.
La ardilla ahora protege a la ardilla que come poniéndose enfrente del jugador en todo momento.
Ahora hay un altar al que si le entregas 3 bellotas te cura vida.

### Lobo:
Se ha añadido un enemigo para que aparezca en las salas.

### Murciélago:
Se ha añadido un enemigo para que aparezca en las salas.

### Duendecillo Mago:
Ahora el mago cuando la vida total es inferior a un porcentaje, todos los enemigos van a curarse cerca del mago y este deja de atacar.

### David el Gnomo y Árbol Maligno:
Se han mejorado las animaciones de los enemigos y su interacción con el jugador y el mundo.

### Rediseño de enemigos:
Se han rediseñado el diseño visual de los enemigos para que concuerden con otros.

### Animaciones Kero:
Se han añadido animaciones de atacar de Kero y cuando recibe daño.

### Controles:
Se ha mejorado los controles de ataque del móvil y del PC.

### Personalización móvil:
Se le da al usuario la opción de personalizar los controles de ataque del móvil en el menú de pausa.

### NPCs:
Se han añadido nuevos NPCs para la interacción con el jugador.

### Tutorial interactivo:
Se han añadido un tutorial interactivo que aparece la primera vez que inicia el jugador.

### Textos al morir:
Se han añadido varios textos para dotar de más narración al juego.

### UI:
Se ha terminado de hacer totalmente adaptable la interfaz de usuario.

### Balas:
Ahora las balas se destruyen al chocar con algo.

### BUG puertas:
Se ha corregido el bug que impedía proseguir con la expedición cuando te pasas un jefe.

### Arreglos tienda:
Ahora se guardan correctamente los objetos comprados en la tienda.

### Campamento base:
Se ha mejorado el campamento base haciendo más fácil al jugador la navegación.

### Correcciones menores:
Se han hecho correcciones de ortografía, de visibilidad, de jugabilidad y mejoras en el código.


## Enlace de lector al GDD en Google docs
https://docs.google.com/document/d/1l6Qfy3xQ0anlrgdIv_5JKlhzeY_67oTvJoi1X06fd6Q/edit?usp=sharing

[conceptPJ]: ImagesGDD/FINAL_CONCEPT.jpg

[dagaSolar]: ImagesGDD/DagaSolar.png
[dagaLunar]: ImagesGDD/DagaLunar.png
[dagaUsada]: ImagesGDD/DagaUsada.png
[dagaPulida]: ImagesGDD/DagaPulida.png
[plumaDañada]: ImagesGDD/PlumaDañada.png
[plumaBúho]: ImagesGDD/PlumaBúho.png
[plumaGrifo]: ImagesGDD/PlumaGrifo.png
[plumaFénix]: ImagesGDD/PlumaFénix.png
[capaUsada]: ImagesGDD/CapaUsada.png
[capa]: ImagesGDD/Capa.png
[capaCazador]: ImagesGDD/CapaCazador.png
[capaViento]: ImagesGDD/CapaViento.png
[escamaVieja]: ImagesGDD/EscamaDañada.png
[escama]: ImagesGDD/Escama.png
[escamaPulida]: ImagesGDD/EscamaPulida.png
[escamaDragón]: ImagesGDD/EscamaDragón.png
[moscas]: ImagesGDD/Moscas.png
[abejas]: ImagesGDD/Abejas.png
[libélula]: ImagesGDD/Libélula.png
[keroCoin]: ImagesGDD/KeroCoin.png

[mapaEmpatia]: ImagesGDD/Empatia.png
[fantasma]: ImagesGDD/Fantasma.png
[murcielago]: ImagesGDD/Murcielago.png
[murcielago1]: ImagesGDD/MurcielagoCor1.png
[murcielago2]: ImagesGDD/MurcielagoCor2.png
[buho]: ImagesGDD/Buho.png
[arbusto]: ImagesGDD/Arbusto.png
[hierbajo]: ImagesGDD/Hierbajo.png
[puentes]: ImagesGDD/Puentes.png
[bn1]: ImagesGDD/BN1.jpg
[bn2]: ImagesGDD/BN2.jpg
[bn3]: ImagesGDD/BN3.jpg
[bn4]: ImagesGDD/BN4.jpg
[bn5]: ImagesGDD/BN5.jpg
[bn6]: ImagesGDD/BN6.jpg
[bn7]: ImagesGDD/BN7.jpg
[bn8]: ImagesGDD/BN8.jpg
[bn9]: ImagesGDD/BN9.jpg
[bn10]: ImagesGDD/BN10.jpg
[bn11]: ImagesGDD/BN11.jpg
[bn12]: ImagesGDD/BN12.jpg
[bn13]: ImagesGDD/BN13.jpg
[bn14]: ImagesGDD/BN14.jpg
[bn15]: ImagesGDD/BN15.jpg
[bn16]: ImagesGDD/BN16.jpg
[calavera]: ImagesGDD/Calavera.jpg
[pili]: ImagesGDD/Pili.jpg
[modelCanvas]: ImagesGDD/modelCanvas2Fina.png
[toolBox]: ImagesGDD/toolbox.png
[r1]: ImagesGDD/R1.png
[r2]: ImagesGDD/R2.png
[Duende]: ImagesGDD/Duende.png
[b1]: ImagesGDD/B1.png
[f1]: ImagesGDD/F1.png
