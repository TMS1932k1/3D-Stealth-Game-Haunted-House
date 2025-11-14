using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelItemUI : BtnUI
{
    public LevelSO levelData;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI nametext;
    [SerializeField] private TextMeshProUGUI passText;

    [Header("Color")]
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color lockColor = Color.gray;

    private Image borderImage;
    private Button button;

    public bool isSelected { get; private set; }
    private bool canActive;
    private int index;


    protected override void Awake()
    {
        base.Awake();

        borderImage = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        index = GameManager.instance.GetIndexOfLevelItem(levelData);
        SetDisplayLevelItem();
    }

    private void SetDisplayLevelItem()
    {
        canActive = index <= GameManager.instance.passedLevelIndex + 1;

        nametext.color = canActive ? activeColor : lockColor;
        borderImage.color = canActive ? activeColor : lockColor;
        passText.gameObject.SetActive(index <= GameManager.instance.passedLevelIndex);
        button.enabled = canActive;
    }

    public void SetSelectedBtn()
    {
        isSelected = true;

        anim.SetTrigger(BtnAnimationStrings.SELECTED_TRIGGER);
    }

    public void SetDefaultBtn()
    {
        isSelected = false;
        anim.SetTrigger(BtnAnimationStrings.CANCLE_TRIGGER);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!canActive)
            return;

        GetComponentInParent<LevelSelectUI>().SetCurrentIndex(index);
    }

    public override void OnPointerExit(PointerEventData eventData) { }

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
