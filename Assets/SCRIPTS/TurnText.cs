
using UnityEngine;
using TMPro;

public class TurnText : MonoBehaviour
{
    [SerializeField] private TMP_Text turnText;
    private bool _subscribed;

    private void Awake()
    {
        if (turnText == null)
            turnText = GetComponent<TMP_Text>() ?? GetComponentInChildren<TMP_Text>();

        else
            Debug.LogError("TurnText: No TMP_Text found.");
    }

    private void Start()
    {
        TrySubscribe();
    }

    private void OnEnable()
    {
        TrySubscribe();
    }

    private void OnDisable()
    {
        if (_subscribed && TurnManager.Instance != null)
        {
            TurnManager.Instance.OnTurnChanged.RemoveListener(OnTurnChanged);
            _subscribed = false;
        }
    }

    private void TrySubscribe()
    {
        if (_subscribed) return;
        if (TurnManager.Instance == null) return;

        TurnManager.Instance.OnTurnChanged.AddListener(OnTurnChanged);
        _subscribed = true;

        // initialize display with current state
        OnTurnChanged(TurnManager.Instance.CurrentTurn);
    }

    private void OnTurnChanged(TurnManager.TurnState state)
    {

        Debug.Log("TurnText subscribed, current state: " + TurnManager.Instance.CurrentTurn);
        if (turnText == null) return;

        switch (state)
        {
            case TurnManager.TurnState.PlayerTurn:
                turnText.text = "Player Turn";
                turnText.gameObject.SetActive(true);
                break;

            case TurnManager.TurnState.EnemyTurn:
                turnText.text = "Enemy Turn";
                turnText.gameObject.SetActive(true);
                break;

            case TurnManager.TurnState.GameOver:
                turnText.text = "Game Over";
                turnText.gameObject.SetActive(true);
                break;
        }
    }
}