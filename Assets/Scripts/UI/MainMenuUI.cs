using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<MenuBtn> buttons = new();
    private int currentButtonIndex = 0;


    private void Start()
    {
        // Set default main menu buttons
        SetIndexDefault();

        // Display
        DisplayMainMenuBtns();
    }

    private void SetIndexDefault()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].SetIndexBtn(i);
    }

    public void OnConfirnInput()
    {
        buttons[currentButtonIndex].OnConfirnBtn();
    }

    public void OnSelectInput(Vector2 selectInput)
    {
        if (selectInput.y == 1)
            currentButtonIndex = Mathf.Abs((currentButtonIndex + 1) % buttons.Count);
        else if (selectInput.y == -1)
            currentButtonIndex = Mathf.Abs((currentButtonIndex - 1) % buttons.Count);

        DisplayMainMenuBtns();
    }

    public void SetCurrentIndex(int index)
    {
        currentButtonIndex = index;
        DisplayMainMenuBtns();
    }

    public void OnStartBtn()
    {
        Debug.Log($"[{name}]: Onclick start button");
        AudioManager.instance.PlayUiAudio(AudioClipDataNameStrings.SELECT_AUDIO);
    }

    public void OnSettingBtn()
    {
        Debug.Log($"[{name}]: Onclick setting button");
        AudioManager.instance.PlayUiAudio(AudioClipDataNameStrings.SELECT_AUDIO);
    }

    private void DisplayMainMenuBtns()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == currentButtonIndex && buttons[i].isSelected == false)
                buttons[i].SetSelectedBtn();

            if (i != currentButtonIndex && buttons[i].isSelected == true)
                buttons[i].SetDefaultBtn();
        }
    }
}
