using UnityEngine;


public class Player_Hold : MonoBehaviour
{
    [SerializeField] private Transform holdTransform;
    [SerializeField] private DialogueSO holdingDialogue;

    private ObjectPool_SaltJar saltJarPool;
    private PickUp currentpickUp;
    public EPickUpType holdingType { get; private set; } = EPickUpType.None;


    private void Awake()
    {
        saltJarPool = GetComponentInChildren<ObjectPool_SaltJar>();
    }

    public void HoldPickUp(EPickUpType type, DialogueSO dialogue)
    {
        // Code UI to show dialogue
        IngameUIManager.instance.ShowDialogueUI(dialogue);

        switch (type)
        {
            case EPickUpType.SaltJar:
                Debug.Log($"[{name}]: Player hold salt jar");
                HandleHoldSaltJar();
                break;

            default:
                Debug.LogWarning($"[{name}]: This item can't be picked up!");
                return;
        }
    }

    public void HoldingPickUp()
    {
        IngameUIManager.instance.ShowDialogueUI(holdingDialogue);
    }

    private void HandleHoldSaltJar()
    {
        holdingType = EPickUpType.SaltJar;

        currentpickUp = saltJarPool.GetObjectPool();
        currentpickUp.gameObject.transform.position = holdTransform.position;
    }

    public void DismissPickUp()
    {
        if (currentpickUp == null)
            return;

        switch (holdingType)
        {
            case EPickUpType.SaltJar:
                Debug.Log($"[{name}]: Salt Jat is dismissed");
                saltJarPool.ReturnPbjectPool(currentpickUp);
                break;

            default:
                Debug.LogWarning($"[{name}]: Player hold not pick up item");
                return;
        }

        holdingType = EPickUpType.None;
        currentpickUp = null;
    }
}
