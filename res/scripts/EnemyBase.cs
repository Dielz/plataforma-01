using Godot;

namespace Plataform01 {
    public abstract partial class EnemyBase : CharacterBody2D, ICharacterCollision {
        
        // Propiedades exportables
        [Export] public float MoveSpeed = 100f;
        [Export] public float DetectionRange = 300f;
        [ExportGroup("Appearance")]
        [Export] public float Health = 1f;
        [Export] public Color CollisionColor = new Color(1, 0.3f, 0.3f);
        
        // Variables internas
        private bool IsDead = false;
        private Vector2 MoveDirection;
        private int HitTimer;
        
        protected override void _Ready() {
            HitTimer = -1;
            
            // Setear color de colisión
            CollisionShape2D.SetColor(CollisionColor);
        }
        
        public override void _PhysicsProcess(float delta) {
            if (IsDead) {
                FallToFloor();
                return;
            }
            
            // Patrón básico: seguir al jugador si está en rango
            MoveDirection = GetMoveDirection();
            
            // Mover enemigo
            Velocity = new Vector2(MoveDirection.X * MoveSpeed, Velocity.Y);
            MoveAndCollide(Velocity);
            
            // Animación de muerte (rotación)
            if (HitTimer > 0) {
                Rotation += delta * -5f;
                HitTimer -= delta;
            }
        }
        
        public virtual Vector2 GetMoveDirection() {
            var player = GetNode<Node>("../..").GetNodeOrMissing<Node>("Player");
            if (player == null) return new Vector2(1, 0);
            
            var relativePos = player.Position - Position;
            MoveDirection = RelativeTransform.Basis.X * Sign(relativePos.X);
            
            // Invertir dirección si hay jugador en el lado opuesto
            if (GetNode<CollisionShape2D>("EnemyHitbox").HasCorners() && 
                GetNodeOrMissing<Node>("../..").GetNodeOrMissing<Node>("Player") != null) {
                
                var playerPos = GetNodeOrMissing<Node>("../..").GetNodeOrMissing<Node>("Player").Position;
                if (playerPos.X < Position.X) {
                    MoveDirection.X *= -1;
                }
            }
            
            return new Vector2(MoveDirection.X, 0);
        }
        
        public override void _Process(float delta) {
            // Rotación suave hacia el jugador cuando está en movimiento
            if (IsDead) return;
            
            var player = GetNodeOrMissing<Node>("../..").GetNodeOrMissing<Node>("Player");
            if (player != null && MoveDirection.Abs() > 0.1f) {
                float targetRotation = new Vector2(MoveDirection, 0).Angle();
                Rotation = RotateTowards(Rotation, targetRotation, delta * 90);
            }
        }
        
        public void TakeDamage(float damage, Vector2 impactPosition) {
            if (HitTimer > 0) return; // Invencible tras ser golpeado
            
            Health -= damage;
            
            // Flash blanco rápido
            Modulate = new Color(1, 1, 1, 0.8f);
            
            // Efecto de sonido y partículas (si existen)
            GetTree().CallGroup("particles", "spawn_impact", impactPosition);
            
            if (Health <= 0) {
                Die();
            } else {
                HitTimer = 1; // Invencibilidad de 1 segundo
                Modulate = new Color(1, 1, 1, 1);
            }
        }
        
        public void FallToFloor() {
            GetGravityVector().Normalized() * MoveSpeed;
            Position.Y += GetPhysicsDirectSceneState().GetWorldTransform().Basis.GetColumn(1).Y;
        }
        
        public float Sign(float a) => a >= 0 ? 1 : -1;
        
        // Método de colisión para detectar jugador y eliminar enemigo
        public bool OnCharacterEnter(Node character, NodeArea area) {
            if (IsDead || !GetNodeOrMissing<Node>("Player").InGroup("player_group", true)) return false;
            
            var player = GetNodeOrMissing<Node>("../..").GetNodeOrMissing<Node>("Player");
            if (player == null) return false;
            
            // Eliminar enemigo si el jugador está encima
            if (player.Position.Y < Position.Y && 
                CharacterCollision.CheckOverlap(character, area)) {
                Die();
                return true;
            }
            
            return false;
        }
        
        public void Die() {
            IsDead = true;
            // Efecto de muerte (partículas, sonido)
            GetTree().CallGroup("particles", "spawn_death");
        }
    }
}