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
       * ¿Quien va a comprar el juego?
       * ¿Quien va a jugar el juego?
    * Publicidad y redes sociales	
    * Modelo de Negocio	
    * Planes de monetización	
6. CAMBIOS	


### INTRODUCCIÓN
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

https://store.steampowered.com/app/113200/The_Binding_of_Isaac/
https://store.steampowered.com/app/1942280/Brotato/
Influencias estéticas de series como Más allá del Jardín u Hora de Aventuras para crear un mundo de fantasía no realista pero si reconocible.

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

###Pociones
Las pociones servirán para curar la vida, al consumirlas el jugador se deberá quedar quieto brevemente y hacer una animación para poder curarse. 
Para usar una poción se deberá hacer desde el menú de inventario, una vez usado se recuperará una cantidad de vida acorde a la calidad de las pociones.
Las pociones no se podrán usar durante el combate, solo cuando se haya acabado con los enemigos de la sala o el jugador esté en una sala de tienda.

###Items
Los objetos pueden aparecer al terminar  una sala y servirán para aumentar estadísticas y en algunos casos alterarán aspectos del personaje como el tipo de daño que hace o los estados que puede infligir, en versiones posteriores se implementarán más atributos que puedan dar los objetos, como sangrado, veneno, quemaduras…
Los objetos tienen 4 **rarezas**: común, raro, épico, legendario. La rareza de los objetos determinará las stats que da al jugador y el precio de venta, además, los objetos de rareza superiores le podrán dar al jugador más de una estadística. 
Más adelante en el juego, cuando el jugador desbloqueé la capacidad de ver auras, podrá ver el aura de los objetos en el bosque, este aura afecta al precio de venta y su daño y determina el estado del objeto en 5 categorías: Maltrecho, - 5% de su precio ; sin aura, tiene el precio de venta y estadísticas normal ; buen estado, + 10% de su precio de venta y estadísticas; perfecto estado, +15% de precio de venta y estadísticas; purificado, +20% de su precio de venta, estadísticas y un efecto sumado aleatorio. Estos porcentajes son orientativos y cambiarán cuando se haya testeado si impactan mucho o poco el gameplay.
Enlace a la hoja de cálculo con los items: https://docs.google.com/spreadsheets/d/1nqJ6MIjXAtS5HyprhY6oJlgkHAHZMD0HF10bQ-MYcy4/edit?usp=sharing


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

Daga Solar



Daga Lunar


##DISEÑO
###Personaje
Kero es el protagonista.
El personaje busca ser desenfadado y familiar. Un personaje simple del que poder encariñarse y ver como va cambiando a medida que el jugador desarrolla su manera de jugar.


###Niveles
El mapa del videojuego está generado aleatoriamente mediante un algoritmo, este mapa consiste en una serie de salas a las que llamaremos niveles organizadas en tres pisos. Estos pisos cuentan con 7-10 niveles por cada piso. A medida que se vaya pasando de piso, la estética y dificultad cambiará para balancear la curva de dificultad.

Los niveles se dividen en 4 categorías: niveles de desafío, niveles de jefe, niveles con tienda y niveles de puzzle. Cada uno de estos devuelve una recompensa diferente e involucra a un tipo de enemigo distinto.

Niveles de desafío: estos niveles son los más comunes entre todos. Se componen de enemigos parcialmente aleatorios (ya que controlaremos la aleatoriedad) y obstáculos que dificultan al jugador. 
Objetivo: el único objetivo de esta sala es matar a todos los enemigos, cuando esto se haga se abrirá la puerta. 
Recompensa: este nivel no cuenta con una recompensa asegurada aunque los enemigos tienen posibilidad de soltar objetos cuando mueren.
Enemigos: cualquier tipo de enemigo
Niveles de jefe: son los más difíciles de completar porque cuentan con enemigos mucho más complicados. Estos niveles no cuentan o tienen muy pocos obstáculos porque la dificultad residirá en el enemigo.
Objetivo: matar a todos los enemigos presentes en la sala (las salas no tienen solo por qué contar con un solo enemigo fuerte, este puede estar acompañado de enemigos comunes para aumentar la dificultad.
Recompensa: el nivel de jefe cuenta con las mejores recompensas entre todas las salas. Estas son objetos de mucha rareza que serán muy valiosos para el jugador.
Enemigos: Jefes y enemigos comunes o modificados.
Niveles de tienda: estos niveles no tienen enemigos en ellos, simplemente cuentan con una pequeña tienda con un tendero donde el jugador puede comprar vida, objetos y mejoras para las habilidades
Objetivo: ninguno en específico, el jugador no tiene por qué comprar nada y puede salir cuando desee
Recompensa: los objetos y mejoras proporcionadas por el tendero
Enemigos: ninguno
Niveles de puzzle: estos niveles son los más complejos. Contaremos con 4 tipos de niveles de puzzle y cada uno de ellos tendrá diferentes distribuciones de enemigos y obstáculos. Los cuatro tipos son:
King of the hill: dentro del nivel aparecerá una zona destacada de la sala donde cuanto más tiempo esté el jugador más recompensas obtendrá. Estás recompensas serán principalmente oro, si el jugador consigue estar más de 15 segundos en la zona se le recompensará con un objeto.
Objetivo: estar el máximo tiempo posible en la zona destacada.
Recompensa: una moneda por segundo dentro. Si el jugador llega a más de 15 segundos además se recompensará con un objeto
Enemigos: Perdidos, ardillas y fantasmas.
Duración: 20 segundos, una vez terminados la zona destacada desaparecerá y los enemigos morirán.
Recoge las bellotas: en el nivel aparecerán un número de bellotas (entre 4-5 bellotas). El jugador deberá recoger las bellotas y dejarlas en un pedestal de piedra para que se pueda abrir la puerta. Los enemigos podrán coger las bellotas y para recuperarlas deberás matarlos, una vez muertos las bellotas caen al suelo.
Objetivo: recoger las bellotas y dejarlas en el pedestal
Recompensa: una vez dejadas las bellotas en el pedestal, este te proporcionará vida
Enemigos: ardillas, duendes y duendes mágicos
La calavera dorada: en el nivel se encontrará una calavera dorada tirada en algún lugar de la sala y un mayor número de enemigos que de costumbre. Cuando el jugador coja la calavera dorada todos los enemigos morirán. Por cada enemigo que mate la calavera (que no haya muerto antes de cogerla) se soltará una moneda. 
Objetivo: coger la calavera lo antes posible
Recompensa: una moneda por cada enemigo que mate la calavera
Enemigos: perdidos, lobos, duendes o magos
Apaga el incendio: en este nivel el jugador se encontrará con una serie de fuegos ardiendo, estos fuegos tienen recompensas de interés debajo (oro y objetos). Para obtenerlos el jugador debe apagar lo más rápidamente los fuegos. Para apagar un fuego el jugador deberá pegarlo tres veces. El jugador no podrá apagar todos los fuegos por lo que deberá escoger qué objeto es más valioso o más arriesgado coger. También habrá enemigos que el jugador deberá matar o esquivar. El nivel no se acabará hasta que todos los enemigos mueran
Objetivo: salvar los objetos y matar a los enemigos
Recompensa: objetos u oro
Enemigos: fantasmas y magos
Tiempo: los fuegos quemarán el objeto en 5-7 segundos




Chrono Trigger. Sala con más posible cercanía a una posible cuarto(no todas las salas tienen que tener 4 paredes) un claro rodeado por árboles. 

Legend of Zelda Link to the past. Puede ser para referenciar una sala de enemigos y/o puzzle. Por ejemplo no le dejan pasar los guardias rojos si no mata a los enemigos y si pone los jarrones en un orden abrirá la puerta derecha o izquierda.

The binding of  Isaac. Una sala donde hay que acabar con los enemigos, hasta que no lo hagas no te deja salir a la siguiente sala.



Enemigos
Por ahora estos son de perseguir o huir. Iremos desarrollando estas IA a medida que avancemos con la asignatura de Desarrollo de Personajes.

Fantasma
Vida: 1 (el valor va aumentando en 1 en base a la sala)

Tipo de movimiento: Persigue lentamente al jugador (su velocidad aumenta según se avanza en el juego).

Velocidad de movimiento: Un 10% inferior a la velocidad de movimiento del jugador.

Ataque: Quita poco a poco vida al jugador.

Debilidad: El enemigo es débil a todos los ataques


Lobo

Vida: 10 (el valor va aumentando en 1 en base a la sala)

Tipo de movimiento: Se mantiene alejado del jugador.

Velocidad de movimiento:Un 15% superior a la velocidad de movimiento del jugador.

Ataque: Realiza un dash hacia el jugador.

Debilidad: Si impacta contra una pared al  realizar el dash se queda aturdido durante 2 segundos.

Variante superior: no se aturde al impactar contra las paredes.

Duendecillo

Vida: 6 (el valor va aumentando en 1 en base a la sala)

Tipo de movimiento: Se teletransporta detrás del jugador con tiempo de espera entre cada movimiento.

Velocidad de movimiento:Igual a la del jugador.

Ataque: Se teletransporta detrás del jugador y a los dos segundos ataca con un cuchillo, poco rango.

Debilidad: Entre un teletransporte y otro está indefenso.

Duendecillo mago

Vida: 5 (el valor va aumentando en 1 en base a la sala)

Tipo de movimiento: Busca alejarse del jugador.

Velocidad de movimiento:Un 10% menos que la del jugador.

Ataque: Lanza una bola de energía que desaparece delante suya y aparece detrás del jugador en dirección suya.

Debilidad: Lento disparando y mientras huye no puede atacar.

Carne de cañón (ardillas por ejemplo)

Vida: 3 (el valor va aumentando en 1 en base a la sala)

Tipo de movimiento: Se mueve hacia al jugador

Velocidad de movimiento:Un 20% menos que la del jugador.

Ataque: Hace daño por contacto con el jugador

Debilidad: Poco resistentes.

Los perdidos (probablemente murciélagos o similar volador)

Vida: 1 

Tipo de movimiento: Aleatorio (como si rebotaran en las paredes)

Velocidad de movimiento: Igual a la del jugador

Ataque: Hace daño por contacto con el jugador, cada vez que te golpea uno el siguiente ataque de otro te hace más daño así infinitamente.

Debilidad: Poco resistentes (oneshot).


Jefes
David el gnomo

Vida: 100 (aparece al final de la primera área)

Tipo de movimiento: Mantiene la distancia pero si el jugador se acerca va a por él.

Velocidad de movimiento:Un 20% menos que la del jugador.

Ataque 1: Golpea el suelo y crea una onda expansiva.

Ataque 2: Derechazo al jugador si se encuentra cerca.

Ataque 3: Lanza rocas al aire que caen en la zona en la que esté el jugador.

Habilidad (Soy diez veces más fuerte que tu): Cuando le queda un 20% de vida aumenta su velocidad en un 30% y hace un 25% más de daño durante 10 segundos (puede ser que sume el daño del jugador al suyo), después debe descansar durante 5 segundos.

Debilidad: sus golpes son fáciles de esquivar.

Corazón del bosque

Vida: 150 (aparece al final de la primera área)

Tipo de movimiento: No se mueve, se encuentra ubicado en la zona derecha de la sala.

Velocidad de movimiento: 0

Ataque 1: Raíces salen debajo del jugador.

Ataque 2: Las raíces salen formando una pequeña área cerca de él.

Ataque 3: Lanza sus frutos que explotan al impactar con los disparos del jugador o al segundo de quedarse quietos.

Habilidad (Cuida la naturaleza): Se añade un escudo que reduce el daño un 30% cuando le queda la mitad de la vida.

Debilidad: Ataques predecibles que intenta compensar con una gran cantidad de vida.

It was me, the mage

Vida: 180 (aparece al final de la segunda área)

Tipo de movimiento: Mantiene la distancia todo lo posible

Velocidad de movimiento: Velocidad del jugador.

Ataque 1: Lanza un orbe rojo en dirección al jugador que va acelerando.

Ataque 2: Lanza un orbe morado en dirección al jugador, si el orbe impacta contra una pared vuelve a aparecer en una de las otras paredes y vuelve a ir a por el jugador máximo ocurre dos veces.

Ataque 3: dispara 4 orbes azules en direcciones aleatorias y rebotan en las paredes el jugador puede destruirlos o desaparecen al cabo del tiempo.

Habilidad: Cuando el jugador está cerca suya durante más de 5 segundos se pone una barrera que empuja al jugador al lado contrario de la sala.

Debilidad: Pasa bastante tiempo entre ataque y ataque.

El encontrado

Vida: 200 (aparece al final de la segunda área)

Tipo de movimiento: Se va moviendo por la sala.

Velocidad de movimiento: Un 30% más lento que el jugador.

Ataque 1: Lanza ácido que se queda en el suelo durante un tiempo determinado, el jugador recibe daño al pasar por encima.

Ataque 2: Invoca a los perdidos, estos son especiales en vez de aumentar el daño infinito cada vez que golpean al jugador el encontrado.

Ataque 3: Acelera y realiza tres ataques directos seguidos a por el jugador

Habilidad (Sin límites): Su vida no tiene límites es decir con la curación de su segunda habilidad puede sobrepasar sus 200 de vida inicial y cuanto menos vida tenga más se cura.

Debilidad: A determinar.

Campamento Base
El Campamento Base es el lugar al que el jugador regresa tras las Expediciones para conseguir nuevo equipamiento y abastecerse de objetos para su próxima expedición.
Debe tener un aspecto acogedor y cada puesto tiene que tener una identidad reconocible.
Herrería
La herrería es el lugar en el que el jugador puede conseguir nueva y mejor armadura
El jugador puede comprar equipamiento básico.
O puede crear equipamiento con los materiales que consiga de las expediciones.
Armería
Funciona de forma similar a la Herrería, se pueden comprar armas básicas, pero las mejores se crean con materiales 
Tienda de pociones
Las pociones se compran con oro.
El juego comienza con solo pociones de vida básicas, puedes conseguir mejorar las pociones dándole al alquimista los materiales necesarios para ello.
//Versiones posteriores, poder añadir efectos como rapidez de consumo, menos espacio en el inventario, o aumentar brevemente estadísticas.
Cabaña Mística
Los talismanes ocupan espacio en el inventario y otorgan estadísticas adicionales, como defensa, ataque o velocidad de movimiento.
Los talismanes tienen se pueden usar en dos expediciones, tras eso desaparecen del inventario.
Se pueden conseguir a cambio de oro y se encuentran disponibles de forma rotativa en la tienda.
Casa
Es el hogar del personaje en el campamento, aquí el jugador puede cambiar su equipamiento y su clase; además puede organizar su inventario antes de salir de expedición. 
En la entrada al bosque también habrá un baúl que permite algunas de estas opciones.
Tienda de ítems
Cuando se entra el la tienda se pueden llegar a intercambiar un máximo de tres objetos, los objetos que aparecen en la tienda son aleatorios pero corresponden con la rareza del equipamiento que llevas puesto ( es decir si llevas un peto épico en la tienda puede aparecer otro peto épico por el que puedes intercambiarlo). El valor de las piezas que cambies se igualará para obtener el mismo resultado en tu oró que si hubieras optado por no cambiarlo. 
Además en cada tienda (pueden aparecer máximo 1 vez por área) habrá un item que te permite recuperar la mitad de tu vida, si ya lo has comprado previamente su precio aumentará.
La tienda de 
ARTE
Interfaces
Flujo de Pantallas
Menú Principal
La interfaz de usuario del menú principal será sencilla y minimalista. Estará compuesta de un fondo con el personaje inicial “Kero” el nombre del videojuego “Beyond the trees” con su correspondiente tipografía y un escenario con temática de bosque encantado, y tres botones, el botón de jugar, el de opciones y el de créditos.

Créditos
La interfaz de créditos es accesible a partir del botón correspondiente en el menú principal.
Esta pantalla muestra los nombres de todos los desarrolladores implicados en el proyecto y un mail del contacto de la empresa. Además incluirá un botón de volver al menú principal.

Opciones
La pantalla de opciones tiene las siguientes preferencias: modo de pantalla completa en el que el jugador podrá marcar la casilla según como quiera visualizar el juego, modificar volumen de la música y por último, el jugador puede seleccionar la calidad gráfica que tendrá en videojuego. El botón de volver también está presente para volver al menú principal.

Login
Tras dar al botón de jugar, aparecerá la pantalla de login en la que el usuario deberá introducir sus datos. El jugador podrá elegir su género y deberá introducir un nombre de usuario. Una vez hecho todo, el botón de empezar llevará al inicio del juego  al igual que la interfaz anterior, esta tiene el botón de volver para regresar al menú principal.
En Juego
La interfaz in-game de Beyond the Trees opta por ser afable y con temática fantástica.
Esta interfaz contará con una barra de vida de color rojo indicando la vitalidad del personaje. Un contador de tiempo que indica el tiempo que lleva jugando el jugador y el dinero que muestra todo el oro con el que cuenta el jugador.


Inventario

Victoria/Derrota


Bocetos
Como primeros bocetos tenemos alguno del personaje en papel. La idea es crear algo simple y fácilmente reconocible. A la derecha se han añadido posibles personalizaciones del personaje cambiándole los ojos.

Arte conceptual
El concept art del personaje es una versión aún más simplificada que el boceto inicial. Lo que se busca es que cualquiera pueda dibujar o reconocer al personaje principal. Esto está pensado para poder llegar al mayor público posible.









Turn-around personaje:


Sprites 

Banda Sonora 

ALCANCE 
El objetivo principal es desarrollar un juego base con un conjunto de niveles ya hechos al que mediante DLCs, se le pueden añadir muchos más niveles.
Público objetivo
Quien va a comprar el juego. 
Profesores, instituciones educativas, editoriales y el gobierno. Padres e instituciones educativas tienen que sentirse atraídos hacia la idea del juego. Tenemos dos principales atractivos para este grupo, una estética familiar que no les parezca demasiado disparatada o inadecuada para los niños; y el más importante un juego capaz de medir y mejorar el pensamiento computacional de los niños, una faceta clave del currículo escolar.

Quién va a jugar el juego. 
El juego está dirigido a niños de entre 3 y 12 años haciendo especial énfasis al grupo de 8 a 12. Con partidas rápidas que permiten invertir una pequeña cantidad de tiempo, perfectas para jugar entre descansos, con el fin de obtener resultados analíticos.

Publicidad y redes sociales
Para publicitar el juego se va a dividir el proceso en dos etapas:
-Desarrollo: Mientras el juego esté en proceso de desarrollo se va a hacer una campaña por las redes sociales donde se pretenderá crear una comunidad de personas que sigan el proceso de creación del videojuego, para ello se ha de hacer publicaciones constantes en redes sociales. Se ha optado por las siguientes redes sociales para promocionar el videojuego: Twitter, Youtube y Tiktok. Se han elegido estas redes sociales debido a su facilidad para la exposición al público, ya que nuestro objetivo en esta fase es que llegue al máximo de personas posibles.

-Juego terminado: Una vez que tengamos el videojuego desarrollado, nuestro objetivo será que a los posibles compradores les llegue el producto. Para ello se ha decidido contactar con editoriales e instituciones educativas, esto se hará acudiendo presencialmente a escuelas y ferias como “AULA” donde se presentará el producto como una herramienta para aprender de forma inconsciente y se presentarán las métricas elegidas previamente. También se contactará con posibles clientes mediante métodos a distancia como correos electrónicos.

Modelo de Negocio

Usaremos un modelo Freemium (Free + Premium) ofreciendo el juego de manera totalmente gratuita a los usuarios, teniendo estos la opción de ver métricas más completas y avanzadas si pagan la actualización a la versión completa. La versión completa son X € 	(Qué os parecen 3€???) haciendo descuento si se compran más de 100 unidades.

Planes de monetización
F2P con futuros dlcs de pago.





IDEAS
Los pj tienen ultis 
L. Las armas tienen espacio para gemas elementales, que le permiten tener distintas propiedades elementales o ataques especiales. Tipo materias en FFVII

El juego cuenta con 3 pj cada uno de estos con sus respectivas estadísticas:

PJ
VIDA
DAÑO
VEL
HABILIDAD
INV
Caballero
4
2
2
3 segundos de stats mejoradas
2
Explorador
2
3
4
Rodar
1
Arquero
3
2
2
lanza 3 flechas en forma de cono
3







 Cambios

