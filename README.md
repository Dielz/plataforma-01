# 🎮 Plataforma Game

<div align="center">
  <img src="./res/assets/Preview (Characters).png" width="400"/>
</div>

<div align="center">
  <h3>Juego de plataformas 2D en Godot .NET para Desktop y Mobile</h3>
</div>

## 🚀 Inicio Rápido

### Prerrequisitos

**Para Desarrollo Desktop:**
```bash
# Descargar e instalar Godot 4.x con soporte para .NET
# Instalar .NET SDK 6.0+
git clone https://github.com/Dielz/plataforma-01.git
cd plataforma-01
```

### Ejecución Rápida

1. **Abrir en Godot:**
   ```bash
   File → Open Project → plataform01/
   ```

2. **Ejecutar:**
   - File → Run Main Scene
   - O presiona F5 desde el editor

## 📁 Estructura del Proyecto

```
plataforma-01/
├── .gitignore           # Archivos excluidos de Git
├── project.godot        # Configuración del proyecto Godot
├── icon.svg             # Icono del juego
├── res/                 # Recursos del juego
│   ├── assets/          # Sprites, audio, texturas
│   │   ├── Sprites/     # Imágenes individuales
│   │   ├── Audio/       # Sonidos y música
│   │   └── Backgrounds/ # Fondos de nivel
│   ├── scenes/          # Escenas (.tscn)
│   ├── scripts/         # Scripts en C# (.cs)
│   └── types/           # Modelos de datos
├── doc/                 # Documentación
│   ├── game-design.md   # Diseño del juego
│   ├── development-guide.md  # Guía técnica
│   └── project-manager.md     # Seguimiento del proyecto
└── .git/                # Git (no commitear manualmente)
```

## 🎮 Características

### ✅ Implementadas (Fase 1):
- **Mecánicas de Plataforma**: Salto básico, correr, inercia suave
- **Enemigo Básico**: Goomba con patrón de colisión clásico
- **Sistema de Trampas**: Pistones con cooldown y activación por proximidad
- **Gestión de Niveles**: Carga desde archivos .tscn, reinicio automático
- **Sistema de Daño**: Invencibilidad temporal, pantallazos por impacto

### 🚧 Pendientes (Fases 2+):
- Enemigos avanzados: Koopa, Fire Pea, Bullet Bills
- Power-ups: Flor de fuego, Hongo gigante, Estrella
- Más trampas y obstáculos complejos
- Progresión de niveles avanzada (4 niveles)

## 🛠️ Tecnologías

- **Motor:** Godot Engine 4.7.x
- **Lenguaje:** C# (.NET) Native Interop ✅
- **Renderizado:** D3D12 (Desktop), GLES3 (Mobile)
- **Control de Versiones:** Git + GitHub

## 📜 Licencia

Los assets provienen de Kenney.nl bajo licencia gratuita.
El código fuente está disponible bajo la licencia MIT.

Ver [res/assets/License.txt](./res/assets/License.txt) para detalles de los assets.

---

## 📚 Documentación Completa

| Documento | Descripción | Ubicación |
|-----------|-------------|-----------|
| **Game Design** | Mecánicas, niveles, enemigos | `doc/game-design.md` |
| **Development Guide** | Setup técnico, configuración mobile | `doc/development-guide.md` |
| **Project Manager** | Tracking de progreso, tareas | `doc/project-manager.md` |

---

## 🎯 Roadmap Inmediato

### Sprint 1 (Semana 1-2)
- [ ] Configurar Godot .NET GDExtension
- [ ] Implementar jugador con movimiento básico
- [ ] Primer enemigo (Goomba) funcional
- [ ] Nivel de prueba jugable

### Fase 2 - Alpha (Semanas 3-4)
- [ ] Power-ups y trampas
- [ ] UI/HUD completa
- [ ] Build para Android listo

### Fase 3 - Beta+ 
- [ ] Optimización mobile
- [ ] Más niveles y enemigos
- [ ] Balanceo de dificultad

---

## 🆘 Ayuda y Soporte

### Problemas Comunes

**GDExtension no se carga:**
```bash
# Verificar que el DLL está en la ruta correcta
ls res/godot_plugins/
# Deberías ver: GDExtension.dll para Windows
```

**No se ven los assets:**
- Verificar importación en Godot: `Assets → Import`
- Re-importar manualmente si es necesario

### Preguntas Frecuentes

**¿Cómo exportar para Android?**
1. File → Export Project
2. Configurar Gradle Build Settings
3. Exportar y obtener APK/AAB

**¿Dónde están los scripts?**
- `res/scripts/` - Lógica del juego en C#
- `res/types/` - Modelos de datos (.cs)

### Contribuyendo

1. Fork el repositorio
2. Crear branch desde main
3. Commit con mensajes descriptivos
4. Abrir Pull Request

---

## 📞 Contacto y Recursos

- **Godot Official:** https://godotengine.org/
- **Kenney Assets:** https://kenney.nl/
- **GDExtension Dotnet:** https://github.com/godot-rust/gdextension-dotnet

---

<div align="center">
  <strong>Hecho con ❤️ usando Godot .NET</strong>
</div>