using UnityEngine;

public class DontDestroyOnLoadObjects : MonoBehaviour
{
    public static DontDestroyOnLoadObjects Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
