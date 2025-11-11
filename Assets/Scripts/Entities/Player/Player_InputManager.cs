using UnityEngine;

public class Player_InputManager : MonoBehaviour
{
    private Player player;

    private Player_InputSystem input;
    private Vector2 moveInput;

    public float verticalValue { get; private set; }
    public float horizontalValue { get; private set; }


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        if (input == null)
        {
            input = new Player_InputSystem();

            // Player input
            input.Player.Movement.performed += i => moveInput = i.ReadValue<Vector2>();
            input.Player.Active.performed += i => player.OnActiveInput();
            // UI input
            input.UI.Confirm.performed += i => UIManager.instance.OnConfirmInput();
            input.UI.Back.performed += i => UIManager.instance.OnBackInput();
        }

        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        EnablePlayerInput(true);
    }

    private void Update()
    {
        HandleInputValue();
    }

    public void EnablePlayerInput(bool enable)
    {
        if (enable)
        {
            Debug.Log($"[{name}]: Enable Player input");
            input.Player.Enable();

            Debug.Log($"[{name}]: Disable UI input");
            input.UI.Disable();
        }
        else
        {
            Debug.Log($"[{name}]: Disable Player input");
            input.Player.Disable();

            Debug.Log($"[{name}]: Enable UI input");
            input.UI.Enable();

            moveInput = Vector3.zero;
        }
    }

    private void HandleInputValue()
    {
        verticalValue = moveInput.y;
        horizontalValue = moveInput.x;
    }

    public bool IsWalking()
    {
        return horizontalValue != 0 || verticalValue != 0;
    }
}
