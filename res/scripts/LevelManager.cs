using Godot;

namespace Plataform01 {
    public partial class LevelManager : Node2D {
        
        // Propiedades exportables para configurar desde inspector
        [Export] public string StartingLevel = "res://scenes/levels/level_1.tscn";
        
        // Variables internas
        private int CurrentLevelIndex = 0;
        private bool IsLevelLoaded = false;
        
        protected override void _Ready() {
            LoadLevel(CurrentLevelIndex);
            
            // Conectar señales del jugador si existen
            var player = GetNodeOrMissing<Node>("Player");
            if (player != null) {
                player.SignalConnected("health_changed", typeof(HealthChangedSignal), OnPlayerHealthChanged);
                player.SignalConnected("level_completed", typeof(LevelCompletedSignal), OnLevelComplete);
                player.SignalConnected("player_died", typeof(PlayerDiedSignal), OnPlayerDied);
            }
        }
        
        public void LoadLevel(int levelIndex) {
            CurrentLevelIndex = levelIndex;
            
            // Cargar escena de nivel desde path exportado en inspector
            var levelPath = StartingLevel.Replace("{level}", CurrentLevelIndex.ToString());
            var scene = GD.Load<PackedScene>(levelPath);
            
            if (scene != null) {
                Instantiate(scene);
                IsLevelLoaded = true;
                
                // Reproducir música de nivel si existe
                var bgm = GetNodeOrMissing<AudioStreamPlayer>("BGMPlayer");
                if (bgm != null && BgmStream != null) {
                    bgm.Stream = BgmStream;
                    bgm.Play();
                }
            } else {
                PrintErr($"Error cargando nivel: {levelPath}");
            }
        }
        
        public int GetCurrentLevel() => CurrentLevelIndex + 1;
        
        private void OnPlayerHealthChanged(int newHealth, int maxHealth) {
            var hud = GetNodeOrMissing<Label>("HUD/HealthLabel");
            if (hud != null) {
                hud.Text = $"❤️ {newHealth}/{maxHealth}";
            }
            
            // Actualizar UI de barra de vida si existe
            var healthBar = GetNodeOrMissing<Node>("HUD/HealthBarContainer");
            if (healthBar != null) {
                var fillRect = healthBar.GetNodeOrMissing<Rect2D>("Fill");
                if (fillRect != null) {
                    float percentage = newHealth / (float)maxHealth;
                    fillRect.Position = new Vector2(0, 0);
                    fillRect.Size = new Vector2(fillRect.Rect().Size.X * percentage, fillRect.Rect().Size.Y);
                }
            }
        }
        
        private void OnLevelComplete() {
            // Cargar siguiente nivel
            LoadLevel(CurrentLevelIndex + 1);
        }
        
        private void OnPlayerDied() {
            // Reiniciar jugador o cargar nivel anterior con checkpoint
            var player = GetNodeOrMissing<Node>("Player");
            if (player != null) {
                player.ReparentTo(GetTree().Root);
                player.Position = GetNodeOrMissing<Node>("SpawnPoint").Position;
                player.CurrentHealth = player.MaxHealth;
                
                // Reproducir sonido de reinicio
                var restartSound = GetNodeOrMissing<AudioStreamPlayer>("RestartSound");
                if (restartSound != null) {
                    restartSound.Play();
                }
            }
        }
        
        public string GetCurrentLevelName() => $"Level {CurrentLevelIndex + 1}";
    }
}