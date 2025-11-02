using UnityEngine;

public class Observer : MonoBehaviour
{
    private Transform caughtTransform;
    private bool isPlayerInRange;
    private bool isCaughted;


    private void Update()
    {
        if (isPlayerInRange && caughtTransform != null && !isCaughted)
        {
            Debug.Log($"[{name}]: Player is in range");
            HandleRayCast();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IGetCaught canCaught = other.GetComponent<IGetCaught>();
        if (canCaught != null)
        {
            isPlayerInRange = true;
            caughtTransform = canCaught.GetTransform();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IGetCaught canCaught = other.GetComponent<IGetCaught>();
        if (canCaught != null)
        {
            isPlayerInRange = false;
            caughtTransform = null;
        }
    }

    private void HandleRayCast()
    {
        Vector3 direction = caughtTransform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit))
        {
            IGetCaught canCaught = raycastHit.collider.GetComponent<IGetCaught>();
            if (canCaught != null)
            {
                isCaughted = true;
                GameManager.instance.OnPlayerCaughted(canCaught);
            }
        }
    }
}
