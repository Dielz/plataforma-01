using Godot;

namespace Plataform01
{
    public partial class Coin : Area2D
    {
        private bool _collected = false;

        public override void _Ready()
        {
            CollisionLayer = 1;
            CollisionMask = 1;
            Monitoring = true;
        }

        public override void _PhysicsProcess(double delta)
        {
            if (_collected) return;

            foreach (Node2D body in GetOverlappingBodies())
            {
                if (body is Player player)
                {
                    _collected = true;
                    player.OnCoinCollected();
                    QueueFree();
                    return;
                }
            }
        }
    }
}