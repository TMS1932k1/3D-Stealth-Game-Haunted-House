using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


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
        AudioManager.instance.PlayBgmAudio(AudioClipDataNameStrings.AMBIENT_AUDIO);
    }

    public void OnPlayerExit(Player player)
    {
        Debug.Log($"[{name}]: Player is at exit");

        player.DisableInputManager();

        AudioManager.instance.PlayBgmAudio(AudioClipDataNameStrings.ESCAPE_AUDIO);
        UIManager.instance.ShowWonUI();
    }

    public void OnPlayerCaughted(IGetCaught canCaught)
    {
        Debug.Log($"[{name}]: Player is caughted");

        canCaught.DisableInputManager();

        AudioManager.instance.PlayBgmAudio(AudioClipDataNameStrings.CAUGHT_AUDIO);
        UIManager.instance.ShowCaughtUI();
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
