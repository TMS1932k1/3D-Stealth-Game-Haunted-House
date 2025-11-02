using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        AudioManager.instance.PlayAudioClip(audioSource, AudioClipDataNameStrings.MOVE_AUDIO);
    }
}
