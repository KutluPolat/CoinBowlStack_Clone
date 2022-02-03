using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Coati.CoinBowlStack.Enums;

public class LevelManager : MonoBehaviour
{
    #region Singleton

    public static LevelManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SubscribeEvents();
        InitializeLevel();
    }

    #endregion // Singleton

    #region Variables

    public GameState CurrentGameState = GameState.TapToPlay;

    [HideInInspector]
    public int NumberOfLevels { get { return _levels.Count; } }

    [SerializeField]
    private List<GameObject> _levels = new List<GameObject>();

    [SerializeField]
    private GameStateHandler _gameStateHandler;

    private GameObject _activeLevel;

    #endregion // Variables

    #region Update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
        }
    }

    #endregion // Update

    #region Methods

    #region Scene Management

    public void InitializeLevel()
    {
        _activeLevel = Instantiate(_levels[SaveSystem.IndexOfLevel]);
    }

    private void NextLevel()
    {
        SaveSystem.Level++;
        ReloadActiveScene();
    }

    private void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetData()
    {
        SaveSystem.Level = 1;
        SaveSystem.HowManyPrizeOpened = 0;
        SaveSystem.TotalAssets = 0;
        SaveSystem.FillRate = 0;
        ReloadActiveScene();
    }

    #endregion // Scene Management

    #region Events

    private void SubscribeEvents()
    {
        EventManager.Instance.PressedRestart += ReloadActiveScene;
        EventManager.Instance.PressedNextLevel += NextLevel;

        _gameStateHandler.SubscribeEvents();
    }
    private void UnsubscribeEvents()
    {
        EventManager.Instance.PressedRestart -= ReloadActiveScene;
        EventManager.Instance.PressedNextLevel -= NextLevel;
    }

    #endregion // Sub & Unsub Events

    #region OnDestroy

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion // OnDestroy

    #endregion // Methods
}
