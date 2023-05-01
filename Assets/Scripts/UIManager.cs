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

    // Start is called before the first frame update
    void Start()
    {
        SetUIState(UIState.Title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUIState(UIState uIState)
    {
        titleUI.SetActive(uIState == UIState.Title);
        //print(titleUI.gameObject.activeInHierarchy);
        tutorialUI.SetActive(uIState == UIState.Pause);
        inGameUI.SetActive(uIState == UIState.Play);
        pauseUI.SetActive(uIState == UIState.Pause || uIState == UIState.Title);
        
    }

    public void PlayGame()
    {
        SetUIState(UIState.Play);
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

    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

}
