using Godot;

namespace Plataform01
{
    public partial class LevelManager : Node
    {
        [Export] public string StartingLevel = "res://res/scenes/levels/level_1.tscn";

        public override void _Ready()
        {
            GD.Print("LevelManager listo");
        }

        public void RestartLevel()
        {
            GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
        }

        public void CompleteLevel()
        {
            GD.Print("Level complete!");
            RestartLevel();
        }
    }
}