using System;
using TMPro;
using UnityEngine;

public class AnswerSymbol : MonoBehaviour
{
    public static event Action<int> RemoveSymbol;
    public int CharIndex { get; set; }
    public TextMeshProUGUI Text { get; private set; }

    private void OnEnable()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Text.text = null;
    }

    public void RemoveSymbolInCurrentAnswer() => RemoveSymbol?.Invoke(CharIndex);

}
