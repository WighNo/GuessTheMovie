using UnityEngine;
using UnityEngine.UI;

public class GuessAnswerField : MonoBehaviour
{
    [SerializeField] private AnswerSymbol[] _answerSymbols;

    private void Awake()
    {
        AnswerSymbol.RemoveSymbol += DisplaySymbol;
        GameKeyboard.Input += DisplaySymbol;
        
        DateLoader.Instance.LaodNextLevel.AddListener(ResetAnswerSymbols);
    }

    private void Start()
    {
        InitAnswerSymbols();
    }

    private void InitAnswerSymbols()
    {
        _answerSymbols = GetComponentsInChildren<AnswerSymbol>();

        for (int i = 0; i < _answerSymbols.Length; i++)
        {
            _answerSymbols[i].CharIndex = i;

            if (_answerSymbols[i].TryGetComponent(out Button button))
                button.onClick.AddListener(_answerSymbols[i].RemoveSymbolInCurrentAnswer);
        }
    }

    private void DisplaySymbol(int index)
    {
        _answerSymbols[index].Text.text = null;
    }

    private void DisplaySymbol(char symbol)
    {
        for (int i = 0; i < _answerSymbols.Length; i++)
        {
            if (_answerSymbols[i].Text.text == null)
            {
                _answerSymbols[i].Text.text = symbol.ToString();
                break;
            }
        }
    }

    private void ResetAnswerSymbols()
    {
    }

    private void OnDestroy()
    {
        DateLoader.Instance.LaodNextLevel.RemoveListener(ResetAnswerSymbols);
        AnswerSymbol.RemoveSymbol -= DisplaySymbol;
        GameKeyboard.Input -= DisplaySymbol;
    }
}
