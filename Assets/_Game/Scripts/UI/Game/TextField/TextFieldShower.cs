using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class TextFieldShower : MonoBehaviour
{
    [SerializeField] private Image _aiImage;
    [SerializeField] private Sprite _aiCalmSprite;
    [SerializeField] private Sprite _aiMidSprite;
    [SerializeField] private Sprite _aiMadSprite;

    [Space(5)]
    [SerializeField] private TMP_Text _tmpProText;
    [SerializeField] private float _delayBeforeStart = 0f;
    [SerializeField] private float _timeBtwChars = 0.1f;
    [SerializeField] private GameObject _clickToContinue;

    private string _writer;
    private bool _isTyping = false;
    private bool _isTextComplete = false;

    private List<string> _strings;
    private int _currentTextIndex = 0;

    public event Action OnTextFinished;

    private void OnDisable()
    {
        AudioManager.Instance.Stop(SoundType.TextTypeSFX);
    }

    public void InitTextField(List<string> strings)
    {
        if (strings == null || strings.Count == 0)
        {
            Finish();
        }

        _aiImage.sprite = DetermineAISprite(GameManager.Instance.CurrentLevel);
        _currentTextIndex = 0;
        _strings = strings;
        SetText();
    }

    private Sprite DetermineAISprite(int currentLevel)
    {
        if (currentLevel > 4)
        {
            return _aiMadSprite;
        }
        else if (currentLevel > 1)
        {
            return _aiMidSprite;
        }
        else
        {
            return _aiCalmSprite;
        }
    }

    private void SetText()
    {
        _writer = _strings[_currentTextIndex];
        _tmpProText.text = "";
        _isTyping = true;
        _isTextComplete = false;
        _clickToContinue.SetActive(false);
        StartCoroutine(TypeWriterTMP());
    }

    private IEnumerator TypeWriterTMP()
    {
        yield return new WaitForSeconds(_delayBeforeStart);

        AudioManager.Instance.Play(SoundType.TextTypeSFX);

        foreach (char character in _writer)
        {
            if (!_isTyping)
            {
                _tmpProText.text = _writer;
                break;
            }

            _tmpProText.text += character;
            yield return new WaitForSeconds(_timeBtwChars);
        }

        AudioManager.Instance.Stop(SoundType.TextTypeSFX);

        _clickToContinue.SetActive(true);
        _isTyping = false;
        _isTextComplete = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isTyping)
            {
                _isTyping = false;
            }
            else if (_isTextComplete)
            {
                _currentTextIndex++;
                if (_currentTextIndex >= _strings.Count)
                {
                    Finish();
                    return;
                }

                SetText();
            }
        }
    }

    private void Finish()
    {
        OnTextFinished?.Invoke();
    }
}
