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
            GD.Print($"[COIN] Body entered: {body.GetType().Name}");
            GD.Print($"[COIN] Is Player: {body is Player}");
            
            if (body is Player player)
            {
                GD.Print($"[COIN] Player detected! Coins: {player.Coins}");
                player.OnCoinCollected();
                QueueFree();
            }
        }
    }
}
