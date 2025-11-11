using UnityEngine;

public class Keeper : Enemy, IDismiss
{
    [SerializeField] private float rotantionAngle;
    [SerializeField] private float speedTurn = 1f;

    private float currentAngle;
    private float minAngle;
    private float maxAngle;
    private int dirRotation = 1;

    private bool canRotation = true;


    protected override void Start()
    {
        base.Start();

        AudioManager.instance.PlayAudioClip(audioSource, AudioClipDataNameStrings.EYE_KEEPER_AUDIO);

        currentAngle = transform.localEulerAngles.y;
        minAngle = currentAngle - rotantionAngle / 2;
        maxAngle = currentAngle + rotantionAngle / 2;
    }

    protected override void Update()
    {
        if (canRotation)
        {
            if (currentAngle >= maxAngle)
                dirRotation = -1;
            else if (currentAngle <= minAngle)
                dirRotation = 1;

            currentAngle += dirRotation * Time.deltaTime * speedTurn;
            currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
        }
    }

    protected override void DisableMovement()
    {
        canRotation = false;
    }

    protected override void EnableMovement()
    {
        canRotation = true;
    }
}
