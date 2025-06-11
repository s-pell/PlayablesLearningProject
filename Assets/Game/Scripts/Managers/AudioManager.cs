using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop = false;

        [HideInInspector] public AudioSource source;
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Mixer (optional)")] public AudioMixerGroup mixerGroup;

        [Header("Sounds")] public Sound[] sounds;

        private void Awake()
        {
            // Реализация Singleton
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // Инициализация AudioSource для каждого звука
            foreach (var s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                if (mixerGroup != null)
                    s.source.outputAudioMixerGroup = mixerGroup;
            }
        }

        // Воспроизведение звука по имени
        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning($"Sound {name} not found!");
                return;
            }

            s.source.Play();
        }

        // Остановка звука по имени
        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning($"Sound {name} not found!");
                return;
            }

            s.source.Stop();
        }

        // Изменение громкости звука по имени
        public void SetVolume(string name, float volume)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning($"Sound {name} not found!");
                return;
            }

            s.source.volume = Mathf.Clamp01(volume);
        }
    }
}