using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image levelProgressImage;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    
    private float targetFillAmount = 0f;

    private void Start()
    {
        Ball.Instance.OnRingPlatformPassed += Ball_OnRingPlatformPassed;
        LevelManager.Instance.OnLevelChanged += LevelManager_OnLevelChanged;
    }

    private void Update()
    {
        float lerpSpeed = 10f;
        levelProgressImage.fillAmount = Mathf.Lerp(levelProgressImage.fillAmount, targetFillAmount, Time.deltaTime * lerpSpeed);
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

    private void OnDestroy()
    {
        LevelManager.Instance.OnLevelChanged -= LevelManager_OnLevelChanged;
        Ball.Instance.OnRingPlatformPassed -= Ball_OnRingPlatformPassed;
    }
}
