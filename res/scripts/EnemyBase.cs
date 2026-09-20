using Godot;

namespace Plataform01
{
    public partial class EnemyBase : CharacterBody2D
    {
        [Export] public float MoveSpeed = 60f;

        public override void _PhysicsProcess(double delta)
        {
            float d = (float)delta;

            if (GlobalPosition.Y > 1500f)
            {
                QueueFree();
                return;
            }

            if (!IsOnFloor())
            {
                Velocity += new Vector2(0, 1200f * d);
            }

            Velocity = new Vector2(MoveSpeed, Velocity.Y);
            MoveAndSlide();

            if (IsOnWall())
            {
                MoveSpeed *= -1f;
            }
        }

        public virtual void Die()
        {
            SetPhysicsProcess(false);
            CollisionLayer = 0;
            CollisionMask = 0;
            QueueFree();
        }
    }
}