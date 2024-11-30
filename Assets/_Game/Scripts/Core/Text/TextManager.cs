using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextManager : MonoSingleton<TextManager>
{
    [SerializeField] private TextFieldShower _textFieldPrefab;
    [SerializeField] private List<StringStorage> _stringStorage;
    
    private List<StringStorage> _playedStrings = new();

    public TextFieldShower CurrentText;

    public void ShowText(StringStorageType stringStorageType)
    {
        CurrentText = Instantiate(_textFieldPrefab, ScreenManager.Instance.GetActiveCanvasTransform());
        StringStorage relevantStringStorage = _stringStorage.First(storage => storage.StringStorageType == stringStorageType);
        CurrentText.InitTextField(!_playedStrings.Contains(relevantStringStorage) ? relevantStringStorage.FirstTimeStrings : relevantStringStorage.NextTimeStrings);

        if (!_playedStrings.Contains(relevantStringStorage))
        {
            _playedStrings.Add(relevantStringStorage);
        }
    }
}
