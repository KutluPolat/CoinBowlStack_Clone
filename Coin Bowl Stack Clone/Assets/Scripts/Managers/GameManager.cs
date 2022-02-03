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
        EventManager.Instance.OnStateTapToPlay();
    }

    #endregion // Singleton

    #region Variables

    #region UI Elements

    [HideInInspector]
    public float CollectedAsset, TotalAssetInThisLevel;

    #endregion // UI Elements

    #region Level Elements

    [TabGroup("Elements", "GameObject"), InlineEditor(InlineEditorModes.LargePreview), SceneObjectsOnly]
    public GameObject Player;

    [TabGroup("Elements", "Animators"), SceneObjectsOnly]
    public Animator CameraAnimator, PlayerAnimator;

    [TabGroup("Elements", "CharacterController"), SceneObjectsOnly]
    public CharacterController PlayerCharacterController;

    #endregion // Level Elements

    #region Script Holders

    [BoxGroup("Script Holders"), SceneObjectsOnly, SerializeField]
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
    [BoxGroup("Managers"), AssetsOnly, SerializeField]
    private GameObject _animationManager, _levelManager, _eventManager;

    #endregion // Manager Prefabs

    #region Controllers

    [BoxGroup("Controllers"), SceneObjectsOnly]
    public MovementController MovementController;

    [BoxGroup("Controllers"), SceneObjectsOnly]
    public InputController InputController;

    [BoxGroup("Controllers"), SceneObjectsOnly]
    public StackController StackController;

    [BoxGroup("Controllers"), SceneObjectsOnly]
    public UIController UIController;

    #endregion // Controllers

    #region Handlers

    [BoxGroup("Handlers"), SceneObjectsOnly]
    public ExplosionHandler ExplosionHandler;

    #endregion // Handlers

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
        Instantiate(_animationManager, _managerHolder.transform);
        Instantiate(_levelManager, _managerHolder.transform);
    }

    private void InitializeControllers()
    {
        MovementController.SubscribeEvents();
        //InputController.SubscribeEvents();
        StackController.SubscribeEvents();
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
