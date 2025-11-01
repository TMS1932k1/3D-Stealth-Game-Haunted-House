using UnityEngine;

public class Player_InputManager : MonoBehaviour
{
    private Player_InputSystem input;
    private Vector2 moveInput;

    public float verticalValue { get; private set; }
    public float horizontalValue { get; private set; }


    private void OnEnable()
    {
        if (input == null)
        {
            input = new Player_InputSystem();
            input.Player.Movement.performed += i => moveInput = i.ReadValue<Vector2>();
        }

        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        HandleInputValue();
    }

    public void DisableMoveInput()
    {
        Debug.Log($"[{name}]: Disable move input");
        input.Disable();
        moveInput = Vector3.zero;
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
