using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }


    private const string SENSITIVITY_KEY = "Sensitivity";

    [SerializeField]
    private Slider sensitivitySlider;

    public event EventHandler<OnSensitivityChangedEventArgs> OnSensitivityChanged;
    public class OnSensitivityChangedEventArgs : EventArgs
    {         
        public float sensitivity;
    }

    private Animator animator;

    private bool isUIShown = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        animator = GetComponent<Animator>();
        sensitivitySlider.value = PlayerPrefs.GetFloat(SENSITIVITY_KEY, 0.5f);
    }

    private void Start()
    {
        sensitivitySlider.onValueChanged.AddListener((v) =>
        {
            // save sensitivity
            PlayerPrefs.SetFloat(SENSITIVITY_KEY, v);

            OnSensitivityChanged?.Invoke(this, new OnSensitivityChangedEventArgs
            {
                sensitivity = v
            });
        });
    }

    public void OptionsToggle()
    {
        if (isUIShown)
        {
            // hide ui
            animator.SetTrigger("HideUI");
        }
        else
        {
            // show ui
            animator.SetTrigger("ShowUI");
        }
        isUIShown = !isUIShown;
    }
    public float GetSensitivitySliderValue()
    {
        return sensitivitySlider.value;
    }
}
