using UnityEngine;

public class SceneInfo : MonoBehaviour
{
    public static SceneInfo Instance;
    public string playerName;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetPlayerName(string name)
    {
        playerName = name;
    }
}
