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
# Descargar e instalar Godot 4.x
# Instalar .NET SDK 6.0+
git clone https://github.com/tu-usuario/plataforma-01.git
cd plataforma-01
```

**Para Mobile (Android/iOS):**
```bash
# Configuración adicional de exportación en project.godot
# Ver doc/development-guide.md para detalles
```

### Instalación

1. **Configurar GDExtension .NET:**
   ```bash
   # Descargar GDExtension
   curl -L https://github.com/godot-rust/gdextension-dotnet/releases/download/v0.x/GDExtension.zip \
       -o godot_plugins/GDExtension.zip
   unzip godot_plugins/GDExtension.zip -d godot_plugins/
   ```

2. **Importar Assets:**
   - Los assets de Kenney ya están importados en `res/assets/`

3. **Compilar y Ejecutar:**
   ```bash
   # En Godot: File -> Run
   # O desde terminal:
   mono --debug ./bin/godot_v4.x.windows.build.dll platform.2d
   ```

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

- ✅ **Mecánicas de Plataforma**: Salto, correr, doble salto
- ✅ **Enemigos Variados**: Goombas, Koopa, Fire Pea, Bullet Bills
- ✅ **Power-ups Clásicos**: Flor, Hongo Gigante, Estrella
- ✅ **Trampas Diversas**: Pistones, fuego, plataformas móviles
- ✅ **Progresión de Niveles**: 4 niveles con dificultad creciente
- ✅ **Desktop y Mobile**: Exportación para Windows + Android/iOS
- ✅ **Assets Profesionales**: Paquete completo de Kenney

## 🛠️ Tecnologías

- **Motor:** Godot Engine 4.x
- **Lenguaje:** C# (.NET) vía GDExtension
- **Renderizado:** Vulkan (Desktop), GLES3 (Mobile)
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