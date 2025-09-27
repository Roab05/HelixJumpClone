using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    public event EventHandler OnTap;

    private int isMouseLeftButtonPressed = 0;
    private bool isTapped = false;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();

        playerInputActions.Enable();

        playerInputActions.Player.MouseLeftButtonPress.performed += ctx => isMouseLeftButtonPressed = 1;
        playerInputActions.Player.MouseLeftButtonPress.canceled += ctx => isMouseLeftButtonPressed = 0;
        playerInputActions.Player.Tap.performed += Tap_performed;
    }
    private void Tap_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isTapped = true;
    }
    private void Update()
    {
        if (isTapped)
        {
            isTapped = false;
            if (IsOverUI())
            {
                return;
            }
            OnTap?.Invoke(this, EventArgs.Empty);
        }
    }
    public Vector2 GetInputDeltaVector()
    {
        if (GameManager.Instance.IsWaitingToStart() || SensitivitySlider.Instance.IsHandleHeld())
        {
            return Vector2.zero;
        }
        Vector2 inputDeltaVector1 = playerInputActions.Player.Rotate.ReadValue<Vector2>();
        Vector2 inputDeltaVector2 = playerInputActions.Player.MouseRotate.ReadValue<Vector2>() * isMouseLeftButtonPressed;
        return inputDeltaVector1 + inputDeltaVector2;
    }

    public bool IsPressed()
    {
        return isMouseLeftButtonPressed == 1;
    }

    private bool IsOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Dispose();
    }
}
