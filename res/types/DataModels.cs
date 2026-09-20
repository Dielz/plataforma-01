using System;
using System.Collections.Generic;
using Godot;

namespace Plataforma01.Types {
    // DTO para guardar estado del jugador entre niveles
    public partial class PlayerData : GodotObject {
        [Export] public int Health = 3;
        [Export] public float InvincibilityTimer = 0f;
        [Export] public int Coins = 0;
        [Export] public int HeartsCollected = 0;
        
        public void ResetToDefaults() {
            Health = 3;
            InvincibilityTimer = 0f;
            Coins = 0;
            HeartsCollected = 0;
        }
    }
    
    // DTO para guardar datos de nivel
    public partial class LevelData : GodotObject {
        [Export] public string Name = "";
        [Export] public string Path = "";
        [Export] public int CheckpointIndex = 0;
        [Export] public bool Completed = false;
        
        public void LoadFromConfig(string configPath) {
            // Cargar datos desde archivo de configuración si existe
            var file = FileAccess.Open(configPath, FileAccess.ModeFlags.Read);
            if (file != null) {
                string content = file.GetAsText();
                file.Close();
                
                // Parsear contenido del archivo
                Name = ParseString(content, "name", "");
                Path = ParseString(content, "path", "");
                CheckpointIndex = ParseInt(content, "checkpoint", 0);
                Completed = ParseBool(content, "completed", false);
            }
        }
        
        public void SaveToFile(string configPath) {
            // Guardar datos a archivo de configuración
            var file = FileAccess.Open(configPath, FileAccess.ModeFlags.Write);
            if (file != null) {
                string content = $"name={Name}\n" +
                                $"path={Path}\n" +
                                $"checkpoint={CheckpointIndex}\n" +
                                $"completed={Completed.ToString().ToLower()}\n";
                file.StoreString(content);
                file.Close();
            }
        }
        
        private string ParseString(string content, string key, string defaultValue = "") {
            if (string.IsNullOrEmpty(content)) return defaultValue;
            
            var parts = content.Split(new[] { $"{key}=" }, StringSplitOptions.None);
            if (parts.Length > 1) {
                int braceStart = parts[1].IndexOf('(');
                int braceEnd = parts[1].LastIndexOf(')');
                if (braceStart >= 0 && braceEnd > braceStart) {
                    return parts[1].Substring(braceStart + 1, braceEnd - braceStart - 1);
                }
            }
            return defaultValue;
        }
        
        private int ParseInt(string content, string key, int defaultValue = 0) {
            var value = ParseString(content, key);
            return int.TryParse(value, out int result) ? result : defaultValue;
        }
        
        private bool ParseBool(string content, string key, bool defaultValue = false) {
            if (string.IsNullOrEmpty(content)) return defaultValue;
            
            var parts = content.Split(new[] { $"{key}=" }, StringSplitOptions.None);
            if (parts.Length > 1 && !string.IsNullOrEmpty(parts[1])) {
                string trimmed = parts[1].Trim();
                return trimmed.ToLower() == "true";
            }
            return defaultValue;
        }
    }
    
    // DTO para guardar estado de progreso del juego
    public partial class GameProgress : GodotObject {
        [Export] public int CurrentLevel = 0;
        [Export] public bool Level1Completed = false;
        [Export] public bool Level2Completed = false;
        [Export] public bool Level3Completed = false;
        [Export] public bool Level4Completed = false;
        [Export] public int TotalCoinsCollected = 0;
        
        public void SaveProgress() {
            // Guardar progreso en archivo local
            var path = "user://game_progress.json";
            var progress = new Godot.Collections.Dictionary {
                ["current_level"] = CurrentLevel,
                ["level_1_completed"] = Level1Completed,
                ["level_2_completed"] = Level2Completed,
                ["level_3_completed"] = Level3Completed,
                ["level_4_completed"] = Level4Completed,
                ["total_coins_collected"] = TotalCoinsCollected
            };
            
            string json = Json.Stringify(progress);
            var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
            if (file != null) {
                file.StoreString(json);
                file.Close();
            }
        }
    }
}