using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
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

    public void Toggle()
    {
        if (isSoundEnabled)
        {
            button.GetComponent<Image>().color = Color.gray;
            MusicManager.Instance.Mute();
        }
        else
        {
            button.GetComponent<Image>().color = Color.white;
            MusicManager.Instance.Unmute();
        }
        isSoundEnabled = !isSoundEnabled;
    }
}
