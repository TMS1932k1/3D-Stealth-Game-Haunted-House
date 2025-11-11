using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI instance;

    private Player_InputSystem input;

    [SerializeField] private List<MainMenuBtn> buttons = new();
    private int currentButtonIndex = 0;


    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        if (input == null)
        {
            input = new Player_InputSystem();

            // UI input
            input.UI.Select.performed += i => HandleSelectInput(i.ReadValue<Vector2>());
            input.UI.Confirm.performed += i => OnConfirnInput();
            input.UI.Back.performed += i => OnBackInput();
        }

        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        // Set input
        input.Player.Disable();
        input.UI.Enable();

        // Set default main menu buttons
        SetIndexDefault();

        // Display
        DisplayMainMenuBtns();
    }

    private void SetIndexDefault()
    {
        currentButtonIndex = 0;
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].SetIndexBtn(i);
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

    private void HandleSelectInput(Vector2 selectInput)
    {
        if (selectInput.y == 1)
            currentButtonIndex = Mathf.Abs((currentButtonIndex + 1) % buttons.Count);
        else if (selectInput.y == -1)
            currentButtonIndex = Mathf.Abs((currentButtonIndex - 1) % buttons.Count);

        DisplayMainMenuBtns();
    }

    private void OnConfirnInput()
    {
        buttons[currentButtonIndex].OnConfirnBtn();
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

    public void OnBackInput()
    {
    }
}
