using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

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

        InitializeLevelElements();
    }

    private void Start()
    {
        SubscribeEvents();
    }

    #endregion // Singleton

    #region Variables

    #region Level Elements

    [TabGroup("Level Elements", "GameObjects"), InlineEditor(InlineEditorModes.LargePreview), SceneObjectsOnly]
    public GameObject Player;

    [TabGroup("Level Elements", "Animators"), SceneObjectsOnly]
    public Animator CameraAnimator, PlayerAnimator;

    [TabGroup("Level Elements", "Rigidbodies"), SceneObjectsOnly]
    public Rigidbody PlayerRigidbody;

    #endregion // Level Elements

    #region Script Holders

    [BoxGroup("Script Holders"), SceneObjectsOnly, ShowInInspector]
    private GameObject _managerHolder, _controllerHolder;

    #endregion // Script Holders

    #region Manager Prefabs

    /* How to add a new manager?
     * 1-) Create the manager script
     * 2-) Create an empty game object and assign the script to it
     * 3-) Prefabricate the created object
     * 4-) Create a new variable called _...Manager in the Manager Prefabs region
     * 5-) Instantiate it in InitializeManagers() method.
     * 6-) Assign that prefab on created variable.
     */
    [BoxGroup("Managers"), AssetsOnly, ShowInInspector]
    private GameObject _inputManager, _animationManager, _levelManager, _eventManager, _sfxManager;

    #endregion // Manager Prefabs

    #region Controller Prefabs

    [BoxGroup("Controllers"), AssetsOnly, ShowInInspector]
    private GameObject _controllerOnePrefab, _contollerTwoPrefab;

    #endregion // Controller Prefabs

    #region Controller Properties

    public ControllerOne ControllerOne { get; private set; }
    public ControllerTwo ControllerTwo { get; private set; }

    #endregion // Controller Properties

    #endregion // Variables

    #region Methods

    #region Initializations

    private void InitializeLevelElements()
    {
        InitializeManagers();
        SaveSystem.SubscribeEvents();
        InitializeControllers();
    }

    private void InitializeManagers()
    {
        Instantiate(_eventManager, _managerHolder.transform);
        Instantiate(_inputManager, _managerHolder.transform);
        Instantiate(_animationManager, _managerHolder.transform);
        Instantiate(_levelManager, _managerHolder.transform);
        Instantiate(_sfxManager, _managerHolder.transform);
    }

    private void InitializeControllers()
    {
        ControllerOne = Instantiate(_controllerOnePrefab, _controllerHolder.transform).GetComponent<ControllerOne>();
        ControllerTwo = Instantiate(_contollerTwoPrefab, _controllerHolder.transform).GetComponent<ControllerTwo>();
    }

    #endregion // Initializations.

    #region Events

    private void SubscribeEvents()
    {

    }

    private void UnsubscribeEvents()
    {

    }

    #endregion // Events

    #region OnDestroy

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion // OnDestroy

    #endregion // Methods
}
