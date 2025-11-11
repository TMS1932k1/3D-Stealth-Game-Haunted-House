using System.Collections.Generic;
using UnityEngine;


public class MainMenuUIManager : MonoBehaviour
{
    public static MainMenuUIManager instance;

    private MainMenuUI mainMenuUI;

    private Player_InputSystem input;
    private EMainMenuUI currentUI = EMainMenuUI.None;


    private void Awake()
    {
        instance = this;

        mainMenuUI = GetComponentInChildren<MainMenuUI>();
    }

    private void OnEnable()
    {
        if (input == null)
        {
            input = new Player_InputSystem();

            // UI input
            input.UI.Select.performed += i => OnSelectInput(i.ReadValue<Vector2>());
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

        currentUI = EMainMenuUI.MainMenu;
    }

    private void OnSelectInput(Vector2 selectInput)
    {
        switch (currentUI)
        {
            case EMainMenuUI.MainMenu:
                mainMenuUI.OnSelectInput(selectInput);
                break;

            case EMainMenuUI.Setting:

                break;

            case EMainMenuUI.Level:

                break;

            default:
                return;
        }
    }

    private void OnConfirnInput()
    {
        switch (currentUI)
        {
            case EMainMenuUI.MainMenu:
                mainMenuUI.OnConfirnInput();
                break;

            case EMainMenuUI.Setting:

                break;

            case EMainMenuUI.Level:

                break;

            default:
                return;
        }
    }

    private void OnBackInput()
    {
        switch (currentUI)
        {
            case EMainMenuUI.Setting:

                break;

            case EMainMenuUI.Level:

                break;

            default:
                return;
        }
    }

}
