using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeLevelUI : MonoBehaviour, IEndDragHandler
{
    private LevelSelectUI levelSelectUI;
    private float drapThreshould = Screen.width / 15;


    private void Awake()
    {
        levelSelectUI = GetComponentInParent<LevelSelectUI>();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > drapThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)
                levelSelectUI.PreviousPage();
            else
                levelSelectUI.NextPage();
        }
        else
        {
            levelSelectUI.MovePage();
        }
    }
}
