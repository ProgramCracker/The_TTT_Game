using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image _panel;
    public TMP_Text _text;
    public Button _button;
}

[System.Serializable]
public class PlayerColor
{
    public Color _panelColor;
    public Color _textColor;
}

public static class Globals
{
    static public string _difficulty;
}

public class GameController : MonoBehaviour
{
    
    [Header("Space Info")]
    [SerializeField] TMP_Text[] _buttonList;

    [Header("Game State UI")]
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] TMP_Text _gameOverText;
    [SerializeField] GameObject _restartButton;
    [SerializeField] TextMeshProUGUI _startInfo;

    [Header("Game Info")]
    [SerializeField] Player _playerX;
    [SerializeField] Player _playerO;
    [SerializeField] PlayerColor _activePlayerColor;
    [SerializeField] PlayerColor _inactivePlayerColor;
    [SerializeField] float _cpuDelay;

    private string _playerSide;
    private string _cpuSide;
    public bool _playerMove;
    
    private int _value;

    private int _moveCount;

    private void Awake()
    {
        
        _gameOverPanel.SetActive(false);
        SetGameControllerOnButtons();
        _moveCount = 0;
        _restartButton.SetActive(false);
        Globals._difficulty = "SampleScene";

        _playerMove = true;
    }

    private void Update()
    {
        if (_playerMove == false)
        {
            _cpuDelay += _cpuDelay * Time.deltaTime;
            if(_cpuDelay >= 100)
            {
                if (Globals._difficulty == "SampleScene")
                {
                    EasyCPU();
                }
                else if (Globals._difficulty == "NormalPlay")
                {
                    NormalCPU();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void NormalCPU()
    {

    }

    void EasyCPU()
    {
        _value = Random.Range(0, 8);
        if (_buttonList[_value].GetComponentInParent<Button>().interactable == true)
        {
            _buttonList[_value].text = GetCPUSide();
            _buttonList[_value].GetComponentInParent<Button>().interactable = false;
            EndTurn();
        }
    }

    void SetGameControllerOnButtons()
    {
        for (int i = 0; i < _buttonList.Length; i++)
        {
            _buttonList[i].GetComponentInParent<GameSpace>().SetGameController(this);
        }
    }

    public void SetStartingSide(string startingSide)
    {
        _playerSide = startingSide;
        if(_playerSide == "X")
        {
            _cpuSide = "O";
            SetPlayerColors(_playerX, _playerO);
        }
        else
        {
            _cpuSide = "X";
            SetPlayerColors(_playerO, _playerX);
        }

        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        //_startInfo.SetActive(false);
    }

    public string GetPlayerSide()
    {
        return _playerSide;
    }

    public string GetCPUSide()
    {
        return _cpuSide;
    }

    public void EndTurn()
    {
        _moveCount++;

        //PLAYER WIN CONDITIONS
        //row wins
        if (_buttonList[0].text == _playerSide && _buttonList[1].text == _playerSide && _buttonList[2].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if (_buttonList[3].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[5].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if(_buttonList[6].text == _playerSide && _buttonList[7].text == _playerSide && _buttonList[8].text == _playerSide)
        {
            GameOver(_playerSide);
        }

        //column wins
        else if (_buttonList[0].text == _playerSide && _buttonList[3].text == _playerSide && _buttonList[6].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if (_buttonList[1].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[7].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if (_buttonList[2].text == _playerSide && _buttonList[5].text == _playerSide && _buttonList[8].text == _playerSide)
        {
            GameOver(_playerSide);
        }

        //diagnol wins
        else if (_buttonList[0].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[8].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if (_buttonList[2].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[6].text == _playerSide)
        {
            GameOver(_playerSide);
        }


        //CPU WIN CONDITIONS
        //row wins
        else if (_buttonList[0].text == _cpuSide && _buttonList[1].text == _cpuSide && _buttonList[2].text == _cpuSide)
        {
            GameOver(_cpuSide);
        }
        else if (_buttonList[3].text == _cpuSide && _buttonList[4].text == _cpuSide && _buttonList[5].text == _cpuSide)
        {
            GameOver(_cpuSide);
        }
        else if (_buttonList[6].text == _cpuSide && _buttonList[7].text == _cpuSide && _buttonList[8].text == _cpuSide)
        {
            GameOver(_cpuSide);
        }

        //column wins
        else if (_buttonList[0].text == _cpuSide && _buttonList[3].text == _cpuSide && _buttonList[6].text == _cpuSide)
        {
            GameOver(_cpuSide);
        }
        else if (_buttonList[1].text == _cpuSide && _buttonList[4].text == _cpuSide && _buttonList[7].text == _cpuSide)
        {
            GameOver(_cpuSide);
        }
        else if (_buttonList[2].text == _cpuSide && _buttonList[5].text == _cpuSide && _buttonList[8].text == _cpuSide)
        {
            GameOver(_cpuSide);
        }

        //diagnol wins
        else if (_buttonList[0].text == _cpuSide && _buttonList[4].text == _cpuSide && _buttonList[8].text == _cpuSide)
        {
            GameOver(_cpuSide);
        }
        else if (_buttonList[2].text == _cpuSide && _buttonList[4].text == _cpuSide && _buttonList[6].text == _cpuSide)
        {
            GameOver(_cpuSide);
        }


        else if (_moveCount >= 9)
        {
            GameOver("draw");
        }
        else
        {
            ChangeSide();
            _cpuDelay = 10;
        }


    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer._panel.color = _activePlayerColor._panelColor;
        newPlayer._text.color = _activePlayerColor._textColor;
        oldPlayer._panel.color = _inactivePlayerColor._panelColor;
        oldPlayer._text.color = _inactivePlayerColor._textColor;

    }

    void GameOver(string winner)
    {
        SetBoardInteractable(false);
        
        if (winner == "draw")
        {
            SetGameOverText("It's a Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winner + " Wins!");
        }

        _restartButton.SetActive(true);
    }

    void ChangeSide()
    {
        //_playerSide = (_playerSide == "X") ? "O" : "X";
        _playerMove = (_playerMove == true) ? false : true;

        //if (_playerSide == "X")
        if (_playerMove == true)
        {
            SetPlayerColors(_playerX, _playerO);
            
            SetGameOverText(_playerSide + "'s Turn");
        }
        else
        {
            SetPlayerColors(_playerO, _playerX);
            
            SetGameOverText( _cpuSide + "'s Turn");
        }
    }

    void SetGameOverText(string message)
    {
        //_gameOverPanel.SetActive(true);
        //_gameOverText.text = message;
        _startInfo.text = message;
    }

    public void RestartGame()
    {

        _moveCount = 0;
        _gameOverPanel.SetActive(false);
        _restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        SetGameOverText("X or O" + System.Environment.NewLine + "Pick a Side");
        //_startInfo.SetActive(true);
        _playerMove = true;
        _cpuDelay = 10;


        for (int i = 0; i < _buttonList.Length; i++)
        {
            _buttonList[i].text = "";
        }

    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < _buttonList.Length; i++)
        {
            _buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void SetPlayerButtons(bool toggle)
    {
        _playerX._button.interactable = toggle;
        _playerO._button.interactable = toggle;
    }

    void SetPlayerColorsInactive()
    {
        _playerX._panel.color = _inactivePlayerColor._panelColor;
        _playerX._text.color = _inactivePlayerColor._textColor;
        _playerO._panel.color = _inactivePlayerColor._panelColor;
        _playerO._text.color = _inactivePlayerColor._textColor;
    }
}
