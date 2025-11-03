using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AudioSource audioSource;

    private WaypointPatrol waypointPatrol;


    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        waypointPatrol = GetComponentInChildren<WaypointPatrol>();
    }

    private void OnEnable()
    {
        GameManager.OnPauseGame += DisableMovement;
        GameManager.OnResumeGame += EnableMovement;
    }

    private void OnDisable()
    {
        GameManager.OnPauseGame -= DisableMovement;
        GameManager.OnResumeGame -= EnableMovement;
    }

    private void Start()
    {
        AudioManager.instance.PlayAudioClip(audioSource, AudioClipDataNameStrings.MOVE_AUDIO);
    }

    public void EnableMovement()
    {
        waypointPatrol.EnableMovement(true);
    }

    public void DisableMovement()
    {
        waypointPatrol.EnableMovement(false);
    }
}
