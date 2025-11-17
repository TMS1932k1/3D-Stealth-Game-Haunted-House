using TMPro;
using UnityEngine;

public class ControllerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI conditionText;
    [SerializeField] private Color defaultConditionsColor = Color.gray;
    [SerializeField] private Color completedDoneColor = Color.green;
    private bool isUpdated = false;


    private void Awake()
    {

    }

    private void Start()
    {
        SetFirstDisplayCondition();
    }

    private void Update()
    {
        if (GameManager.instance.CheckCompleted() && !isUpdated)
        {
            isUpdated = true;
            conditionText.color = completedDoneColor;
        }
    }

    private void SetFirstDisplayCondition()
    {
        if (GameManager.instance.currentLevelData == null)
        {
            Debug.LogWarning($"[{name}]: Conditions list is null!");
            return;
        }

        conditionText.text = GameManager.instance.currentLevelData.goalDescription;
        conditionText.color = defaultConditionsColor;
    }
}
