using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button _StartGame;
    [SerializeField] Button _StopGame;

    // Start is called before the first frame update
    void Start()
    {
        _StartGame.onClick.AddListener(StartNewGame);
        _StopGame.onClick.AddListener(QuitGame);

    }

    private void StartNewGame()
    {
        ScenesManager.Instance.LoadNextScene();
    }
    
    private void QuitGame()
    {
        Application.Quit();
    }
}
