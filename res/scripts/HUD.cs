using Godot;

namespace Plataform01
{
    public partial class HUD : CanvasLayer
    {
        private const string HeartPath = "res://res/assets/Sprites/Tiles/Default/hud_heart.png";
        private const string HeartEmptyPath = "res://res/assets/Sprites/Tiles/Default/hud_heart_empty.png";

        private HBoxContainer _hearts;
        private Label _timerLabel;
        private Label _coinLabel;
        private Player _player;
        private float _elapsed = 0f;

        public override void _Ready()
        {
            var control = new Control();
            control.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            AddChild(control);

            var topLeft = new MarginContainer();
            topLeft.SetAnchorsPreset(Control.LayoutPreset.TopLeft);
            topLeft.OffsetLeft = 20;
            topLeft.OffsetTop = 16;
            control.AddChild(topLeft);

            _hearts = new HBoxContainer();
            _hearts.AddThemeConstantOverride("separation", 6);
            topLeft.AddChild(_hearts);

            _timerLabel = new Label();
            _timerLabel.SetAnchorsPreset(Control.LayoutPreset.TopRight);
            _timerLabel.OffsetRight = -24;
            _timerLabel.OffsetTop = 20;
            _timerLabel.AddThemeFontSizeOverride("font_size", 32);
            control.AddChild(_timerLabel);

            _coinLabel = new Label();
            _coinLabel.SetAnchorsPreset(Control.LayoutPreset.TopLeft);
            _coinLabel.OffsetLeft = 100;
            _coinLabel.OffsetTop = 70;
            _coinLabel.AddThemeFontSizeOverride("font_size", 28);
            control.AddChild(_coinLabel);

            // Buscar jugador inmediatamente
            _player = GetTree().GetFirstNodeInGroup("player") as Player;
            if (_player != null)
            {
                BuildHearts(_player.MaxHealth);
                UpdateCoins();
            }
        }

        public override void _Process(double delta)
        {
            _elapsed += (float)delta;
            UpdateTimer();
            UpdateHearts();
            UpdateCoins();
        }

        private void UpdateTimer()
        {
            int total = (int)_elapsed;
            int minutes = total / 60;
            int seconds = total % 60;
            _timerLabel.Text = $"{minutes:00}:{seconds:00}";
        }

        private void UpdateHearts()
        {
            if (_player == null || !IsInstanceValid(_player))
            {
                _player = GetTree().GetFirstNodeInGroup("player") as Player;
                if (_player == null)
                {
                    return;
                }
                BuildHearts(_player.MaxHealth);
            }

            int filled = _player.Health;
            for (int i = 0; i < _hearts.GetChildCount(); i++)
            {
                if (_hearts.GetChild(i) is TextureRect tex)
                {
                    tex.Texture = LoadHeart(i < filled);
                }
            }
        }

        private void UpdateCoins()
        {
            if (_player == null || !IsInstanceValid(_player))
            {
                _player = GetTree().GetFirstNodeInGroup("player") as Player;
                if (_player == null) return;
            }

            _coinLabel.Text = $"Coins: {_player.Coins}";
        }

        private void BuildHearts(int count)
        {
            foreach (Node child in _hearts.GetChildren())
            {
                child.QueueFree();
            }
            for (int i = 0; i < count; i++)
            {
                var tex = new TextureRect();
                tex.Texture = LoadHeart(i < _player.Health);
                tex.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
                tex.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;
                tex.CustomMinimumSize = new Vector2(40, 40);
                _hearts.AddChild(tex);
            }
        }

        private static Texture2D LoadHeart(bool filled)
        {
            return GD.Load<Texture2D>(filled ? HeartPath : HeartEmptyPath);
        }
    }
}