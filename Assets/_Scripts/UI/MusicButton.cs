using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    private void Awake()
    {
        button.GetComponent<Image>().color = Color.white;
    }
    private void Start()
    {
        button.onClick.AddListener(Toggle);
        if (MusicManager.Instance.IsMuted())
        {
            TurnOffMusic();
        }
        else
        {
            TurnOnMusic();
        }
    }

    private void Toggle()
    {
        if (!MusicManager.Instance.IsMuted())
        {
            TurnOffMusic();
        }
        else
        {
            TurnOnMusic();
        }
    }
    private void TurnOffMusic()
    {
        button.GetComponent<Image>().color = Color.gray;
        MusicManager.Instance.Mute();
    }
    private void TurnOnMusic()
    {
        button.GetComponent<Image>().color = Color.white;
        MusicManager.Instance.Unmute();
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
