
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    // Player actions trigger animations.
    // Damage is applied via Animation Events to sync visuals with logic.
    // Both player and enemy use Animator Controllers with different states
    // Damage is applied using Animation Events, ensuring that hits are synch with gameplay and visuals.

    [SerializeField] private int attackDamage = 20;
    [SerializeField] private int healAmount = 15;
    [SerializeField] private Health _enemy;

    private Animator _animator;
    private bool _hasActed;
    private Health _self;

    private void Awake()
    {
        TryGetComponent(out _animator);
     

        _self = GetComponent<Health>();
        if (_self == null) Debug.LogError("PlayerCombat: no Health component found on player.");
    }
    private void Start()
    {
        TurnManager.Instance.OnTurnChanged.AddListener(OnTurnChanged);
    }

    private void OnDestroy()
    {
        if (TurnManager.Instance != null)
            TurnManager.Instance.OnTurnChanged.RemoveListener(OnTurnChanged);
    }

    private void OnTurnChanged(TurnManager.TurnState state)
    {
        if (state == TurnManager.TurnState.PlayerTurn)
        {
            _hasActed = false;
        }
    }
    public void Attack()
    {
        
        if (TurnManager.Instance.CurrentTurn != TurnManager.TurnState.PlayerTurn) return;
        if (_hasActed) return;

        _hasActed = true;

        _animator?.SetTrigger("Attack");


    }


    public void DealDamage()
    {
        if (_enemy == null)
        {
            Debug.LogError("PlayerCombat: enemy Health not found.");
            return;
        }
        CombatLogUI.OnLog?.Invoke("Player attacks for " + attackDamage);
        _enemy.TakeDamage(attackDamage);
        
        TurnManager.Instance.PlayerEndTurn();
    }

    public void Heal()
    {
        if (TurnManager.Instance.CurrentTurn != TurnManager.TurnState.PlayerTurn) return;
        if (_hasActed) return;

        _hasActed = true;

        if (_self == null)
        {
            Debug.LogError("PlayerCombat: cannot heal, Health missing.");
            return;
            
        }
        _animator?.SetTrigger("Heal");
        _self.Heal(healAmount);
        CombatLogUI.OnLog?.Invoke("Player heals for " + healAmount);
        TurnManager.Instance.PlayerEndTurn();
    }
}