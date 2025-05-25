using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Gradient fuelColor;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI bulletCountText;
    [SerializeField] private TextMeshProUGUI playerNameDisplay;
    [SerializeField] private TextMeshProUGUI scoreDisplayText;
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float fuelDecreaseInterval;
    [SerializeField] private int score;
    private float fuel;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        score = 0;
        scoreDisplayText.text = "Score: " + score;
        playerNameDisplay.text = "Pilot Name: " + MenuManager.Instance.playerName;
        fuel = maxFuel;
        SetMaxFuelLevel(maxFuel);
        SetFuel(fuel);
        InvokeRepeating("DecreaseFuel", 1f, fuelDecreaseInterval);
    }

    public void PlaneDestroyed()
    {
        GameObject newEnemy = Instantiate(enemy, new Vector3(0, 0, 450), Quaternion.identity);
        newEnemy.name = "Enemy";
        score++;
        scoreDisplayText.text = "Score: " + score;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void DisplayBulletCount(int value)
    {
        bulletCountText.text = "Ammo: " + value;
    }
    public void SetFuel(float value)
    {
        slider.value = value;
        fill.color = fuelColor.Evaluate(slider.normalizedValue);
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
            fuel -= 1;
            SetFuel(fuel);
        }
    }
}

