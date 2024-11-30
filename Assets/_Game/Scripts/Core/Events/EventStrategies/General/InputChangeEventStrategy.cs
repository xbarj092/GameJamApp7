using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class InputChangeEventStrategy : IEventStrategy
{
    private RebindingOperation _rebindingOperation;
    private List<string> _usedKeys = new();

    private readonly string[] _directions = { "up", "down", "left", "right" };
    private readonly string[] _wasdKeys = { "w", "s", "a", "d" };

    public void ApplyEvent()
    {
        _usedKeys.Clear();
        InputAction action = EventManager.Instance.MovementAction.action;
        List<string> keys = new();

        for (int i = 0; i < _directions.Length; i++)
        {
            string randomKey = GetRandomKey();

            int bindingIndex = FindBindingIndex(action, _directions[i]);
            if (bindingIndex == -1)
            {
                continue;
            }

            if (!action.enabled)
            {
                action.Enable();
            }

            _usedKeys.Add(randomKey);
            keys.Add(randomKey);
            action.ApplyBindingOverride(bindingIndex, $"<Keyboard>/{randomKey}");
        }

        EventManager.Instance.OnInputChangeInvoke(keys);
    }

    private int FindBindingIndex(InputAction action, string compositePart)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (action.bindings[i].isPartOfComposite && action.bindings[i].name == compositePart)
            {
                return i;
            }
        }

        return -1;
    }

    private string GetRandomKey()
    {
        ReadOnlyArray<KeyControl> keys = Keyboard.current.allKeys;
        List<string> validKeys = new();
        foreach (KeyControl key in keys)
        {
            string keyName = key.name;
            if (IsAlphabeticKey(keyName) || IsNumericKey(keyName) && !_usedKeys.Contains(keyName))
            {
                validKeys.Add(keyName);
            }
        }

        int randomIndex = Random.Range(0, validKeys.Count);
        return validKeys[randomIndex];
    }

    private bool IsAlphabeticKey(string keyName)
    {
        return keyName.Length == 1 && char.IsLetter(keyName[0]);
    }

    private bool IsNumericKey(string keyName)
    {
        return keyName.Length == 1 && char.IsDigit(keyName[0]);
    }

    public void StopEvent()
    {
        List<string> keys = new(); 
        _usedKeys.Clear();
        InputAction action = EventManager.Instance.MovementAction.action;

        for (int i = 0; i < _directions.Length; i++)
        {
            int bindingIndex = FindBindingIndex(action, _directions[i]);
            if (bindingIndex == -1)
            {
                continue;
            }

            if (!action.enabled)
            {
                action.Enable();
            }

            _usedKeys.Add(_wasdKeys[i]);
            keys.Add(_wasdKeys[i]);
            action.ApplyBindingOverride(bindingIndex, $"<Keyboard>/{_wasdKeys[i]}");
        }

        EventManager.Instance.OnInputChangeInvoke(keys);
    }
}
