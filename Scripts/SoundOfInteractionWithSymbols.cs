using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundOfInteractionWithSymbols : MonoBehaviour
{
    private CheckerGuessByFrame _checkerGuessByFrame;
    private AudioSource _audioSource;

    [Space(10f)]
    [SerializeField] private AudioClip _addSymbolSound;
    [SerializeField] private AudioClip _removeSymbolSound;
    
    private void Awake()
    {
        _checkerGuessByFrame = FindObjectOfType<CheckerGuessByFrame>().GetComponent<CheckerGuessByFrame>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _checkerGuessByFrame.AddSymbol.AddListener(AddSymbol);
        _checkerGuessByFrame.RemoveSymbol.AddListener(RemoveSymbol);
    }

    private void AddSymbol()
    {
        _audioSource.clip = _addSymbolSound;
        _audioSource.Play();
    }

    private void RemoveSymbol()
    {
        _audioSource.clip = _removeSymbolSound;
        _audioSource.Play();
    }

    private void OnDestroy()
    {
        _checkerGuessByFrame.AddSymbol.RemoveListener(AddSymbol);
        _checkerGuessByFrame.RemoveSymbol.RemoveListener(RemoveSymbol);
    }
}
