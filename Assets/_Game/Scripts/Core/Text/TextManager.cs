using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextManager : MonoSingleton<TextManager>
{
    [SerializeField] private TextFieldShower _textFieldPrefab;
    [SerializeField] private TextFieldShower _textFieldDeathPrefab;
    [SerializeField] private TextFieldShower _textFieldWinPrefab;
    [SerializeField] private TextFieldShower _textFieldMenuPrefab;

    [SerializeField] private List<StringStorage> _stringStorage;
    
    private List<StringStorage> _playedStrings = new();

    public TextFieldShower CurrentText;

    public void ShowText(StringStorageType stringStorageType, bool death = false, bool win = false, bool backToMenu = false)
    {
        DestroyOldText();

        TextFieldShower textField = _textFieldPrefab;
        if (death)
        {
            textField = _textFieldDeathPrefab;
        }
        else if (win)
        {
            textField = _textFieldWinPrefab;
        }
        else if (backToMenu)
        {
            textField = _textFieldMenuPrefab;
        }

        CurrentText = Instantiate(textField, ScreenManager.Instance.GetActiveCanvasTransform());
        StringStorage relevantStringStorage = _stringStorage.First(storage => storage.StringStorageType == stringStorageType);
        CurrentText.InitTextField(!_playedStrings.Contains(relevantStringStorage) ? relevantStringStorage.FirstTimeStrings : relevantStringStorage.NextTimeStrings);

        if (!_playedStrings.Contains(relevantStringStorage))
        {
            _playedStrings.Add(relevantStringStorage);
        }
    }

    public void DestroyOldText()
    {
        if (CurrentText != null)
        {
            Destroy(CurrentText.gameObject);
            CurrentText = null;
        }
    }

    public void ResetScript()
    {
        CurrentText = null;
        _playedStrings = new();
    }
}
