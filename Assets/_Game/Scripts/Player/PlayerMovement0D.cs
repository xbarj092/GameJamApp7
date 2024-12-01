using System;
using UnityEngine;

public class PlayerMovement0D : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(EndGame), 2f);
    }

    private void EndGame()
    {
        TextManager.Instance.ShowText(StringStorageType.ZeroDimension);
        TextManager.Instance.CurrentText.OnTextFinished += OnTextFinished;
    }

    private void OnTextFinished()
    {
        TextManager.Instance.CurrentText.OnTextFinished -= OnTextFinished;
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Death);
    }
}
