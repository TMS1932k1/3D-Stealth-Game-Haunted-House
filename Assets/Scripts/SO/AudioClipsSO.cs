using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AudioClipsData
{
    public string nameData;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume;
    public bool isLoop;
}

[CreateAssetMenu(fileName = "AUDIO_CLIPS_DATA", menuName = "Create new AudioClips Data")]
public class AudioClipsSO : ScriptableObject
{
    [SerializeField] private List<AudioClipsData> player;
    [SerializeField] private List<AudioClipsData> ivan;
    [SerializeField] private List<AudioClipsData> enemy;
    [SerializeField] private List<AudioClipsData> ui;
    [SerializeField] private List<AudioClipsData> bgm;

    private Dictionary<string, AudioClipsData> audioClipsCollection = new();


    private void OnEnable()
    {
        AddAudioClipsData(player);
        AddAudioClipsData(ivan);
        AddAudioClipsData(enemy);
        AddAudioClipsData(ui);
        AddAudioClipsData(bgm);
    }

    public AudioClipsData GetAudioClipsData(string nameData)
    {
        if (audioClipsCollection.Count <= 0)
        {
            Debug.LogWarning($"[{name}]: AudioClips collection is empty");
            return null;
        }

        if (audioClipsCollection.ContainsKey(nameData) == false)
        {
            Debug.LogWarning($"[{name}]: Not found audioclip data with \"{nameData}\"");
            return null;
        }

        return audioClipsCollection[nameData];
    }

    private void AddAudioClipsData(List<AudioClipsData> clipsDatas)
    {
        if (clipsDatas == null || clipsDatas.Count <= 0)
            return;

        foreach (AudioClipsData data in clipsDatas)
        {
            if (data != null && audioClipsCollection.ContainsKey(data.nameData) == false)
            {
                audioClipsCollection.Add(data.nameData, data);
            }
        }
    }
}