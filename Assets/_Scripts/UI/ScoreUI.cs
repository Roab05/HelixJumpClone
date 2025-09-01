using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Transform scoreTextTransform;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
    }

    private void ScoreManager_OnScoreChanged(object sender, System.EventArgs e)
    {
        scoreTextTransform.GetComponent<TextMeshProUGUI>().text = ScoreManager.Instance.GetScore().ToString();
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnScoreChanged -= ScoreManager_OnScoreChanged;
    }
}
