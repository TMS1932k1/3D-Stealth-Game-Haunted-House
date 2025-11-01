using UnityEngine;

public class GameEnding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            GameManager.instance.OnPlayerExit(player);
        }
    }
}
