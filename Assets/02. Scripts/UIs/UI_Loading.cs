using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI_Loading : UI_Scene
{
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private float _interval = 1f;
    private List<(string message, int priority)> _messageQueue = new();
    private int i = 0;
    private readonly string[] _fixStrings = new string[]
    {
        @" . ",
        @" . . ",
        @" . . . ",
    };

    private IEnumerator Start()
    {
        WaitForSecondsRealtime wait = new(_interval);

        while (true)
        {
            _loadingText.text = $"{_fixStrings[i]}{GetMeesage()}{_fixStrings[i]}";
            i = (i + 1) % 3;
            yield return wait;
        }
    }

    public void SetMessage(string message, int priority)
    {
        _messageQueue.Add((message, priority));
        _messageQueue = _messageQueue.OrderBy(x => x.priority).ToList();
        _loadingText.text = $"{_fixStrings[i]}{GetMeesage()}{_fixStrings[i]}";
    }

    public void RemoveMessage(string message)
    {
        _messageQueue.RemoveAll(x => x.message == message);
        _loadingText.text = $"{_fixStrings[i]}{GetMeesage()}{_fixStrings[i]}";
    }

    public string GetMeesage()
    {
        string res = "";
        if (_messageQueue.Count > 0)
            res = _messageQueue.Last().message;
        return res;
    }
}