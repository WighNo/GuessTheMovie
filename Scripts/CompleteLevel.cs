using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private GameObject[] _words;

    [SerializeField] private List<Vector3> _startPositions = new List<Vector3>();
    [SerializeField] private Transform[] _endPositions;
    
    [Space(10f)]
    [SerializeField] private Continue _continue;
    
    private void Awake()
    {
        SetCanvas();
        SetStartPositions();
        
        DateLoader.Instance.LaodNextLevel.AddListener(LoadNextDate);
        CheckerGuessByFrame.Instance.CompleteLevel.AddListener(Win);
        
        _continue.Init(DateLoader.Instance.DateId);

        gameObject.SetActive(false);
    }

    private void SetCanvas()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }

    private void SetStartPositions()
    {
        _startPositions.Add(_words[0].GetComponent<Transform>().position);
        _startPositions.Add(_words[1].GetComponent<Transform>().position);
    }
    
    private void Win()
    {
        gameObject.SetActive(true);
        
        _continue.Check(DateLoader.Instance.DateId);
        
        _canvasGroup.alpha = 1f;
        LeanTween.move(_words[0], _endPositions[0], 0.25f);
        LeanTween.move(_words[1], _endPositions[1], 0.25f);
    }

    private void LoadNextDate()
    {
        _canvasGroup.alpha = 0f;

        _words[0].transform.position = _startPositions[0];
        _words[1].transform.position = _startPositions[1];

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        CheckerGuessByFrame.Instance.CompleteLevel.RemoveListener(Win);
        DateLoader.Instance.LaodNextLevel.RemoveListener(LoadNextDate);
        _continue.Unsubscribe();
    }
    
    [Serializable]
    private class Continue
    {
        [SerializeField] private Button _button;
        private TextMeshProUGUI _buttonText;

        private string _defaultContent = "Далее";
        private string _finishLevelContent = "Завершить";

        public void Init(int currentLevel)
        {
            _buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();
            
            if(currentLevel >= DateLoader.MaximumLevel)
                FinishGame();
            else 
                Reset();
        }

        public void Check(int currentLevel)
        {
            if(currentLevel >= DateLoader.MaximumLevel)
                FinishGame();
        }

        private void FinishGame()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(LoadMainMenuScene);

            _buttonText.text = _finishLevelContent;
        }

        private void Reset()
        {
            _buttonText.text = _defaultContent;
            _button.onClick.AddListener(DateLoader.Instance.NextDate);
        }

        private void LoadMainMenuScene()
        {
            SceneManager.LoadScene(0);
        }

        public void Unsubscribe()
        {
            _button.onClick.RemoveListener(LoadMainMenuScene);
        }
    }
}
