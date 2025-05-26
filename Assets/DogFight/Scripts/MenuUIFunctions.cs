using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIFunctions : MonoBehaviour
{
    public TextMeshProUGUI warnings;
    public TMP_InputField nameFieldText;
    private bool allowedToPlay;

    void Start()
    {
        allowedToPlay = false;
        if (SceneInfo.Instance.playerName != null)
        {
            nameFieldText.text = SceneInfo.Instance.playerName;
            allowedToPlay = true;
        }

    }
    public void NameEntered(string str)
    {
        if (str == "")
        {
            warnings.gameObject.SetActive(true);
            warnings.text = "Player name cannot be empty.";
            return;
        }
        warnings.gameObject.SetActive(false);
        allowedToPlay = true;
        SceneInfo.Instance.SetPlayerName(str);
    }

    public void StartGame() {
        if (!allowedToPlay)
            return;
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