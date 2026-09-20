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

        private int _health;

        public override void _Ready()
        {
            _health = MaxHealth;
        }

        public override void _PhysicsProcess(double delta)
        {
            float d = (float)delta;

            if (!IsOnFloor())
            {
                Velocity += new Vector2(0, Gravity * d);
            }

            float direction = Input.GetAxis("ui_left", "ui_right");
            Velocity = new Vector2(direction * MoveSpeed, Velocity.Y);

            if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            {
                Velocity = new Vector2(Velocity.X, -JumpVelocity);
            }

            MoveAndSlide();
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;
            GD.Print($"Player damaged! HP: {_health}/{MaxHealth}");

            if (_health <= 0)
            {
                QueueFree();
            }
        }
    }
}