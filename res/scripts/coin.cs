using Godot;

namespace Plataform01
{
    public partial class Coin : Area2D
    {
        public override void _Ready()
        {
            CollisionLayer = 1;
            CollisionMask = 1;
            BodyEntered += OnBodyEntered;
        }

        private void OnBodyEntered(Node2D body)
        {
            if (body is Player player)
            {
                player.OnCoinCollected();
                QueueFree();
            }
        }
    }
}
