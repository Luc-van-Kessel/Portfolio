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
    public Animator Anim;

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
                break;
            case GameState.GamePlay:
                // Handle gameplay state 
                UpdateGameplayState();
                break;
            case GameState.PlayerInteraction:
                // Handle player interaction state 
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
        EffectSystem.ResetEffects();
        ObjectManager.OnObjectsMoved += StartSpawning; 
    }
    private void StartSpawning()
    { 
        ObjectManager.OnObjectsMoved -= StartSpawning;
        WaveManager.StartNextWave();
    } 

    public void EnterPlayerInteractionState()
    {
        EnterCurse.Play();
        Anim.SetBool("PopUp", true);
        Invoke("ShowUpgradePopup", 2.5f);
        CurrentState = GameState.PlayerInteraction;
        EffectSystem.SetEffects();
        WaveManager.PauseSpawning();
        ObjectManager.SwitchObjects();
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
        Anim.SetBool("PopUp", false); 

        if (CurrentState == GameState.PlayerInteraction)
        {
            EnterGameplayState();
        }
    }

    private void UpdateGameplayState()
    {
        UiManager.ShowScore();
    }

}
