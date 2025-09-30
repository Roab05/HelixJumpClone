using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    private void Awake()
    {
        button.GetComponent<Image>().color = Color.white;
    }

    private void Start()
    {
        if (SoundManager.Instance.IsMuted())
        {
            TurnOffSound();
        }
        else
        {
            TurnOnSound();
        }
        button.onClick.AddListener(Toggle);
    }

    private void Toggle()
    {
        if (SoundManager.Instance.IsMuted())
        {
            TurnOnSound();
        }
        else
        {
            TurnOffSound();
        }
    }

    private void TurnOnSound()
    {
        button.GetComponent<Image>().color = Color.white;
        SoundManager.Instance.Unmute();
    }
    private void TurnOffSound()
    {
        button.GetComponent<Image>().color = Color.gray;
        SoundManager.Instance.Mute();
    }
}
