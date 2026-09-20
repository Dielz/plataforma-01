# 📚 Documentación del Proyecto - Plataforma Game

Bienvenido a la documentación oficial del juego de plataformas desarrollado en Godot .NET.

## 📖 Navegación Rápida

| Documento | Propósito | Qué Encontrarás |
|-----------|-----------|-----------------|
| **[game-design.md](./game-design.md)** | Diseño del Juego | Mecánicas, enemigos, niveles, power-ups |
| **[development-guide.md](./development-guide.md)** | Configuración Técnica | Setup GDExtension, mobile setup, API references |
| **[project-manager.md](./project-manager.md)** | Seguimiento de Proyecto | Tareas pendientes, roadmap, bugs conocidos |

---

## 🗂️ Estructura de la Documentación

```
doc/
├── game-design.md          ← Diseño completo del juego
├── development-guide.md    ← Guía técnica y configuración
├── project-manager.md      ← Tracking y planificación
└── index.md                ← Este archivo (navegación)
```

---

## 📋 Resumen por Documento

### 🎮 game-design.md
**Para:** Diseñadores, artistas, testeo de gameplay

Contiene:
- Mecánicas de juego detalladas (jugador, enemigos, trampas)
- Estructura y progresión de niveles
- Sistema de vidas y power-ups
- Especificaciones de controles (desktop/mobile)
- Requisitos técnicos por plataforma

### ⚙️ development-guide.md  
**Para:** Desarrolladores C#/.NET, DevOps

Contiene:
- Instrucciones de instalación completa
- Configuración Godot .NET GDExtension paso a paso
- Estructura de carpetas del proyecto
- Convenciones de codificación en C#
- Setup específico para Android/iOS exportación
- Optimización de assets para diferentes plataformas

### 📊 project-manager.md
**Para:** Todo el equipo, seguimiento de progreso

Contiene:
- Checklist de tareas pendientes por prioridad
- Roadmap de sprints y milestones
- Bug tracker con plantilla de reporte
- Métricas de progreso del proyecto
- Hitos de lanzamiento (alpha, beta, RC)

---

## 🚀 Inicio Rápido para Nuevos Miembros

1. **Lectura Obligatoria:**
   - [development-guide.md](./development-guide.md) - Configuración
   - `README.md` en raíz - Overview del proyecto

2. **Seguimiento Diario:**
   - Revisar `doc/project-manager.md` por la mañana
   - Actualizar lista de tareas completadas al día

3. **Antes de Empezar una Feature:**
   - Consultar `doc/game-design.md` para especificaciones
   - Verificar `doc/project-manager.md` para prioridades actuales

---

## 📁 Ubicación de Assets

Los assets importados se encuentran en:

```
res/assets/
├── Sprites/           → Sprites individuales (.png)
├── Audio/             → Sonidos y música
├── Backgrounds/       → Fondos de nivel
├── Spritesheets/      → Hojas de sprites (si aplica)
└── Vector/            → Arte vectorial (si aplica)
```

**Licencia:** Assets de Kenney.nl - Ver [res/assets/License.txt](./License.txt)

---

## 🔗 Enlaces Externos

- [Godot Engine 4.x](https://godotengine.org/)
- [GDExtension Dotnet GitHub](https://github.com/godot-rust/gdextension-dotnet)
- [Kenney Assets](https://kenney.nl/assets/new-platformer-pack)

---

## 📝 Contribuyendo a la Documentación

Si encuentras errores o quieres añadir información:

1. Edita el archivo correspondiente
2. Comenta claramente los cambios
3. Mantén un tono profesional y claro
4. Prueba que todos los enlaces funcionan

---

**Última actualización:** 19 Septiembre, 2026  
**Estado:** Activo - En desarrollo junto con el juego