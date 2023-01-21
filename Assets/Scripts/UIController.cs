using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    [Header("Timer")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _time;

    [Header("Fuel")]
    [SerializeField] private Slider _fuelSlider;
    [SerializeField] private TMP_Text _fuelText;
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _fuelConssumptionRate;

    private ShipController _shipController;
    private float _fuel;
    private float _timeRemaining;

    private void Start()
    {
        _shipController = ShipController.Instance;
        _timeRemaining = _time;
        _fuel = _maxFuel;
    }

    private void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
            if (_timeRemaining < 0)
                _timeRemaining = 0;
            UpdateTimer();
        }
        
        if (_shipController.Moving && _fuel > 0)
        {
            _fuel -= Time.deltaTime * _shipController.Magnitude * _fuelConssumptionRate;
            UpdateFuelUI();
        }

        if (_fuel <= 0 || _timeRemaining <= 0)
        {
            EndGame();
        }
    }

    private void UpdateFuelUI()
    {
        _fuelSlider.maxValue = _maxFuel;
        _fuelSlider.value = _fuel;
        int leftFuel = Mathf.RoundToInt(_fuel);
        _fuelText.text = $"Fuel remaining {leftFuel} / {_maxFuel}";
    }

    private void UpdateTimer()
    {
        _timerText.text =
            $"Time remaining " +
            $"{(int)_timeRemaining / 60:00}:{(int)_timeRemaining % 60:00}";
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
}
