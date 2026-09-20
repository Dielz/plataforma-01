using Godot;

namespace Plataform01
{
    public partial class Trap : Area2D
    {
        [Export] public int Damage = 1;

        public override void _Ready()
        {
            BodyEntered += OnBodyEntered;
        }

        private void OnBodyEntered(Node2D body)
        {
            if (body is Player player)
            {
                player.TakeDamage(Damage);
            }
        }
    }
}