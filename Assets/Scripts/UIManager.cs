using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIState { Title, Play, Pause }

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; } = null;

    #region Variables
    public GameObject titleUI;
    public GameObject tutorialUI;
    public GameObject pauseUI;
    public GameObject inGameUI;

    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetUIState(UIState uIState)
    {
        pauseUI.SetActive(false);
        titleUI.SetActive(false);
        tutorialUI.SetActive(false);
        inGameUI.SetActive(false);
        switch (uIState)
        {
            case UIState.Title:
                pauseUI.SetActive(true);
                titleUI.SetActive(true);
                StartCoroutine("GoToTutorial");
                break;
            case UIState.Play:
                inGameUI.SetActive(true);
                break;
            case UIState.Pause:
                pauseUI.SetActive(true);
                tutorialUI.SetActive(true);
                break;
            default:
                inGameUI.SetActive(true);
                break;
        }
    }

    IEnumerator GoToTutorial()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.SetGameState(GameState.Pause);
    }

    public void PlayGame()
    {
        GameManager.instance.SetGameState(GameState.Play);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    #region QuitGame
    public void QuitGame()
    {

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

    }

    #endregion
}
