using Godot;

namespace Plataform01
{
    public partial class LevelManager : Node2D
    {
        [Export] public string StartingLevel = "res://res/scenes/levels/level_1.tscn";

        public override void _Ready()
        {
            GD.Print("LevelManager listo");
        }

        public void LoadLevel(int levelIndex)
        {
            var packed = GD.Load<PackedScene>(StartingLevel);
            if (packed == null)
            {
                GD.PrintErr($"No se pudo cargar el nivel: {StartingLevel}");
                return;
            }

            AddChild(packed.Instantiate());
        }
    }
}