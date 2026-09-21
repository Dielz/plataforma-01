using System;
using Godot;

namespace Plataform01
{
    public partial class Menu : Control
    {
        private const string LevelToStart = "res://res/scenes/levels/level_1.tscn";

        public override void _Ready()
        {
            GetTree().Paused = false;
            BuildUi();
        }

        public override void _Process(double delta)
        {
            if (Input.IsActionJustPressed("ui_cancel"))
            {
                GetTree().Quit();
            }
        }

        private void BuildUi()
        {
            var bg = new ColorRect();
            bg.Color = new Color(0.09f, 0.11f, 0.16f);
            bg.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            AddChild(bg);

            var center = new CenterContainer();
            center.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            AddChild(center);

            var box = new VBoxContainer();
            box.AddThemeConstantOverride("separation", 20);
            center.AddChild(box);

            var title = new Label();
            title.Text = "PLATAFORMA 01";
            title.AddThemeFontSizeOverride("font_size", 72);
            title.HorizontalAlignment = HorizontalAlignment.Center;
            box.AddChild(title);

            var subtitle = new Label();
            subtitle.Text = "Aventura de plataformas";
            subtitle.AddThemeFontSizeOverride("font_size", 28);
            subtitle.HorizontalAlignment = HorizontalAlignment.Center;
            box.AddChild(subtitle);

            var spacer = new Control();
            spacer.CustomMinimumSize = new Vector2(0, 20);
            box.AddChild(spacer);

            var playButton = MakeButton("Jugar", OnPlayPressed);
            box.AddChild(playButton);

            var quitButton = MakeButton("Salir", OnQuitPressed);
            box.AddChild(quitButton);

            playButton.GrabFocus();

            var version = new Label();
            version.Text = "v0.1";
            version.AddThemeFontSizeOverride("font_size", 18);
            version.HorizontalAlignment = HorizontalAlignment.Center;
            var versionBox = new VBoxContainer();
            versionBox.SetAnchorsPreset(Control.LayoutPreset.CenterBottom);
            versionBox.OffsetTop = -48;
            AddChild(versionBox);
            versionBox.AddChild(version);
        }

        private static Button MakeButton(string text, Action onPressed)
        {
            var button = new Button();
            button.Text = text;
            button.CustomMinimumSize = new Vector2(280, 64);
            button.AddThemeFontSizeOverride("font_size", 32);
            button.Pressed += onPressed;
            return button;
        }

        private void OnPlayPressed()
        {
            PlaySfx("sfx_select");
            GetNode<LevelManager>("/root/LevelManager").LoadLevel(LevelToStart);
        }

        private void OnQuitPressed()
        {
            PlaySfx("sfx_select");
            GetTree().Quit();
        }

        private void PlaySfx(string name)
        {
            GetNodeOrNull<AudioManager>("/root/AudioManager")?.PlaySfx(name);
        }
    }
}