using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;
    private int isMouseLeftButtonPressed = 0;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();

        playerInputActions.Enable();

        playerInputActions.Player.MouseLeftButtonPress.performed += ctx => isMouseLeftButtonPressed = 1;
        playerInputActions.Player.MouseLeftButtonPress.canceled += ctx => isMouseLeftButtonPressed = 0;
    }

    public Vector2 GetInputDeltaVector()
    {
        Vector2 inputDeltaVector1 = playerInputActions.Player.Rotate.ReadValue<Vector2>();
        Vector2 inputDeltaVector2 = playerInputActions.Player.MouseRotate.ReadValue<Vector2>() * isMouseLeftButtonPressed;
        return inputDeltaVector1 + inputDeltaVector2;
    }

    public bool IsMouseLeftButtonPressed()
    {
        return isMouseLeftButtonPressed == 1;
    }
    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Dispose();
    }
}
