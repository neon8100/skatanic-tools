using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Living The Deal/Sound/DynSFXEvent")]
public class DynSFXEvent : AudioEvent
{
    public AudioClip[] sFX;
    public AudioMixerGroup audioOutput;

    [MinMaxRange(0f, 2f)]
    public RangedFloat volume;

    [MinMaxRange(0f, 2f)]
    public RangedFloat pitch;

    public override void Play(AudioSource source)
    {
        if (sFX.Length == 0) return;

        source.clip = sFX[Random.Range(0, sFX.Length)];
        source.volume = Random.Range(volume.minValue, volume.maxValue);
        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        source.outputAudioMixerGroup = audioOutput;
        source.Play();
    }

    private void Play(float pitch)
    {
        GameObject audioObject = new GameObject();
        AudioSource source = audioObject.AddComponent<AudioSource>();

        if (sFX.Length == 0) return;

        source.clip = sFX[Random.Range(0, sFX.Length)];
        source.volume = Random.Range(volume.minValue, volume.maxValue);
        source.pitch = pitch;
        source.outputAudioMixerGroup = audioOutput;
        source.Play();

        audioObject.AddComponent<DestroyAudioOnComplete>();
    }
    
    public void Play()
    {
        Play(Random.Range(pitch.minValue, pitch.maxValue));
    }

    public void PlayAtPitch(float pitch)
    {
        Play(pitch);
    }
}
