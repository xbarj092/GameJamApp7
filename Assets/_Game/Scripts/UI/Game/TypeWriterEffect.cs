using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmpProText;
    [SerializeField] private string _writer;

    [SerializeField] private float _delayBeforeStart = 0f;
    [SerializeField] private float _timeBtwChars = 0.1f;

    private void Start()
    {
        _tmpProText = GetComponent<TMP_Text>()!;

        if (_tmpProText != null)
        {
            _writer = _tmpProText.text;
            _tmpProText.text = "";

            StartCoroutine(TypeWriterTMP());
        }
    }

    private IEnumerator TypeWriterTMP()
    {
        yield return new WaitForSeconds(_delayBeforeStart);

        foreach (char c in _writer)
        {
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length);
            }
            _tmpProText.text += c;
            yield return new WaitForSeconds(_timeBtwChars);
        }
    }
}
