using System.Collections.Generic;
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
    [SerializeField] private Sprite _questKeyPointerSprite;

    [SerializeField] private float _questPointerRadius;
    [SerializeField] private float _newQuestMinDistanceThreshold;
    [SerializeField] private float _questPointerVisibilityThreshold;

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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        PointerVisible = false;
    }

    public void SetSatellites(List<Transform> satellites)
    {
        _satellites = satellites;
    }
}
