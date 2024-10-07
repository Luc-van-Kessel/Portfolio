using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{ 
    // singleton instance
    public static GameManager Instance;

    public GameState CurrentState = GameState.Countdown;

    public Countdown GameCountdown;
    public ObjectManager ObjectManager; 
    public GameObject StandBox;
    public WaveManager WaveManager;
    public UIManager UiManager;  
    public PlayerStats PlayerStats;
    public EnemyStats[] EnemyStats;
    public EffectSystem EffectSystem;
    public PlayableDirector PlayableDirector;
    public PlayableDirector EnterCurse;
    public PlayableDirector ExitCurse;
    public Animator anim;

    public bool isPaused = false;
   
    private void Awake()
    {
        // Create singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerStats.ResetToBaseStats();

        // reset all enemy stats to base stats
        foreach (var enemy in EnemyStats)
        {   
            enemy.ResetToBaseStats();
        }
    }

    private void Start()
    {
        // Start the countdown when the game begins
        EnterCountdownState();
        UiManager.ShowScore();
    }

    private void Update()
    {
        // Check the current state and handle logic accordingly
        switch (CurrentState)
        { 
            case GameState.Countdown:
                // Handle countdown state  
                
                UpdateCountdownState();
                break;
            case GameState.GamePlay:
                // Handle gameplay state 
                UpdateGameplayState();
                break;
            case GameState.PlayerInteraction:
                // Handle player interaction state 
                UpdatePlayerInteractionState();
                break;
        }
    }

    private void EnterCountdownState()
    {
        CurrentState = GameState.Countdown;
        GameCountdown.StartCountdown(OnCountdownFinished);
        if (PlayableDirector != null)
        {
            PlayableDirector.Play();
        }
    }

    /// <summary>
    /// The enters the gameplay state.
    /// </summary>
    private void EnterGameplayState()
    { 
        CurrentState = GameState.GamePlay;
        ObjectManager.MoveObjectsUp(); 
        //ObjectManager.HideStandBox(StandBox);
        EffectSystem.ResetEffects();
        //StandBox.GetComponent<Collider>().enabled = true;
        ObjectManager.OnObjectsMoved += StartSpawning; // Subscribe to event
    }
    private void StartSpawning()
    { 
        ObjectManager.OnObjectsMoved -= StartSpawning; // Unsubscribe from event
        WaveManager.StartNextWave(); // Start spawning enemies
    } 

    public void PopUp()
    {
        //UiManager.ShowUpgradePopup(); 
        //Invoke("OnUpgradePopupClosed", 2);
        //OnUpgradePopupClosed();
    }

    public void EnterPlayerInteractionState()
    {
        EnterCurse.Play();
        anim.SetBool("PopUp", true);
        Invoke("ShowUpgradePopup", 2.5f);
        CurrentState = GameState.PlayerInteraction;
        EffectSystem.SetEffects();
        //if (StandBox != null)
        //{
        //    ObjectManager.ShowStandBox(StandBox); 

        //}
        //else
        //{
        //    Debug.LogWarning("StandBox GameObject is null. Cannot show stand box.");
        //}

        WaveManager.PauseSpawning();
        ObjectManager.SwitchObjects();
        // Invoke ShowUpgradePopup method after 1 second
    }

    private void ShowUpgradePopup()
    {
        UiManager.ShowUpgradePopup();
    }



    private void OnCountdownFinished()
    { 
        EnterGameplayState();
    }

    public void OnEnemiesDestroyed()
    {
        if (CurrentState == GameState.GamePlay)
        {
            EnterPlayerInteractionState();
        }
    }

    public void OnUpgradePopupClosed()
    {

        ExitCurse.Play();
        anim.SetBool("PopUp", false); 

        if (CurrentState == GameState.PlayerInteraction)
        {
            EnterGameplayState();
        }
    }
    private void UpdateCountdownState()
    {
    }

    private void UpdateGameplayState()
    {
        UiManager.ShowScore();
    }

    private void UpdatePlayerInteractionState()
    { 
        // Handle player interaction state logic
        // For example, pause spawning, display UI elements for player interaction, wait for player input, etc.
    }

    private void UpdateArenaChangeState()
    {
        // Handle arena change state logic
        // For example, transition the arena, resume spawning, prepare for the next wave of enemies, etc.
    }
}
