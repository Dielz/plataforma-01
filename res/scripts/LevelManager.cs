using Godot;

namespace Plataform01
{
    public partial class LevelManager : Node
    {
        [Export] public string StartingLevel = "res://res/scenes/levels/level_1.tscn";

        private const string MainMenuPath = "res://res/scenes/ui/menu.tscn";
        private const string GameOverPath = "res://res/scenes/ui/game_over.tscn";

        public override void _Ready()
        {
            GD.Print("LevelManager listo");
            GetTree().Paused = false;
        }

        public void LoadLevel(string path)
        {
            GetTree().Paused = false;
            GetTree().ChangeSceneToFile(path);
        }

        public void RestartLevel()
        {
            GetTree().Paused = false;
            GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
        }

        public void CompleteLevel()
        {
            GD.Print("Level complete!");
            GoToMainMenu();
        }

        public void GoToMainMenu()
        {
            LoadLevel(MainMenuPath);
        }

        public void ShowGameOver()
        {
            var scene = GD.Load<PackedScene>(GameOverPath);
            if (scene != null)
            {
                GetTree().Paused = true;
                GetTree().CurrentScene?.AddChild(scene.Instantiate());
            }
        }
    }
}