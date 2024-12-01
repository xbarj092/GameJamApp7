using System;
using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            TextManager.Instance.ShowText(StringStorageType.End);
            TextManager.Instance.CurrentText.OnTextFinished += OnTextFinished;
        }
    }

    private void OnTextFinished()
    {
        TextManager.Instance.CurrentText.OnTextFinished -= OnTextFinished;
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Win);
    }
}
