using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.CheckLevelCondition())
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                GameManager.instance.OnPlayerExit(player);
            }
        }
    }
}
