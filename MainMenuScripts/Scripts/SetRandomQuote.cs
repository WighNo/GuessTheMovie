using System;
using TMPro;
using UnityEngine;

public class SetRandomQuote : MonoBehaviour
{
    [SerializeField] private QuoteData[] _quoteDatas;
    private TextMeshProUGUI _textMeshProUGUI;

    private FastRandom _random = new FastRandom();

    private void Awake()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetQuoteContent(_quoteDatas[_random.Range(0, _quoteDatas.Length)]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SetQuoteContent(_quoteDatas[_random.Range(0, _quoteDatas.Length)]);
    }
    
    private void SetQuoteContent(QuoteData quoteData)
    {
        _textMeshProUGUI.text = quoteData.ContentOfTheQuote();
    }


}
