using UnityEngine;

public class Ghost : Enemy
{
    private WaypointPatrol waypointPatrol;


    protected override void Awake()
    {
        base.Awake();
        waypointPatrol = GetComponentInChildren<WaypointPatrol>();
    }

    protected override void Start()
    {
        base.Start();

        AudioManager.instance.PlayAudioClip(audioSource, AudioClipDataNameStrings.GHOST_MOVE_AUDIO);
    }

    protected override void DisableMovement()
    {
        waypointPatrol.EnableMovement(false);
    }

    protected override void EnableMovement()
    {
        waypointPatrol.EnableMovement(true);
    }
}
