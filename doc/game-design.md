# Plataforma Game Design Document

## Visión General
Juego de plataformas 2D inspirado en Mario, desarrollado con Godot 4.x en C# (.NET).

### Objetivo
Crear una experiencia divertida y desafiante donde los jugadores superen niveles progresivos saltando sobre plataformas, evitando enemigos y resolviendo trampas.

### Mecánicas Principales

#### Movimiento del Jugador
- Salto básico y doble salto (opcional)
- Correr acelerado
- Pantallazo de impacto
- Gravedad ajustable

#### Sistema de Enemigos
- **Goombas**: Enemigos básicos que giran cuando el jugador se acerca
- **Koopa Troopa**: Se rompen al ser pisados y pueden reactivarse
- **Fire Pea**: Lanza proyectiles a distancia
- **Bullet Bills**: Disparan rectilíneamente

#### Trampas
- Pistones con pinchos
- Bloques de fuego
- Plataformas que se mueven
- Agua venenosa (zona segura solo para personajes específicos)

#### Sistema de Power-ups
- **Flor de Fuego**: Proyectiles inflamables
- **Hongo Gigante**: Tamaño aumentado
- **Estrella**: Invencibilidad temporal

## Estructura de Niveles

### Tipos de Niveles

1. **Nivel Introductorio**
   - Enseña el salto básico
   - Plataformas estáticas sencillas
   - Enemigos básicos (Goombas)

2. **Nivel Medio**
   - Introducen trampas simples
   - Enemigos más agresivos
   - Power-ups básicos

3. **Nivel Avanzado**
   - Trampas complejas
   - Enemios con patrones de movimiento
   - Multiple zonas de peligro

4. **Nivel Final**
   - Todas las mecánicas combinadas
   - Jefe final
   - Zona secreta opcional

## Sistema de Progresión

### Niveles y Desafíos
Cada nivel debe completar:
- Alcanzar la bandera/objetivo
- Salto mínimo requerido: X veces
- Enemigos eliminados: Y cantidad
- Sin caerse al vacío (opcional)

### Sistema de Vidas
- 3 vidas estándar
- Power-up extra vida temporal
- Checkpoints en niveles largos

## Requisitos Técnicos

### Plataforma
- Godot 4.x con GDExtension .NET
- Renderizado para desktop y mobile
- Resolución adaptable a pantallas móviles

### Controles
**Desktop:**
- Jump: Barra espaciadora / Click izquierdo
- Run: Shift + Tecla movimiento
- Interact: E / F5

**Mobile:**
- Toque simple para saltar
- Botón de correr opcional
- Deslizar para movimiento lateral

## Archivos del Proyecto

```
res/
├── assets/
│   ├── Sprites/
│   │   ├── player/
│   │   ├── enemies/
│   │   └── backgrounds/
│   ├── Sounds/
│   ├── Spritesheets/
│   └── Vector/
├── scenes/
│   ├── main.tscn
│   ├── levels/
│   ├── ui/
│   └── entities/
├── scripts/
│   ├── Player.gd (con extension .cs)
│   ├── Enemy.gd (con extension .cs)
│   ├── Trap.gd (con extension .cs)
│   └── LevelManager.gd (con extension .cs)
└── types/
    ├── Player.cs
    ├── Enemy.cs
    └── GameLevel.cs
```

## Notas de Desarrollo
- Usar Godot 4.x para rendimiento mejorado
- Implementar lazy loading para mobile
- Optimizar texturas para diferentes densidades (1x, 2x, 3x)