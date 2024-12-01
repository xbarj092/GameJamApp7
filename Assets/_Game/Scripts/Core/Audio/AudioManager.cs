using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private bool _spacialBlend;
    [SerializeField] private AudioMixerGroup _mixer;

    public Sound[] Sounds;
    public bool Muted = false;

    public void Awake()
    {
        foreach (Sound sound in Sounds)
        {
            for (int i = 0; i < sound.NumberOfSource; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.playOnAwake = false;
                source.loop = sound.name == SoundType.GameMusic || sound.name == SoundType.MenuMusic;
                source.clip = sound.clip;
                source.volume = sound.volume;
                source.pitch = sound.pitch;
                source.spatialBlend = sound.SpatialBlend;
                source.outputAudioMixerGroup = _mixer;
                sound.source.Add(source);
            }
        }
    }

    public void Play(SoundType name)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == name);
        bool find = false;

        AudioSource foundSource = sound.source.Find((source) => 
        {
            find = !source.isPlaying;
            return find;
        });

        if (foundSource != null)
        {
            foundSource.Play();
        }
        else
        {
            sound.source[0].Play();
        }
    }

    public void Stop(SoundType name)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == name);
        sound.source.ForEach((source) => source.Stop());
    }

    public bool IsPlaying(SoundType name)
    { 
        Sound sound = Array.Find(Sounds, sound => sound.name == name);
        return sound.source.Exists((source) => source.isPlaying);
    }

    public void SetVolume(bool mute)
    {
        Muted = mute;
        AudioListener.volume = mute ? 0 : 1;
    }

    public void StopAllSounds()
    {
        foreach (Sound sound in Sounds)
        {
            if (IsPlaying(sound.name))
            {
                Stop(sound.name);
            }
        }
    }

    public void Pause()
    {
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            if (!source.loop)
            {
                source.Pause();
            }
        }
    }

    public void Unpause()
    {
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            if (!source.loop)
            {
                source.UnPause();
            }
        }
    }
}
