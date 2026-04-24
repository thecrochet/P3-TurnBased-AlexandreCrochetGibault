using UnityEngine;


public class EnemyCombat : MonoBehaviour

// Simple rule-based AI
// - If health < 30 : Defend (reduce incoming damage)
// - Otherwise : attack
{
    [SerializeField] private int attackDamage = 15;
    [SerializeField] private int healAmount = 10;
    [SerializeField] private Health _player;

    
    private Health _self;
    private Animator _animator;
    public float actionDuration = 1.5f;
    private void Awake()
    {
        
        _self = GetComponent<Health>();
        TryGetComponent(out _animator);
    }

    public void TakeTurn()
    {
        if (TurnManager.Instance.CurrentTurn != TurnManager.TurnState.EnemyTurn)
            return;

        if (_self.CurrentHealth < 30)
        {
            Defend();
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (TurnManager.Instance.CurrentTurn != TurnManager.TurnState.EnemyTurn)
            return;

        CombatLogUI.OnLog?.Invoke("Enemy attacks for " + attackDamage);

        if (_animator != null)
            _animator.SetTrigger("Attack");

        
    }

    private void DealDamage()
    {
        _player.TakeDamage(attackDamage);
    }

    private void Defend()
    {
        CombatLogUI.OnLog?.Invoke("Enemy defends (next damage reduced)");
        _self.SetDefending(true);
       
        _animator?.SetTrigger("Defend");
    }
}