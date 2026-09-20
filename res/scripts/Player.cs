using Godot;

namespace Plataform01 {
    public partial class Player : CharacterBody2D {
        
        // Propiedades exportables para la inspección en Godot
        [Export] public float JumpVelocity = 500f;
        [Export] public int MaxHealth = 3;
        [ExportGroup("Movement")]
        [Export] public float MoveSpeed = 200f;
        [Export] public float Acceleration = 1500f;
        
        // Variables internas
        private Vector2 Velocity;
        private bool IsGrounded;
        private int CurrentHealth;
        private float InvincibilityTimer;
        
        // Señales para UI y eventos
        [Signal] public delegate void HealthChangedSignal(int newHealth, int maxHealth);
        [Signal] public delegate void LevelCompletedSignal();
        [Signal] public delegate void PlayerDiedSignal();
        
        private Vector2 MoveDirection;
        private float GravityScale = 1.5f;
        private float GroundedYOffset = 0.5f; // Para colisión con suelo
        
        public override void _Ready() {
            CurrentHealth = MaxHealth;
            
            // Conectar señales de UI cuando existan
            GDExtension.RegisterSignal(this, "health_changed", typeof(HealthChangedSignal));
            GDExtension.RegisterSignal(this, "level_completed", typeof(LevelCompletedSignal));
            GDExtension.RegisterSignal(this, "player_died", typeof(PlayerDiedSignal));
        }
        
        public override void _PhysicsProcess(float delta) {
            // Aplicar gravedad
            Velocity.Y += GetPhysicsDirectSceneState().GetGravityVector().Length() * GravityScale * delta;
            
            // Input de movimiento (soporta teclado y touch móvil)
            if (Input.IsActionJustPressed("ui_cancel")) {
                EmitSignal(SignalName.PlayerDied);
                QueueFree();
                return;
            }
            
            MoveDirection = GetMoveVector();
            
            // Mover jugador
            if (!IsMoving()) {
                Velocity = Vector2.Zero;
            } else {
                float direction = Sign(MoveDirection.X);
                Velocity.X = move_toward(Velocity.X, MoveSpeed * direction, Acceleration * delta);
                
                if (Velocity.Abs() > MoveSpeed) {
                    Velocity = new Vector2(MoveSpeed.Sign() * MoveSpeed, Velocity.Y);
                }
            }
            
            // Salto
            if (IsMoving()) {
                Velocity.X = move_toward(Velocity.X, MoveDirection.X * JumpVelocity, 
                    Abs(MoveDirection.X) * Acceleration * delta * 2);
                
                if (!IsGrounded) {
                    IsMoving(false);
                }
            }
            
            // Aplicar velocidad
            Velocity = MoveAndCollide(Velocity);
            
            // Pantallazo por impacto
            float ImpactSpeed = new Vector2(Abs(Velocity.X), Abs(Velocity.Y)).Length();
            if (ImpactSpeed > 400) {
                GetTree().CallGroup("particles", "spawn_impact");
                Velocity *= 0.75f;
            }
            
            // Invencibilidad temporal
            if (InvincibilityTimer > 0) {
                InvincibilityTimer -= delta;
                
                // Flashing si está invulnerable
                float FlashTimer = GetNode<Timer>("FlashTimer").TimeLeft;
                float FlashSpeed = 0.1f;
                Color flashColor = InvincibilityTimer < FlashTimer % FlashSpeed ? 
                    new Color(1, 1, 1, 0) : new Color(1, 1, 1, 0.3f);
                
                Modulate = Modulate.Lerp(Color.WHITE, flashColor, delta / FlashSpeed);
            }
        }
        
        private void _OnHealthChanged(int newHealth, int maxHealth) {
            CurrentHealth = newHealth;
            EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
        }
        
        public override void _Process(float delta) {
            // Invencibilidad expira gradualmente
            if (InvincibilityTimer > 0 && InvincibilityTimer < 1f) {
                InvincibilityTimer -= delta;
            }
        }
        
        private float Sign(float a) => a >= 0 ? 1 : -1;
        private int Abs(int a) => a >= 0 ? a : -a;
        private float Abs(float a) => a >= 0 ? a : -a;
        
        private bool IsMoving() {
            return new Vector2(Input.GetAxis("ui_left"), Input.GetAxis("ui_right")).Length() > 0 ||
                   Input.IsActionPressed("jump");
        }
        
        public float GetMoveVector() {
            var axis = Input.GetAxis("move");
            
            // Soporte para input táctil simple en mobile
            if (GetViewport().IsControl()) {
                return axis;
            }
            
            return axis;
        }
        
        public void TakeDamage(float damage, Vector2 impactPosition) {
            if (InvincibilityTimer > 0) return;
            
            CurrentHealth -= Convert.ToInt32(damage);
            EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
            
            // Efecto de pantalla roja y parpadeo
            Modulate = new Color(1, 0.2f, 0.2f, 0.5f);
            
            if (CurrentHealth <= 0) {
                EmitSignal(SignalName.PlayerDied);
                QueueFree();
            } else {
                // Flash blanco rápido
                GetNode<Timer>("FlashTimer").StartTime = Time.GetUnixTimestamp() + 0.5f;
                
                // Pantallazo en el punto de impacto
                GetTree().CallGroup("particles", "spawn_impact", impactPosition);
            }
        }
        
        public void ApplyInvincibility(float duration) {
            InvincibilityTimer = duration;
            Modulate = new Color(1, 1, 1, 0.5f);
        }
        
        public void SpawnImpacts() {
            GetTree().CallGroup("particles", "spawn_impact");
        }
    }
}