using UnityEngine;

public class Observer : MonoBehaviour
{
    private Transform playerTransform;
    private bool isPlayerInRange;
    private bool isCaughted;


    private void Update()
    {
        if (isPlayerInRange && playerTransform != null && !isCaughted)
        {
            Debug.Log($"[{name}]: Player is in range");
            HandleRayCast();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            isPlayerInRange = true;
            playerTransform = player.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            isPlayerInRange = false;
            playerTransform = null;
        }
    }

    private void HandleRayCast()
    {
        Vector3 direction = playerTransform.position - transform.position + Vector3.up; // Vector3.up to ray hit between player

        if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit))
        {
            Player player = raycastHit.collider.GetComponent<Player>();
            if (player != null)
            {
                isCaughted = true;
                GameManager.instance.OnPlayerCaughted(player);
            }
        }
    }
}
