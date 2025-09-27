using UnityEngine;

public class Cylinder : MonoBehaviour
{
    private const string SENSITIVITY_KEY = "Sensitivity";

    private float waitingToStartRotateY = 0.1f;
    private float rotateY;

    [SerializeField] private float rotateSensitivityMultiplier;
    [SerializeField] private float rotateSensitivityNormalized;
    private float mousePressedMultiplier = 2.25f;

    private void Start()
    {
        rotateSensitivityNormalized = PlayerPrefs.GetFloat(SENSITIVITY_KEY, 0.5f);
        OptionsUI.Instance.OnSensitivityChanged += OptionsUI_OnSensitivityChanged;
    }

    private void OptionsUI_OnSensitivityChanged(object sender, OptionsUI.OnSensitivityChangedEventArgs e)
    {
        rotateSensitivityNormalized = e.sensitivity;
    }

    private void Update()
    {
        if (GameManager.Instance.IsWaitingToStart())
        {
            rotateY = waitingToStartRotateY;
        }
        else
        {
            Vector2 inputDeltaVector = GameInput.Instance.GetInputDeltaVector();
            rotateY = -inputDeltaVector.x * (rotateSensitivityMultiplier * rotateSensitivityNormalized);
            if (GameInput.Instance.IsPressed())
            {
                rotateY *= mousePressedMultiplier;
            }
        }
        transform.Rotate(0f, rotateY, 0f);
    }
    public void SetRotateSensitivityNormalized(float value)
    {
        rotateSensitivityNormalized = value;
    }
}
