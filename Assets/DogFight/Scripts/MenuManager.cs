using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    private static MenuManager m_Instance;
    public static MenuManager Instance
    {
        get { return m_Instance; }
        set
        {
            if (value == null)
                Debug.Log("Instance cant be set to null.");
            else
                m_Instance = value;
        }
    }
    public string playerName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    public void NameEntered(string str) {
        playerName = str;
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void EndGame() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    } 
}
