
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour
{
    
    [SerializeField] private GameObject playerButtonsContainer;


    private void Start()
    {
        Debug.Log("TurnUI subscribed");

        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnTurnChanged.AddListener(OnTurnChanged);
            // initialize UI to current state immediately
            OnTurnChanged(TurnManager.Instance.CurrentTurn);

        }


    }

    private void OnDisable()
    {
        if (TurnManager.Instance != null)
            TurnManager.Instance.OnTurnChanged.RemoveListener(OnTurnChanged);
    }

    private void OnTurnChanged(TurnManager.TurnState state)
    {
        bool isPlayerTurn = state == TurnManager.TurnState.PlayerTurn;

        Debug.Log("STATE: " + state);

        if (playerButtonsContainer != null)
        {
          
            playerButtonsContainer.SetActive(isPlayerTurn);
            return;
        }

  
    }
}