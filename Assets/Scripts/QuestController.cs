using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; private set; }

    public bool PointerVisible
    { 
        get => _questPointer.activeSelf;
        set => _questPointer.SetActive(value); 
    }

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _questPointer;

    [SerializeField] private Sprite _defaultPointerSprite;
    [SerializeField] private Vector2 _defaultPointerScale;
    [SerializeField] private Sprite _questKeyPointerSprite;
    [SerializeField] private Vector2 _questKeyPointerScale;

    [SerializeField] private float _questPointerRadius;
    [SerializeField] private float _newQuestMinDistanceThreshold;
    [SerializeField] private float _questPointerVisibilityThreshold;

    [SerializeField] private TMP_Text _questRemainingSatellites;
    [SerializeField] private GameObject _gameFinishedScreen;

    private int _fixedSatelliteCount;
    private bool _isDocked;
    private bool _canGetNewQuest = true;
    private bool _canUpdateCurrentQuest;
    private Transform _questSatelliteTransform;
    private SpriteRenderer _spriteRenderer;
    private List<Transform> _satellites = new List<Transform>();


    private void Awake()
    {
        Instance = this;  
    }

    private void Start()
    {
        _spriteRenderer = _questPointer.GetComponent<SpriteRenderer>();
        PointerVisible = false;
    }

    private void Update()
    {
        if (_satellites.Count > 0 && _canGetNewQuest)
        {
            _canGetNewQuest = false;
            GenerateNewQuest();
        }

        if (_canUpdateCurrentQuest)
        {
            UpdateCurrentQuest();
        }
    }

    public void SetSatellites(List<Transform> satellites)
    {
        _satellites = satellites;
    }

    private void GenerateNewQuest()
    {
        List<Transform> tempSatellites = _satellites;
        for (int i = 0; i < tempSatellites.Count; i++)
        {
            int randIndex = Random.Range(0, tempSatellites.Count);
            float distance = Vector2.Distance(_player.position, tempSatellites[randIndex].position);
            if (distance > _newQuestMinDistanceThreshold)
            {
                _questSatelliteTransform = tempSatellites[randIndex];
                PointerVisible = true;
                break;
            }
            else
            {
                tempSatellites.RemoveAt(randIndex);
            }
        }
        _canUpdateCurrentQuest = true;
    }

    private void UpdateCurrentQuest()
    {
        float distance = Vector2.Distance(_player.position, _questSatelliteTransform.position);
        if (distance > _questPointerVisibilityThreshold)
        {
            _spriteRenderer.sprite = _defaultPointerSprite;
            Vector3 direction = (_questSatelliteTransform.position - _player.position).normalized;
            _questPointer.transform.position = direction * _questPointerRadius + _player.position;
            _questPointer.transform.up = direction;
            _questPointer.transform.localScale = _defaultPointerScale;
        }
        else
        {
            _spriteRenderer.sprite = _questKeyPointerSprite;
            _questPointer.transform.position = _questSatelliteTransform.position + Vector3.up * _questPointerRadius;
            _questPointer.transform.up = Vector3.up;
            _questPointer.transform.localScale = _questKeyPointerScale;
        }

        if (!_isDocked && Input.GetKeyDown(KeyCode.E) && distance < 2)
        {
            ShipController.Instance.CanMove = false;
            _player.SetParent(_questSatelliteTransform);
            PointerVisible = false;
            _isDocked = true;
        }
    }

    public void UpdateFixedSatellites()
    {
        _fixedSatelliteCount++;
        _questRemainingSatellites.text = $"Satellites fixed {_fixedSatelliteCount} / 5";

        if (_fixedSatelliteCount == 5)
        {
            Time.timeScale = 0;
            _gameFinishedScreen.SetActive(true);
        }
    }
}
