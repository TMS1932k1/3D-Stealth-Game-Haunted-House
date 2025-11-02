using Unity.Mathematics;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private void OnEnable()
    {
        Vector3 lookDir = transform.position - Camera.main.transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}
