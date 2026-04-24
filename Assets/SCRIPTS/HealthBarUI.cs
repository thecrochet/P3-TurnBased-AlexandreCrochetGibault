
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Health target; 

    private void Awake()
    {
        if (slider == null) slider = GetComponentInChildren<Slider>();
       
    }

    private void OnEnable()
    {
        if (target != null)
            target.OnHealthChanged.AddListener(UpdateHealth);
    }

    private void Start()
    {
        if (slider == null) slider = GetComponentInChildren<Slider>();
        

        if (slider == null)
        {
            Debug.LogError("Slider not assigned");
            enabled = false;
            return;
        }

        if (target == null)
        {
            Debug.LogError("Health target not found.");
            enabled = false;
            return;
        }

        
        slider.maxValue = target.MaxHealth;
        slider.value = target.CurrentHealth;
    }

    private void OnDisable()
    {
        if (target != null)
            target.OnHealthChanged.RemoveListener(UpdateHealth);
    }

    public void UpdateHealth(int value)
    {
        if (slider != null) slider.value = value;
    }

    public void SetMaxHealth(int max)
    {
        if (slider != null)
        {
            slider.maxValue = max;
            slider.value = max;
        }
    }
}