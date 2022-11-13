using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpace : MonoBehaviour
{
    public Button _button;
    public TMP_Text _buttonText;
    public string _playerSide;

    public void SetSpace()
    {
        _buttonText.text = _playerSide;
        _button.interactable = false;
    }
}
