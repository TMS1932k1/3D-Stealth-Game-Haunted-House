using UnityEngine;
using UnityEngine.EventSystems;

public class BtnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Animator anim;


    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger(BtnAnimationStrings.SELECTED_TRIGGER);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        anim.SetTrigger(BtnAnimationStrings.CANCLE_TRIGGER);
    }
}
