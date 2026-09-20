using Godot;

namespace Plataform01
{
    public partial class Goal : Area2D
    {
        private bool _reached = false;

        public override void _Ready()
        {
            BodyEntered += OnBodyEntered;
        }

        private void OnBodyEntered(Node2D body)
        {
            if (_reached || body is not Player) return;
            _reached = true;
            GetNode<LevelManager>("/root/LevelManager").CompleteLevel();
        }
    }
}