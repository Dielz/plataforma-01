using Godot;

namespace Plataform01
{
    public partial class GameOver : Control
    {
        public override void _Ready()
        {
            ProcessMode = ProcessModeEnum.Always;
            GetTree().Paused = true;
            BuildUi();
        }

        private void BuildUi()
        {
            var bg = new ColorRect();
            bg.Color = new Color(0.16f, 0.05f, 0.05f, 0.9f);
            bg.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            AddChild(bg);

            var center = new CenterContainer();
            center.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            AddChild(center);

            var box = new VBoxContainer();
            box.AddThemeConstantOverride("separation", 20);
            center.AddChild(box);

            var title = new Label();
            title.Text = "GAME OVER";
            title.AddThemeFontSizeOverride("font_size", 80);
            title.HorizontalAlignment = HorizontalAlignment.Center;
            box.AddChild(title);

            var coinsLabel = new Label();
            coinsLabel.AddThemeFontSizeOverride("font_size", 28);
            coinsLabel.HorizontalAlignment = HorizontalAlignment.Center;
            box.AddChild(coinsLabel);

            var player = GetTree().GetFirstNodeInGroup("player") as Player;
            coinsLabel.Text = player != null ? $"Monedas: {player.Coins}" : "Monedas: 0";

            var spacer = new Control();
            spacer.CustomMinimumSize = new Vector2(0, 20);
            box.AddChild(spacer);

            var retryButton = MakeButton("Reintentar", OnRetryPressed);
            box.AddChild(retryButton);

            var menuButton = MakeButton("Menú principal", OnMenuPressed);
            box.AddChild(menuButton);

            retryButton.GrabFocus();
        }

        private static Button MakeButton(string text, System.Action onPressed)
        {
            var button = new Button();
            button.Text = text;
            button.CustomMinimumSize = new Vector2(280, 64);
            button.AddThemeFontSizeOverride("font_size", 30);
            button.Pressed += onPressed;
            return button;
        }

        private void OnRetryPressed()
        {
            PlaySfx("sfx_select");
            GetNode<LevelManager>("/root/LevelManager").RestartLevel();
        }

        private void OnMenuPressed()
        {
            PlaySfx("sfx_select");
            GetNode<LevelManager>("/root/LevelManager").GoToMainMenu();
        }

        private void PlaySfx(string name)
        {
            GetNodeOrNull<AudioManager>("/root/AudioManager")?.PlaySfx(name);
        }
    }
}