using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItemUI : BtnUI
{
    [SerializeField] private LevelSO levelData;

    [Space]
    [SerializeField] private TextMeshProUGUI nametext;
    [SerializeField] private Image passImage;
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color lockColor = Color.gray;

    private Image borderImage;

    private int index = -1;


    protected override void Awake()
    {
        base.Awake();

        borderImage = GetComponent<Image>();
    }

    private void Start()
    {
        index = GameManager.instance.GetIndexOfLevelItem(levelData);
        SetDisplayLevelItem();
    }

    private void SetDisplayLevelItem()
    {
        nametext.color = index <= GameManager.instance.passedLevelIndex + 1 ? activeColor : lockColor;
        borderImage.color = index <= GameManager.instance.passedLevelIndex + 1 ? activeColor : lockColor;
        passImage.gameObject.SetActive(index <= GameManager.instance.passedLevelIndex);
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (levelData != null)
        {
            name = $"Level_{levelData.nameLevel}";
            nametext.text = levelData.nameLevel;
        }
#endif
    }
}
