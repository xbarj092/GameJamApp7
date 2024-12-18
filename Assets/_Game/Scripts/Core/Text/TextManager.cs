using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextManager : MonoSingleton<TextManager>
{
    [SerializeField] private TextFieldShower _textFieldPrefab;
    [SerializeField] private TextFieldShower _textFieldPrefabGood;

    [SerializeField] private List<StringStorage> _stringStorage;

    private List<StringStorage> _playedStrings = new();
    private Queue<(StringStorageType, bool)> _textQueue = new();
    private bool _isShowingText = false;

    public TextFieldShower CurrentText;
    public StringStorageType CurrentTextType;
    public bool Good;

    public void ShowText(StringStorageType stringStorageType, bool good = false)
    {
        _textQueue.Enqueue((stringStorageType, good));

        if (!_isShowingText)
        {
            ProcessNextText();
        }
    }

    private void ProcessNextText()
    {
        if (_textQueue.Count == 0)
        {
            _isShowingText = false;
            return;
        }

        _isShowingText = true;

        (StringStorageType stringStorageType, bool good) = _textQueue.Dequeue();

        CurrentText = Instantiate(good ? _textFieldPrefabGood : _textFieldPrefab, ScreenManager.Instance.GetActiveCanvasTransform());
        StringStorage relevantStringStorage = _stringStorage.First(storage => storage.StringStorageType == stringStorageType);
        CurrentTextType = relevantStringStorage.StringStorageType;
        Good = good;
        CurrentText.InitTextField(!_playedStrings.Contains(relevantStringStorage) ? relevantStringStorage.FirstTimeStrings : relevantStringStorage.NextTimeStrings);

        CurrentText.OnTextFinished += HandleTextFinished;

        if (!_playedStrings.Contains(relevantStringStorage))
        {
            _playedStrings.Add(relevantStringStorage);
        }
    }

    private void HandleTextFinished()
    {
        StartCoroutine(HandleTextFinishedRoutine());
    }

    private IEnumerator HandleTextFinishedRoutine()
    {
        yield return StartCoroutine(DestroyOldText());
        ProcessNextText();
    }

    public IEnumerator DestroyOldText()
    {
        yield return null;
        if (CurrentText != null)
        {
            CurrentText.OnTextFinished -= HandleTextFinished;
            Destroy(CurrentText.gameObject, 0.01f);
            CurrentText = null;
            CurrentTextType = StringStorageType.None;
        }
    }

    public bool HasPlayedTutorial(StringStorageType stringStorageType)
    {
        return _playedStrings.Any(storage => storage.StringStorageType == stringStorageType);
    }

    public void ResetScript()
    {
        StopAllCoroutines();
        DestroyOldText();
        _playedStrings = new();
        _textQueue.Clear();
        _isShowingText = false;
    }

    public void HandleCanvasSwitch()
    {
        if (CurrentTextType != StringStorageType.None)
        {
            StringStorage relevantStringStorage = _playedStrings.FirstOrDefault(storage => storage.StringStorageType == CurrentTextType);
            if (relevantStringStorage != null)
            {
                _playedStrings.Remove(relevantStringStorage);
            }

            _textQueue.Enqueue((CurrentTextType, Good));
            CurrentTextType = StringStorageType.None;

            StopAllCoroutines();
            _isShowingText = false;
            ProcessNextText();
        }
    }
}