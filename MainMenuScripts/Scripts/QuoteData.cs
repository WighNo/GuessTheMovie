using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuoteData", menuName = "Quote Data", order = 51)]
public class QuoteData : ScriptableObject
{
    private char _quoteSymbol = '©';

    [TextArea(2, 3)] 
    [SerializeField] private string _quote;
    
    [Space(5f)]
    [TextArea(2, 3)]
    [SerializeField] private string _quoteAuthor;

    [Space(10f)]
    [SerializeField] private Color32 _quoteAuthorColor = new Color32(195, 45, 45, 255);
    [SerializeField] private Color32 _quoteSymbolColor = new Color32(68, 68, 68, 255);

    public string ContentOfTheQuote()
    {
        string authorHex = ColorUtility.ToHtmlStringRGB(_quoteAuthorColor);
        string symbolHex = ColorUtility.ToHtmlStringRGB(_quoteSymbolColor);
        return $"{_quote}\n<color=#{symbolHex}>{_quoteSymbol}</color> <color=#{authorHex}>{_quoteAuthor}</color>";
    }
}
