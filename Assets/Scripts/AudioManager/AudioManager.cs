using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClipsSO audioClipsSO;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource ui;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayBgmAudio(string nameData)
    {
        PlayAudioClip(bgm, nameData);
    }

    public void PlayUiAudio(string nameData)
    {
        PlayAudioClip(ui, nameData);
    }

    public void PlayAudioClip(AudioSource source, string nameData, float pitch = 1)
    {
        AudioClipsData audioClipData = audioClipsSO.GetAudioClipsData(nameData);

        if (source == null)
        {
            Debug.LogWarning($"[{name}]: AudioSource is null");
            return;
        }

        if (audioClipData == null)
            return;

        source.Stop();
        source.pitch = pitch;
        source.loop = audioClipData.isLoop;
        source.volume = audioClipData.volume;
        source.clip = audioClipData.audioClip;
        source.Play();
    }

    public void StopAudioClip(AudioSource source)
    {
        source.Stop();
    }
}
