using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected AudioSource audioSource;

    private Enemy_VFX enemyVFX;
    private CapsuleCollider cd;
    private NavMeshAgent navMeshAgent;


    protected virtual void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();

        enemyVFX = GetComponent<Enemy_VFX>();
        cd = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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

    protected virtual void Start() { }

    protected virtual void Update() { }

    public void DismissBySaltJar()
    {
        Debug.Log($"[{name}]: Dismiss this enemy by Salt Jar");

        HandleDismiss();
    }

    private void HandleDismiss()
    {
        // Dismiss movement
        DisableMovement();

        // Disable collider
        cd.enabled = false;
        navMeshAgent.enabled = false;

        // Show VFX and SFX Dismiss
        enemyVFX.PlayDismissVFX();
        AudioManager.instance.PlayAudioClip(audioSource, AudioClipDataNameStrings.ENEMY_DEATH_AUDIO);

        // Hide enemy after 2s
        Invoke(nameof(HideEnemy), 2f);
    }

    private void HideEnemy()
    {
        gameObject.SetActive(false);
    }

    protected abstract void EnableMovement();

    protected abstract void DisableMovement();
}
