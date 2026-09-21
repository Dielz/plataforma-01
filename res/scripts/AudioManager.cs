using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Plataform01
{
    public partial class AudioManager : Node
    {
        private const string SoundDir = "res://res/assets/Audio/Sounds/";

        private readonly Dictionary<string, AudioStream> _cache = new();
        private readonly List<AudioStreamPlayer> _players = new();
        private int _next = 0;

        private const int MaxPlayers = 12;

        public override void _Ready()
        {
            for (int i = 0; i < MaxPlayers; i++)
            {
                AddChild(new AudioStreamPlayer());
            }
        }

        public void PlaySfx(string name, float pitchScale = 1.0f)
        {
            PlayFile(SoundDir + name + ".ogg", pitchScale);
        }

        public void PlayFile(string path, float pitchScale = 1.0f)
        {
            if (!_cache.TryGetValue(path, out AudioStream stream))
            {
                stream = GD.Load<AudioStream>(path);
                _cache[path] = stream;
            }

            if (stream == null) return;

            AudioStreamPlayer player = _players.FirstOrDefault(p => !p.Playing);
            if (player == null)
            {
                player = _players[_next];
                _next = (_next + 1) % _players.Count;
            }

            player.Stream = stream;
            player.PitchScale = pitchScale;
            player.Play();
        }
    }
}