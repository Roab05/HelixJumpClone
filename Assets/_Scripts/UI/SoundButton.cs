using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    private bool isSoundEnabled = true;
    [SerializeField]
    private Button button;

    private void Awake()
    {
        button.GetComponent<Image>().color = Color.white;
    }

    private void Start()
    {
        button.onClick.AddListener(Toggle);
    }

    private void Toggle()
    {
        if (isSoundEnabled)
        {
            button.GetComponent<Image>().color = Color.gray;
            SoundManager.Instance.Mute();
        }
        else
        {
            button.GetComponent<Image>().color = Color.white;
            SoundManager.Instance.Unmute();
        }
        isSoundEnabled = !isSoundEnabled;
    }
}
