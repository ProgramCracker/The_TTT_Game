using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class NormalGameController : MonoBehaviour
{
    [Header("Game Info")]
    [SerializeField] int _turn = 1;
    [SerializeField] bool _AI1 = false;
    [SerializeField] bool _AI2 = false;
    [SerializeField] float _AIdelay;

    public enum AILevel { VeryEasy, Easy, Medium, Impossible }

    [SerializeField] AILevel _aiLevel;
    [SerializeField] int[] _aiDepth;

    [Header("Space Info")]
    [SerializeField] int[] _gridSpaces;

    [Header("Space Display")]
    [SerializeField] TextMeshProUGUI[] _gridText;
    [SerializeField] string[] _gridDisplayText;
    [SerializeField] Color[] _gridDisplayColor;

    [Header("Game State UI")]
    [SerializeField] TextMeshProUGUI _gameStateText;
    [SerializeField] GameObject _playAgainButton;
    [SerializeField] Player _playerX;
    [SerializeField] Player _playerO;
    [SerializeField] PlayerColor _activePlayerColor;
    [SerializeField] PlayerColor _inactivePlayerColor;

    void Start()
    {
        ResetGameState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void ResetGameState()
    {
        _gridSpaces = new int[9];

        _turn = 1;
        UpdateUI();

        SetBoardInteractable(true);

        _playAgainButton.SetActive(false);

        StartCoroutine(AIMove());
    }
    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < _gridText.Length; i++)
        {
            _gridText[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < _gridSpaces.Length; i++)
        {
            _gridText[i].text = _gridDisplayText[_gridSpaces[i]];
            _gridText[i].color = _gridDisplayColor[_gridSpaces[i]];
            
        }

        _gameStateText.text = "Player " + _turn + "'s Turn";
    }

    public void SpaceClicked(int spaceClicked)
    {
        SetPlayerColors(_playerO, _playerX);
        if (_turn != -1 &&
            ((_turn == 1 && !_AI1) || (_turn == 2 && !_AI2)))
        {
            
            if (_gridSpaces[spaceClicked] == 0)
                MakeMove(spaceClicked);

        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer._panel.color = _activePlayerColor._panelColor;
        newPlayer._text.color = _activePlayerColor._textColor;
        oldPlayer._panel.color = _inactivePlayerColor._panelColor;
        oldPlayer._text.color = _inactivePlayerColor._textColor;

    }

    IEnumerator AIMove()
    {
        yield return new WaitForSeconds(_AIdelay);

        if ((_AI1 && _turn == 1) || (_AI2 && _turn == 2))
        {
            int aiDepth = _aiDepth[(int)_aiLevel];
            minimax(_gridSpaces, aiDepth, true, aiDepth);
        }
    }

    int minimax(int[] currentSpaces, int aiDepth, bool maximizingPlayer, int initialDepth)
    {
        SetPlayerColors(_playerX, _playerO);
        int gameOver = CheckWin(currentSpaces);
        if (aiDepth == 0 || gameOver != -1)
        {
            if (gameOver == _turn)
            {
                return 1;
            }
            else if (gameOver != _turn && gameOver > 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        if (maximizingPlayer)
        {
            int maxEval = -10000;
            List<int> possibleMoves = GetPossibleMoves(currentSpaces);

            List<int> bestMove = new List<int>();
            for (int i = 0; i < possibleMoves.Count; i++)
            {
                int[] newSpaces = new int[9];
                for (int space = 0; space < 9; space++)
                {
                    newSpaces[space] = currentSpaces[space];
                }
                newSpaces[possibleMoves[i]] = _turn;

                int eval = minimax(newSpaces, aiDepth - 1, false, initialDepth);
                if (initialDepth == aiDepth)
                {
                    if (eval > maxEval)
                    {
                        bestMove.Clear();
                        bestMove.Add(i);
                    }
                    else if (eval == maxEval)
                    {
                        bestMove.Add(i);
                    }
                }
                maxEval = Mathf.Max(maxEval, eval);
            }

            if (initialDepth == aiDepth)
            {
                int moveChosen = bestMove[Random.Range(0, bestMove.Count)];
                MakeMove(possibleMoves[moveChosen]);
            }

            return maxEval;
        }
        else
        {
            int minEval = 10000;
            List<int> possibleMoves = GetPossibleMoves(currentSpaces);

            for (int i = 0; i < possibleMoves.Count; i++)
            {
                int[] newSpaces = new int[9];
                for (int space = 0; space < 9; space++)
                {
                    newSpaces[space] = currentSpaces[space];
                }
                newSpaces[possibleMoves[i]] = _turn == 1 ? 2 : 1;

                int eval = minimax(newSpaces, aiDepth - 1, true, initialDepth);
                minEval = Mathf.Min(minEval, eval);
            }

            return minEval;
        }
    }

    void MakeMove(int spaceToMove)
    {
        _gridSpaces[spaceToMove] = _turn;
        _turn = _turn == 1 ? 2 : 1;
        UpdateUI();

        CheckEndGame();

        StartCoroutine(AIMove());
        
        _gridText[spaceToMove].GetComponentInParent<Button>().interactable = false;
    }

    List<int> GetPossibleMoves(int[] spacesToCheck)
    {
        List<int> possibleMoves = new List<int>();
        for (int i = 0; i < spacesToCheck.Length; i++)
        {
            if (spacesToCheck[i] == 0)
                possibleMoves.Add(i);
        }

        return possibleMoves;
    }

    void Player1Wins()
    {
        _gameStateText.text = "Player 1 Wins!";
    }

    void Player2Wins()
    {
        _gameStateText.text = "Player 2 Wins!";
    }

    void CheckEndGame()
    {
        int win = CheckWin(_gridSpaces);

        if (win == 0)
            Tie();
        else if (win == 1)
            Player1Wins();
        else if (win == 2)
            Player2Wins();

        if (win != -1)
            EndGame();
    }

    void Tie()
    {
        _gameStateText.text = "Tie!";
    }

    void EndGame()
    {
        _turn = -1;

        _playAgainButton.SetActive(true);
    }

    int CheckWin(int[] spacesToCheck)
    {
        List<int> spaceNum = new List<int>();

        // Rows
        int rowStart = 0;
        for (int row = 0; row < 3; row++)
        {
            if (spacesToCheck[rowStart] == spacesToCheck[rowStart + 1] && spacesToCheck[rowStart + 1] == spacesToCheck[rowStart + 2])
            {
                if (spacesToCheck[rowStart] != 0)
                    spaceNum.Add(spacesToCheck[rowStart]);
            }

            rowStart += 3;
        }

        // Columns
        int columnStart = 0;
        for (int column = 0; column < 3; column++)
        {
            if (spacesToCheck[columnStart] == spacesToCheck[columnStart + 3] && spacesToCheck[columnStart + 3] == spacesToCheck[columnStart + 6])
            {
                if (spacesToCheck[columnStart] != 0)
                    spaceNum.Add(spacesToCheck[columnStart]);
            }

            columnStart++;
        }

        // Diagonal Up
        if (spacesToCheck[0] == spacesToCheck[4] && spacesToCheck[4] == spacesToCheck[8])
        {
            if (spacesToCheck[0] != 0)
                spaceNum.Add(spacesToCheck[0]);
        }

        // Diagonal Down
        if (spacesToCheck[6] == spacesToCheck[4] && spacesToCheck[4] == spacesToCheck[2])
        {
            if (spacesToCheck[6] != 0)
                spaceNum.Add(spacesToCheck[6]);
        }

        if (spaceNum.Count > 0)
        {
            for (int i = 0; i < spaceNum.Count; i++)
            {
                if (spaceNum[i] == 1)
                {
                    return 1;
                }
                else if (spaceNum[i] == 2)
                {
                    return 2;
                }
            }
        }
        else
        {
            int freeSpaces = 0;
            for (int i = 0; i < _gridSpaces.Length; i++)
            {
                if (spacesToCheck[i] == 0)
                    freeSpaces++;
            }

            if (freeSpaces == 0)
                return 0;
            else
                return -1;
        }

        return -1;
    }

    public void PlayAgain()
    {
        ResetGameState();
    }
}
