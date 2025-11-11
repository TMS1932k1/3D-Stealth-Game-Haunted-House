using UnityEngine;

public class Enemy_VFX : MonoBehaviour
{
    [SerializeField] private GameObject model;

    private ParticleSystem smokeCartoon;


    private void Awake()
    {
        smokeCartoon = GetComponentInChildren<ParticleSystem>();
    }

    public void PlayDismissVFX()
    {
        model.SetActive(false);
        smokeCartoon.Play();
    }
}
