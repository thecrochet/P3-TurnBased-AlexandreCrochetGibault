using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatLogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    private string _log = "";

    public static System.Action<string> OnLog;

    private void Awake()
    {
        if (logText == null)
            logText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        OnLog += AddLog;
    }

    private void OnDisable()
    {
        OnLog -= AddLog;
    }

    private Queue<string> _logs = new Queue<string>();

    public void AddLog(string message)
    {
        _logs.Enqueue(message);

        if (_logs.Count > 3)
            _logs.Dequeue();

        logText.text = string.Join("\n", _logs);
    }
}