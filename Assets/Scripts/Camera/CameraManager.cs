using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    private CinemachineVirtualCamera cinemachine;

    [Header("Zoom Camera")]
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomFov;
    [SerializeField] private float normalFov;
    private float targetFov;


    private void Awake()
    {
        instance = this;

        cinemachine = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        targetFov = normalFov;
        cinemachine.m_Lens.FieldOfView = normalFov;
    }

    private void Update()
    {
        cinemachine.m_Lens.FieldOfView = Mathf.Lerp(cinemachine.m_Lens.FieldOfView, targetFov, zoomSpeed * Time.deltaTime);
    }

    public void StartActive()
    {
        targetFov = zoomFov;
    }

    public void EndActive()
    {
        targetFov = normalFov;
    }
}
