using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingUI : MonoBehaviour
{
    [SerializeField] private Transform scoreTextTransform;
    [SerializeField] private Transform levelProgressTransform;
    [SerializeField] private Image levelProgressImage;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    private float targetFillAmount = 0f;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        Ball.Instance.OnRingPlatformPassed += Ball_OnRingPlatformPassed;
        LevelManager.Instance.OnLevelChanged += LevelManager_OnLevelChanged;

        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStart())
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void ScoreManager_OnScoreChanged(object sender, System.EventArgs e)
    {
        scoreTextTransform.GetComponent<TextMeshProUGUI>().text = ScoreManager.Instance.GetScore().ToString();
    }

    private void Ball_OnRingPlatformPassed(object sender, System.EventArgs e)
    {
        targetFillAmount += 1f / RingPlatformPool.Instance.GetPlatformAmount();
    }
    private void LevelManager_OnLevelChanged(object sender, System.EventArgs e)
    {
        targetFillAmount = 0f;
        currentLevelText.text = LevelManager.Instance.GetLevel().ToString();
        nextLevelText.text = (LevelManager.Instance.GetLevel() + 1).ToString();
    }

    private void Update()
    {
        float lerpSpeed = 10f;
        levelProgressImage.fillAmount = Mathf.Lerp(levelProgressImage.fillAmount, targetFillAmount, Time.deltaTime * lerpSpeed);
    }

    private void Show()
    {
        scoreTextTransform.gameObject.SetActive(true);
        levelProgressTransform.gameObject.SetActive(true);
    }
    private void Hide()
    {
        scoreTextTransform.gameObject.SetActive(false);
        levelProgressTransform.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= ScoreManager_OnScoreChanged;
        Ball.Instance.OnRingPlatformPassed -= Ball_OnRingPlatformPassed;
        LevelManager.Instance.OnLevelChanged -= LevelManager_OnLevelChanged;

        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }
}
