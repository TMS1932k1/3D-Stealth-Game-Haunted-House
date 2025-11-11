using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuBtn : MonoBehaviour, IPointerEnterHandler
{
    private TextMeshProUGUI text;
    private Animator anim;
    private int index;
    public bool isSelected { get; private set; }

    [SerializeField] private Color defaultColor = Color.black;
    [SerializeField] private Color selectedColor = Color.yellow;


    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
    }

    public void SetDefaultBtn()
    {
        isSelected = false;
        text.color = defaultColor;
        text.fontStyle = FontStyles.Normal;

        anim.SetTrigger(MainMenuBtnAnimationStrings.CANCLE_TRIGGER);
    }

    public void SetSelectedBtn()
    {
        isSelected = true;
        text.color = selectedColor;
        text.fontStyle = FontStyles.Bold;

        anim.SetTrigger(MainMenuBtnAnimationStrings.SELECTED_TRIGGER);
    }

    public void OnConfirnBtn()
    {
        GetComponent<Button>().onClick.Invoke();
    }

    public void SetIndexBtn(int index) => this.index = index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        MainMenuUI.instance.SetCurrentIndex(index);
    }
}
