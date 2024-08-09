using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Loading : UI_Scene
{
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private float _interval = 1f;
    private string _message = @"Initializing Managers";
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
            _loadingText.text = $"{_fixStrings[i]}{_message}{_fixStrings[i]}";
            i = (i + 1) % 3;
            yield return wait;
        }
    }

    public void SetMessage(string message)
    {
        _message = message;
    }
}