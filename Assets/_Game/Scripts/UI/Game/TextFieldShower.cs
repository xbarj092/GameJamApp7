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

    private string _writer;
    private bool _isTyping = false;
    private bool _isTextComplete = false;

    private List<string> _strings;
    private int _currentTextIndex = 0;

    public void InitTextField(List<string> strings)
    {
        _aiImage.sprite = DetermineAISprite(GameManager.Instance.CurrentLevel);
        _currentTextIndex = 0;
        _strings = strings;
        SetText();
    }

    private Sprite DetermineAISprite(int currentLevel)
    {
        if (currentLevel > 7)
        {
            return _aiMadSprite;
        }
        else if (currentLevel > 2)
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
        StartCoroutine(TypeWriterTMP());
    }

    private IEnumerator TypeWriterTMP()
    {
        yield return new WaitForSeconds(_delayBeforeStart);

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
                    Destroy(gameObject);
                    return;
                }

                SetText();
            }
        }
    }
}
