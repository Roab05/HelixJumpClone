using UnityEngine;

public class Cylinder : MonoBehaviour
{
    private const string SENSITIVITY_KEY = "Sensitivity";

    private float rotateY;

    [SerializeField] private float rotateSensitivityMultiplier;
    [SerializeField] private float rotateSensitivityNormalized;
    private float mousePressedMultiplier = 2.25f;

    private void Awake()
    {
        rotateSensitivityNormalized = PlayerPrefs.GetFloat(SENSITIVITY_KEY, 0.5f);
    }

    private void Start() 
    {
        OptionsUI.Instance.OnSensitivityChanged += OptionsUI_OnSensitivityChanged;
    }

    private void OptionsUI_OnSensitivityChanged(object sender, OptionsUI.OnSensitivityChangedEventArgs e)
    {
        rotateSensitivityNormalized = e.sensitivity;
    }

    private void Update()
    {
        Vector2 inputDeltaVector = GameInput.Instance.GetInputDeltaVector();
        rotateY = -inputDeltaVector.x * (rotateSensitivityMultiplier * rotateSensitivityNormalized);
        if (GameInput.Instance.IsPressed())
        {
            rotateY *= mousePressedMultiplier;
        }
        
        transform.Rotate(0f, rotateY, 0f);
    }
    public void SetRotateSensitivityNormalized(float value)
    {
        rotateSensitivityNormalized = value;
    }
    private void OnDisable()
    {
        OptionsUI.Instance.OnSensitivityChanged -= OptionsUI_OnSensitivityChanged;
    }
}
