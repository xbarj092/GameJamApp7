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
        TextManager.Instance.ShowText(StringStorageType.ZeroDimension, true);
    }
}
