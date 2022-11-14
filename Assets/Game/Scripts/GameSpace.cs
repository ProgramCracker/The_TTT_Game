using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpace : MonoBehaviour
{
    public Button _button;
    public TMP_Text _buttonText;
    

    private GameController _gameController;

    public void SetSpace()
    {
        if (_gameController._playerMove == true)
        {
            _buttonText.text = _gameController.GetPlayerSide();
            _button.interactable = false;
            _gameController.EndTurn();
        }

    }

    public void SetGameController(GameController controller)
    {
        _gameController = controller;
    }
}
