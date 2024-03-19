# Targets

Tipos de _targets_

- Image
- Cylinder
  - Image target de forma cilíndrica o cónica
- Multi
  - Posiciona los targets en una forma cúbica
  - Se usa en el proyecto de [eduacacion](/CursoAR/Educacion/)
  - Target ID en [BD](https://developer.vuforia.com/vui/develop/databases/ed3e202fc2764008bcc67397962c4a39/targets/7cb9f4b6a91647589e3437a9bf09faf4) Vuforia
  - Cómo armar un merge cube [(tutorial)](https://www.youtube.com/watch?v=zEeVO1woFxI&ab_channel=TERALAB)
- Model
  - Se carga un modelo dentro de la aplicación y se crea un contorno. Cuando se matchea el contorno con el modelo real se activa el modelo 3d.
- Ground plane
  - Detecta el suelo, genera un mapa y sobre este coloca el objeto
  - El teléfono necesita ciertos sensores
- Cloud recognition
  - La base de datos de los targets están en la nube y no en el teléfono
  - Vuforia cambia los códigos con demasiada frecuencia
- Barcode scanner
- Device / position
  - Uso los sensores del teléfono
  - Ej. Pokémon go
  - El dispositivo necesita giroscopio y acelerómetro
- VuMark
  - Versión avanzada de un QR
  - Variaciones con un mismo dibujo
  - Muchos problemas pq con aunque sea pequeñas variaciones falla.
- User defined
  - Le permite al usuario asignar los targets
  - Muchos usuarios no saben usar la app, malos usos generan mala experiencia.
- Object recognition
  - Escaneamos el objeto y generamos puntos de detección
  - Llega a tener muchos errores y inestabilidad en el modelo

**Características de un buen target:**

- Rico en detalles
  - Los detalles que sirven para generar puntos son esquinas, sufre con los círculos
  - El exclusión buffer ignora el borde (8%) durante la detección
    - Para targets no rectangulares se les puede poner un borde para que se detecten
- Buen contrastre
- Sin patrones repetitivos
