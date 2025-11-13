using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuBtn : BtnUI
{
    private TextMeshProUGUI text;
    private int index;
    public bool isSelected { get; private set; }

    [SerializeField] private Color defaultColor = Color.black;
    [SerializeField] private Color selectedColor = Color.yellow;


    protected override void Awake()
    {
        base.Awake();

        text = GetComponentInChildren<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
    }

    public void SetDefaultBtn()
    {
        isSelected = false;
        text.color = defaultColor;
        text.fontStyle = FontStyles.Normal;

        anim.SetTrigger(BtnAnimationStrings.CANCLE_TRIGGER);
    }

    public void SetSelectedBtn()
    {
        isSelected = true;
        text.color = selectedColor;
        text.fontStyle = FontStyles.Bold;

        anim.SetTrigger(BtnAnimationStrings.SELECTED_TRIGGER);
    }

    public void OnConfirnBtn()
    {
        GetComponent<Button>().onClick.Invoke();
    }

    public void SetIndexBtn(int index) => this.index = index;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<MainMenuUI>().SetCurrentIndex(index);
    }

    public override void OnPointerExit(PointerEventData eventData) { }
}
