
using UnityEngine;
using UnityEngine.Events;


public class TurnManager : MonoBehaviour
{

    // The TurnState enum defines the global game flow.
    // Only TurnManager can change states, ensuring a controlled turn order.
    // Other systems (UI, Combat) react via OnTurnChanged event.
    // Each action taken by the player ends their turn.
    // The enemy then waits for a short delay before executing its action (ensuring animations are visible and readable)

    public static TurnManager Instance { get; private set; }

    public enum TurnState { PlayerTurn, EnemyTurn, GameOver }

    [SerializeField] private float enemyTurnDelay = 1.5f;
    [SerializeField] private EnemyCombat _enemy;
    


    public UnityEvent<TurnState> OnTurnChanged = new UnityEvent<TurnState>();

    public TurnState CurrentTurn { get; private set; }

   

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

       
        
    }

    private void Start()
    {
        TransitionTo(TurnState.PlayerTurn);
    }

    public void PlayerEndTurn()
    {
        if (CurrentTurn != TurnState.PlayerTurn) return;
        TransitionTo(TurnState.EnemyTurn);
    }

    public void EndGame()
    {
        TransitionTo(TurnState.GameOver);
    }

    private void OnPlayerTurn()
    {
        Debug.Log("Player Turn");
    }

    private void OnEnemyTurn()
    {
        Debug.Log("Enemy Turn");

        if (_enemy == null)
        {
            
            Invoke(nameof(EnemyEndTurn), _enemy.actionDuration);
            return;
        }

        Invoke(nameof(ExecuteEnemyTurn), enemyTurnDelay); 


    }


    private void ExecuteEnemyTurn()
    {
        if (_enemy != null)
        {
            _enemy.TakeTurn();
        }

        Invoke(nameof(EnemyEndTurn), _enemy.actionDuration);
    }

    private void EnemyEndTurn()
    {
        if (CurrentTurn != TurnState.EnemyTurn) return;
        TransitionTo(TurnState.PlayerTurn);
    }
    
    private void OnGameOver()
    {
        Debug.Log("GAME OVER");

        Time.timeScale = 0f; // FREEZE GAME : placeholder for logic
    }

    private void TransitionTo(TurnState next)
    {
        Debug.Log("STATE :--> " + next);

        CurrentTurn = next;
        OnTurnChanged?.Invoke(next);

        switch (next)
        {
            case TurnState.PlayerTurn: OnPlayerTurn(); break;
            case TurnState.EnemyTurn: OnEnemyTurn(); break;
            case TurnState.GameOver: OnGameOver(); break;
        }
    }
}

