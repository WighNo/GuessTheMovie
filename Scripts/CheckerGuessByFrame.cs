using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CheckerGuessByFrame : MonoBehaviour
{
    public static CheckerGuessByFrame Instance { get; private set; }
    
    [SerializeField] private char[] _correctAnswer;
    [SerializeField] private char[] _currentAnswer;

    public UnityEvent AddSymbol { get; private set; } = new UnityEvent();
    public UnityEvent RemoveSymbol { get; private set; } = new UnityEvent();

    public UnityEvent CompleteLevel { get; private set; } = new UnityEvent();

    private void Awake()
    {
        Instance = this;
        
        GuessByFrameLevelBuilder.UpdateQuestion.AddListener(InstallNewAnswer);
        GameKeyboard.Input += AddSymbolInCurrentAnswer;
        AnswerSymbol.RemoveSymbol += RemoveSymbolAtCurrentAnswer;
    }

    private void InstallNewAnswer(GuessByFrameData data)
    {
        string sourceString = data.RussianTitle;
        
        List<char> symbolsForCorrection = sourceString.ToLower().ToCharArray().ToList();
        
        symbolsForCorrection.RemoveAll(x => x == ' ');


        _correctAnswer = symbolsForCorrection.ToArray();

        _currentAnswer = new char[_correctAnswer.Length];
    }

    private void AddSymbolInCurrentAnswer(char symbol)
    {
        AddSymbol?.Invoke();
        for (int i = 0; i < _currentAnswer.Length; i++)
        {
            if (_currentAnswer[i] == default(char))
            {
                _currentAnswer[i] = symbol;
                break;
            }
        }

        if (_correctAnswer.Length == _currentAnswer.Length)
        {
            for (int i = 0; i < _correctAnswer.Length; i++)
            {
                if (_correctAnswer[i] != _currentAnswer[i])
                    return;
            }
            
            Invoke(nameof(SuccessfulLevel), 0.2f);
        }
    }

    private void SuccessfulLevel()
    {
        CompleteLevel?.Invoke();
    }

    private void RemoveSymbolAtCurrentAnswer(int charIndex)
    {
        _currentAnswer[charIndex] = default(char);
        RemoveSymbol?.Invoke();
    }

    private void OnDestroy()
    {
        GuessByFrameLevelBuilder.UpdateQuestion.RemoveListener(InstallNewAnswer);

        GameKeyboard.Input -= AddSymbolInCurrentAnswer;
        AnswerSymbol.RemoveSymbol -= RemoveSymbolAtCurrentAnswer;
    }
}
