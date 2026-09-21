using Godot;

namespace Plataform01
{
    public partial class Coin : CharacterBody2D
    {
        [Export] public float MoveSpeed = 0f;

        public override void _Ready()
        {
            AddToGroup("coin");
        }

        public override void _PhysicsProcess(double delta)
        {
            float d = (float)delta;
            
            for (int i = 0; i < GetSlideCollisionCount(); i++)
            {
                var collision = GetSlideCollision(i);
                if (collision.GetCollider() is Player player)
                {
                    GD.Print($"[COIN] Player detected! Coins: {player.Coins}");
                    player.OnCoinCollected();
                    Visible = false;
                    set_physics_process(false);
                    break;
                }
            }
        }
    }
}
