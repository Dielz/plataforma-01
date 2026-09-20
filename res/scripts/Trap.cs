using Godot;

namespace Plataform01 {
    public partial class Trap : Node2D {
        
        // Propiedades exportables
        [Export] public float Damage = 50f;
        [Export] public float ActivationRange = 200f;
        [Export] public float Cooldown = 3f; // Segundos entre activaciones
        
        // Variables internas
        private bool IsActive = true;
        private float LastActivationTime;
        
        protected override void _Ready() {
            var cooldownTimer = GetNodeOrMissing<Timer>("CooldownTimer");
            if (cooldownTimer != null) {
                cooldownTimer.Timeout += OnCooldownTimeout;
            }
            
            // Activar inicialmente si es necesario
            Activate();
        }
        
        public override void _Process(float delta) {
            // Desactivar después del cooldown
            if (!IsActive && LastActivationTime > 0) {
                var timeSinceActivation = Time.GetUnixTimestamp() - LastActivationTime;
                if (timeSinceActivation >= Cooldown) {
                    Activate();
                }
            }
        }
        
        private void OnCooldownTimeout() {
            // Reiniciar el timer de cooldown
            var node = GetNodeOrMissing<Node>("../..");
            if (node != null) {
                LastActivationTime = Time.GetUnixTimestamp();
                IsActive = true;
            }
        }
        
        public void Activate() {
            IsActive = true;
            LastActivationTime = 0;
            
            // Cambiar apariencia si tiene animaciones
            var visual = GetNodeOrMissing<Node>("Visual");
            if (visual != null) {
                visual.Visible = true;
            }
        }
        
        public override void _EnterTree() {
            // Detectar jugador y causar daño
            var area = GetNodeOrMissing<Area2D>("Hitbox");
            if (area == null) return;
            
            var player = GetNodeOrMissing<Node>("../..").GetNodeOrMissing<Node>("Player");
            if (player != null) {
                // Detectar cuando el jugador entra en rango de activación
                area.BodyEntered += OnBodyEntered;
            }
        }
        
        public void OnBodyEntered(Node2D body) {
            if (IsActive && Time.GetUnixTimestamp() - LastActivationTime >= Cooldown) {
                var player = GetNodeOrMissing<Node>("../..").GetNodeOrMissing<Node>("Player");
                if (player != null && new Vector2(player.Position).DistanceTo(new Vector2(body.GlobalPosition)) <= ActivationRange) {
                    var impactPos = player.GlobalPosition;
                    
                    // Causar daño al jugador
                    if (GetNodeOrMissing<Node>("../..").GetNodeOrMissing<Node>("Player") != null) {
                        GetNodeOrMissing<Node>("../..").Call("TakeDamage", Damage, impactPos);
                    }
                    
                    Deactivate();
                }
            }
        }
        
        public void Deactivate() {
            IsActive = false;
            
            // Ocultar visualmente
            var visual = GetNodeOrMissing<Node>("Visual");
            if (visual != null) {
                visual.Visible = false;
            }
        }
        
        public float GetDamage() => Damage;
        public bool IsTrapActive() => IsActive;
    }
}