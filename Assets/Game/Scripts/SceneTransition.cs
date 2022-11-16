using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string _targetScene;

    GameController _gameController;

    private void Awake()
    {
        
    }

    public void ChangeScene()
    {
        Globals._difficulty = _targetScene;
        SceneManager.LoadScene(_targetScene);
        
        
    }
    
    
}
