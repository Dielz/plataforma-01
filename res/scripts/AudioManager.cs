using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Plataform01
{
    public partial class AudioManager : Node
    {
        private const string SoundDir = "res://res/assets/Audio/Sounds/";
        private const int MaxPlayers = 12;

        private readonly Dictionary<string, AudioStream> _cache = new();
        private readonly List<AudioStreamPlayer> _players = new();
        private int _next = 0;
        private bool _poolReady = false;

        public override void _Ready()
        {
            EnsurePool();
        }

        public void PlaySfx(string name, float pitchScale = 1.0f)
        {
            PlayFile(SoundDir + name + ".ogg", pitchScale);
        }

        public void PlayFile(string path, float pitchScale = 1.0f)
        {
            EnsurePool();

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

        private void EnsurePool()
        {
            if (_poolReady) return;
            _poolReady = true;

            for (int i = 0; i < MaxPlayers; i++)
            {
                _players.Add(new AudioStreamPlayer());
                AddChild(_players[i]);
            }
        }
    }
}