using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TMP_Text[] _buttonList;
    private string _playerSide;

    private void Awake()
    {
        SetGameControllerOnButtons();
        _playerSide = "X";
    }
    void SetGameControllerOnButtons()
    {
        for (int i = 0; i < _buttonList.Length; i++)
        {
            _buttonList[i].GetComponentInParent<GameSpace>().SetGameController(this);
        }
    }

    public string GetPlayerSide()
    {
        return _playerSide;
    }

    public void EndTurn()
    {
        //row wins
        if (_buttonList[0].text == _playerSide && _buttonList[1].text == _playerSide && _buttonList[2].text == _playerSide)
        {
            GameOver();
        }
        if (_buttonList[3].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[5].text == _playerSide)
        {
            GameOver();
        }
        if (_buttonList[6].text == _playerSide && _buttonList[7].text == _playerSide && _buttonList[8].text == _playerSide)
        {
            GameOver();
        }

        //column wins
        if (_buttonList[0].text == _playerSide && _buttonList[3].text == _playerSide && _buttonList[6].text == _playerSide)
        {
            GameOver();
        }
        if (_buttonList[1].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[7].text == _playerSide)
        {
            GameOver();
        }
        if (_buttonList[2].text == _playerSide && _buttonList[5].text == _playerSide && _buttonList[8].text == _playerSide)
        {
            GameOver();
        }

        //diagnol wins
        if (_buttonList[0].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[8].text == _playerSide)
        {
            GameOver();
        }
        if (_buttonList[2].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[6].text == _playerSide)
        {
            GameOver();
        }

        ChangeSide();
    }

    void GameOver()
    {
        for (int i=0; i < _buttonList.Length; i++)
        {
            _buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
    }

    void ChangeSide()
    {
        _playerSide = (_playerSide == "X") ? "O" : "X";
    }
}
