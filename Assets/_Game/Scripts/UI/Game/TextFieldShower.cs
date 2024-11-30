using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextFieldShower : MonoBehaviour
{
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
        _currentTextIndex = 0;
        _strings = strings;
        SetText();
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
