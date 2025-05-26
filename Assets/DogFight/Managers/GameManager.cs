using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private ParticleSystem crashFire;
    [SerializeField] private Gradient fuelColor;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI bulletCountText;
    [SerializeField] private TextMeshProUGUI playerNameDisplay;
    [SerializeField] private TextMeshProUGUI scoreDisplayText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject inGameScreen;
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] public float terrainMovementSpeed;
    [SerializeField] private float fuelDecreaseInterval;
    [SerializeField] private int score;
    private string saveFilePath;
    public bool gameEnded;
    private float fuel;

    [System.Serializable]
    class SaveTemplate
    {
        public int score;
        public string playerName;
        public SaveTemplate(string nameValue, int scoreValue)
        {
            score = scoreValue;
            playerName = nameValue;
        }
    }
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SoundFXManager.Instance.PlayBgSound(1f);
        gameEnded = false;
        score = 0;
        scoreDisplayText.text = "Score: " + score;
        if (SceneInfo.Instance != null)
            playerNameDisplay.text = "Pilot Name: " + SceneInfo.Instance.playerName;
        else
            playerNameDisplay.text = "Pilot Name: Unknown";
        fuel = maxFuel;
        SetMaxFuelLevel(maxFuel);
        SetFuel(fuel);
        InvokeRepeating("DecreaseFuel", 1f, fuelDecreaseInterval);
        saveFilePath = Application.persistentDataPath + "savefile.json";
        SaveTemplate currentHighScore = ReadHighScore();
        if (currentHighScore.playerName == "")
            highScoreText.text = $"Highscore Not Set";
        else
            highScoreText.text = $"Player Name: {currentHighScore.playerName} High Score: {currentHighScore.score}";
    }

    public void PlaneDestroyed()
    {
        GameObject newEnemy = Instantiate(enemy, new Vector3(0, 0, 450), Quaternion.identity);
        newEnemy.name = "Enemy";
        score++;
        scoreDisplayText.text = "Score: " + score;
    }

    public void PlayerCrashed(Vector3 position)
    {
        SoundFXManager.Instance.StopBackgroundSound();
        crashFire.transform.position = position;
        crashFire.gameObject.SetActive(true);
        crashFire.Play();
        gameEnded = true;
        deathScreen.SetActive(true);
        inGameScreen.SetActive(false);
        finalScoreText.text = "Score: " + score;
        ReviseHighScore();
    }

    void SaveNewHighScore(SaveTemplate dataObj)
    {
        string jsonData = JsonUtility.ToJson(dataObj);
        File.WriteAllText(saveFilePath, jsonData);
    }

    SaveTemplate ReadHighScore()
    {
        if (File.Exists(saveFilePath))
        {
            string data = File.ReadAllText(saveFilePath);
            SaveTemplate dataObj = JsonUtility.FromJson<SaveTemplate>(data);
            return dataObj;
        }
        else
            return new SaveTemplate("", 0);
    }

    void ReviseHighScore()
    {
        SaveTemplate dataObj = ReadHighScore();
        if (dataObj.playerName == "")
            SaveNewHighScore(new SaveTemplate(SceneInfo.Instance.playerName, score));
        else
        {
            if (score >= dataObj.score)
                SaveNewHighScore(new SaveTemplate(SceneInfo.Instance.playerName, score));
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void DisplayBulletCount(int value)
    {
        bulletCountText.text = "Ammo: " + value;
    }
    private void SetFuel(float value)
    {
        slider.value = value;
        fill.color = fuelColor.Evaluate(slider.normalizedValue);
        Debug.Log($"Slider: {value}, Normalized: {slider.normalizedValue}");
    }

    public void AddFuel(float value)
    {
        fuel += value;
        SetFuel(Mathf.Clamp(fuel, 0f, maxFuel));
    }

    public void SetMaxFuelLevel(float value)
    {
        slider.maxValue = value;
        fill.color = fuelColor.Evaluate(1f);
    }

    void DecreaseFuel()
    {
        if (fuel <= 0)
        {
            PlaneController.Instance.tankEmpty = true;
        }
        else
        {
            AddFuel(-1f);
        }
    }
}

