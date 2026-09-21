using Godot;

namespace Plataform01
{
    public partial class Player : CharacterBody2D
    {
        [ExportGroup("Movement")]
        [Export] public float MoveSpeed = 300f;
        [Export] public float JumpVelocity = 500f;
        [Export] public float Gravity = 1200f;

        [ExportGroup("Health")]
        [Export] public int MaxHealth = 3;
        [Export] public float InvincibilityTime = 0.8f;

        [ExportGroup("Combat")]
        [Export] public float StompBounceVelocity = 320f;

        [ExportGroup("Level")]
        [Export] public float KillPlaneY = 1000f;

        private int _health;
        private int _coins = 0;

        public int Coins => _coins;
        private float _invincibleTimer = 0f;
        private bool _dead = false;

        public int Health => _health;

        public override void _Ready()
        {
            _health = MaxHealth;
            AddToGroup("player");
            CollisionLayer = 1;
            CollisionMask = 1;
        }

        public override void _PhysicsProcess(double delta)
        {
            if (_dead) return;

            float d = (float)delta;

            if (_invincibleTimer > 0f)
            {
                _invincibleTimer -= d;
            }

            if (!IsOnFloor())
            {
                Velocity += new Vector2(0, Gravity * d);
            }

            float direction = Input.GetAxis("ui_left", "ui_right");
            Velocity = new Vector2(direction * MoveSpeed, Velocity.Y);

            if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            {
                Velocity = new Vector2(Velocity.X, -JumpVelocity);
                PlaySfx("sfx_jump");
            }

            MoveAndSlide();
            HandleEnemyCollisions();

            if (GlobalPosition.Y > KillPlaneY)
            {
                Die();
            }

            UpdateInvincibilityVisual();
        }

        private void HandleEnemyCollisions()
        {
            for (int i = 0; i < GetSlideCollisionCount(); i++)
            {
                var collision = GetSlideCollision(i);
                if (collision.GetCollider() is EnemyBase enemy)
                {
                    if (IsStomping(collision, enemy))
                    {
                        enemy.Die();
                        Velocity = new Vector2(Velocity.X, -StompBounceVelocity);
                        PlaySfx("sfx_gem");
                    }
                    else
                    {
                        TakeDamage(1);
                    }
                }
            }
        }

        private bool IsStomping(KinematicCollision2D collision, EnemyBase enemy)
        {
            bool movingDown = Velocity.Y > 0f;
            bool hitFromAbove = collision.GetNormal().Y < -0.4f;
            bool aboveEnemy = GlobalPosition.Y < enemy.GlobalPosition.Y;
            return aboveEnemy && (movingDown || hitFromAbove);
        }

        public void TakeDamage(int amount)
        {
            if (_dead || _invincibleTimer > 0f) return;

            _health -= amount;
            _invincibleTimer = InvincibilityTime;
            GD.Print($"Player damaged! HP: {_health}/{MaxHealth}");
            PlaySfx("sfx_hurt");

            if (_health <= 0)
            {
                Die();
            }
        }

        public void OnCoinCollected()
        {
            _coins++;
            GD.Print($"Coin collected! Total: {_coins}");
        }

        private void PlaySfx(string name, float pitchScale = 1.0f)
        {
            GetNodeOrNull<AudioManager>("/root/AudioManager")?.PlaySfx(name, pitchScale);
        }

        private void Die()
        {
            if (_dead) return;
            _dead = true;
            SetPhysicsProcess(false);
            CollisionLayer = 0;
            CollisionMask = 0;
            Visible = false;
            PlaySfx("sfx_disappear");

            bool gameOver = _health <= 0;

            GetTree().CreateTimer(gameOver ? 1.4f : 1.0f).Timeout += () =>
            {
                if (gameOver)
                {
                    GetNode<LevelManager>("/root/LevelManager").ShowGameOver();
                }
                else
                {
                    GetNode<LevelManager>("/root/LevelManager").RestartLevel();
                }
            };
        }

        private void UpdateInvincibilityVisual()
        {
            if (_invincibleTimer > 0f)
            {
                bool flashOn = ((int)(_invincibleTimer * 12f) % 2) == 0;
                Modulate = new Color(1f, 1f, 1f, flashOn ? 0.35f : 1f);
            }
            else
            {
                Modulate = Colors.White;
            }
        }
    }
}