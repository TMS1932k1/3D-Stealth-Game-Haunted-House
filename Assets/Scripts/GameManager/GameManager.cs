using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action OnPauseGame;
    public static Action OnResumeGame;

    public static GameManager instance;

    private Player activePlayer = null;
    private IActive activeTarget = null;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        AudioManager.instance.PlayBgmAudio(AudioClipDataNameStrings.MAIN_MENU_AUDIO);
    }

    public void OnPlayerExit(Player player)
    {
        Debug.Log($"[{name}]: Player is at exit");

        OnPauseGame?.Invoke();

        AudioManager.instance.PlayBgmAudio(AudioClipDataNameStrings.ESCAPE_AUDIO);
        IngameUIManager.instance.ShowWonUI();
    }

    public void OnPlayerCaughted(IGetCaught canCaught)
    {
        Debug.Log($"[{name}]: Player is caughted");

        OnPauseGame?.Invoke();

        AudioManager.instance.PlayBgmAudio(AudioClipDataNameStrings.CAUGHT_AUDIO);
        IngameUIManager.instance.ShowCaughtUI();
    }

    public void OnPlayerActive(Player player, IActive target)
    {
        activePlayer = player;
        activeTarget = target;

        activeTarget.Active(activePlayer, out bool successActive);

        if (successActive)
        {
            CameraManager.instance.StartActive();
            OnPauseGame?.Invoke();
        }
    }

    public void OnFinishActive()
    {
        activePlayer = null;
        activeTarget = null;

        CameraManager.instance.EndActive();

        OnResumeGame?.Invoke();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        AudioManager.instance.PlayBgmAudio(AudioClipDataNameStrings.AMBIENT_AUDIO);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
