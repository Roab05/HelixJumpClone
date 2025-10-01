using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private const string GAME_OVER_UI_SHOW = "GameOverUIShow";

    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI bestLevelText;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            bestScoreText.text = ScoreManager.Instance.GetBestScore();
            if (ScoreManager.Instance.isNewBest)
                bestScoreText.color = Color.yellow;
            bestLevelText.text = LevelManager.Instance.GetBestLevel();
            if (LevelManager.Instance.isNewBest)
                bestLevelText.color = Color.yellow;
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
        animator.Play(GAME_OVER_UI_SHOW, 0, 0);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
