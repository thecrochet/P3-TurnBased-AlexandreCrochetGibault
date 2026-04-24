
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public UnityEvent<int> OnHealthChanged = new UnityEvent<int>();
    public UnityEvent OnDeath = new UnityEvent();

    private int _currentHealth;
    private bool _isDead;
    private bool _isDefending;


    private Animator _animator;

    private void Awake()
    {
        TryGetComponent(out _animator);
        _currentHealth = maxHealth;
        _isDead = false;
        if (OnHealthChanged == null) OnHealthChanged = new UnityEvent<int>();
        if (OnDeath == null) OnDeath = new UnityEvent();
    }

    public void TakeDamage(int amount)
    {
        if (_isDead) return;

        int finalDamage = amount;

        if (_isDefending)
        {
            finalDamage = amount / 2;
            CombatLogUI.OnLog?.Invoke($"but {gameObject.name} is defending! Damage will be halved.");
            _isDefending = false; // reset AFTER hit
        }


        _currentHealth = Mathf.Max(0, _currentHealth - finalDamage);

        OnHealthChanged?.Invoke(_currentHealth);

        CombatLogUI.OnLog?.Invoke($"{gameObject.name} took {finalDamage} damage");

        if (_currentHealth == 0)
        {
            _isDead = true;
            OnDeath?.Invoke();
            TurnManager.Instance.EndGame();
        }
    

    _animator?.SetTrigger("Hit");

    }

    public void Heal(int amount)
    {
        if (_isDead) return;

        _currentHealth = Mathf.Min(maxHealth, _currentHealth + amount);
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void SetDefending(bool value)
    {
        _isDefending = value;
    }

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => maxHealth;
}