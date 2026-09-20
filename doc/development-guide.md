# Guía de Desarrollo - Plataforma Game

## Configuración del Entorno

### Requisitos Previos

#### Para Desarrollo en Desktop
```bash
1. Descargar e instalar Godot 4.x (recomendado: Godot v4.2.1.stable)
2. Instalar .NET SDK 6.0 o superior
3. Git para control de versiones
4. Visual Studio / VS Code con extensiones GDScript/C#
```

#### Para Desarrollo Mobile
- Crear archivo `.project` configurado para exportación mobile:
  ```
  project.godot (configurar en File -> Project Settings)
  - Export Platform: Android + iOS
  - Render Method: Vulkan / GLES3
  - DPI Scale: Auto
  ```

### Configuración de Godot .NET Extension

1. **Instalar GDExtension**:
   - Descarga desde https://github.com/godot-rust/gdextension-dotnet/releases
   - Colocar `.so/.dll` en `res://godot_plugins/`

2. **Configurar project.godot**:
   ```
   [gdextension]
   extension_paths=["res://godot_plugins/"]
   
   [rendering]
   renderer/driver=vulkan
   renderer/rendering_method=gpu_raster
   ```

3. **Compilar GDExtension**:
   ```bash
   dotnet build -c Release
   ```

## Estructura del Proyecto

### Organización de Archivos

```
plataforma-01/
├── .gitignore
├── project.godot
├── res/
│   ├── assets/
│   │   ├── Sprites/
│   │   ├── Audio/
│   │   └── Backgrounds/
│   ├── scenes/
│   ├── scripts/
│   └── types/
├── doc/
│   ├── game-design.md
│   ├── development-guide.md
│   └── project-manager.md
└── .git/ (excluido de gitignore)
```

### Esquema de Nomenclatura

**Carpeta `scenes/`:**
- `main.tscn` - Escena principal del juego
- `levels/level_1.tscn`, `level_2.tscn`, etc.
- `ui/main_menu.tscn`, `game_over.tscn`

**Carpeta `scripts/` (C# .NET):**
- `Player.cs` - Lógica del jugador (.cs con extensión GDExtension)
- `EnemyBase.cs` - Clase base para enemigos
- `Trap.cs` - Sistema de trampas
- `LevelManager.cs` - Gestión de niveles

**Carpeta `types/`:**
- DTOs y modelos de datos
- Entidades compartidas entre C# y GDScript

## Convenciones de Codificación

### Nombres de Archivos
- `camelCase` para scripts en C#
- `kebab-case` o `snake_case` según preferencia (C# usa camelCase)
- Nombres descriptivos: `level_1_intro.tscn`, not `scene1.tscn`

### Estructura de Clases

```csharp
public class Player : CharacterBody2D
{
    [Export] public float JumpVelocity = 500f;
    [Export] public int Health = 3;
    
    private Vector2 _velocity;
    private bool _isJumping;
    private LevelManager _levelManager;
    
    // Métodos públicos solo cuando sea necesario
    public override void _PhysicsProcess() { ... }
    public void TakeDamage(float amount) { ... }
}
```

### Comentar Código
- Usar XML docs para API pública: `<summary>...</summary>`
- Comentarios en bloque para lógica compleja
- `//` para comentarios de una línea

## Configuración Móvil

### Android
1. **Gradle Setup**:
   ```bash
   # En project.godot
   export/variants/android/app/build.gradle
   - minSdkVersion 24+
   - targetSdkVersion 33+
   ```

2. **Assets Optimizados**:
   - Texturas: 1024x1024 o menos
   - Sonidos: AAC, 48kHz mono
   - Iconos: múltiples tamaños (mdpi, hdpi, xhdpi, xxhdpi)

3. **Permisos Necesarios**:
   ```xml
   <uses-permission android:name="android.permission.INTERNET"/>
   <uses-permission android:name="android.permission.VIBRATE"/>
   ```

### iOS
- Archivo `Info.plist` para permisos:
  - NSPhotoLibraryUsageDescription (si hay capturas de pantalla)
  - NSMicrophoneUsageDescription (si hay voz)

## Pipeline de Desarrollo

1. **Diseño en Papel**: Esbozar niveles y mecánicas
2. **Prototipado Rápido**: Bloques simples sin arte
3. **Arte y Audio**: Importar assets, ajustar colores/animações
4. **Implementación Lógica**: Programar mecánicas
5. **Polishing**: Partículas, sonidos, balanceo
6. **Testing**: Desktop primero, luego mobile

## Testing

### Unit Tests (C#)
```csharp
[Fact]
public void Player_ShouldJumpWhenGrounded() {
    // Test implementation
}
```

### Playtesting Checklist
- [ ] Movimiento fluido sin inercia extraña
- [ ] Todos los saltos alcanzables
- [ ] Enemigos claros (patrón de ataque visible)
- [ ] UI responsive en móviles
- [ ] Sin fugas de memoria

## Notas para .NET Extension

### Serialización de Tipos C# a Godot
```csharp
// Registrar tipos en GDExtension
public static class TypeManager
{
    public static void RegisterTypes()
    {
        // Registrar GameLevel, Player, Enemy, etc.
    }
}
```

### Interoperabilidad con Godot
```csharp
// Usar GodotObject como base
public partial class LevelManager : GodotObject
{
    [Signal] public delegate void LevelCompletedSignal();
    
    public void LoadLevel(string levelName) { ... }
}
```