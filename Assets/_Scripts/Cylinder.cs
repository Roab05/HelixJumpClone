using UnityEngine;

public class Cylinder : MonoBehaviour
{
    [SerializeField] private float rotateSensitivity;
    private float mousePressedMultiplier = 2f;
    private void Update()
    {
        Vector2 inputDeltaVector = GameInput.Instance.GetInputDeltaVector();
        float rotateY = -inputDeltaVector.x * rotateSensitivity;
        if (GameInput.Instance.IsMouseLeftButtonPressed())
        {
            rotateY *= mousePressedMultiplier;
        }
        transform.Rotate(0f, rotateY, 0f);
    }
}
