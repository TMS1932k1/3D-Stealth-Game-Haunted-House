using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action OnPauseGame;
    public static Action OnResumeGame;

    public static GameManager instance;

    // Level list
    [SerializeField] private LevelListSO levelListData;
    public LevelSO currentLevelData { get; private set; } = null;
    public int passedLevelIndex { get; private set; } = 6;

    // Active player
    private Player activePlayer = null;
    private IActive activeTarget = null;

    // Conditions
    private List<GoalCondition> completedDone = new();


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

    public void LoadToLevel(LevelSO levelData)
    {
        Debug.Log($"[{name}]: Load to scene ({levelData.nameScene})");

        // Create list of conditions which are completed
        currentLevelData = levelData;
        completedDone.Clear();
        foreach (GoalCondition condition in currentLevelData.conditions)
            completedDone.Add(new GoalCondition(condition.idTarget, 0));

        // Load level and change audio
        AudioManager.instance.PlayBgmAudio(AudioClipDataNameStrings.AMBIENT_AUDIO);
        SceneManager.LoadScene(currentLevelData.nameScene);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevelData.nameScene);
        AudioManager.instance.PlayBgmAudio(AudioClipDataNameStrings.AMBIENT_AUDIO);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool CheckLevelCondition()
    {
        return true;
    }

    public void CheckConditions(string idTarget)
    {
        if (currentLevelData.conditions == null)
        {
            Debug.LogWarning($"[{name}]: Conditions list is null!");
            return;
        }

        foreach (GoalCondition condition in completedDone)
        {
            if (condition.idTarget == idTarget)
            {
                condition.count++;
            }
        }
    }

    public bool CheckCompleted()
    {
        if (completedDone.Count != currentLevelData.conditions.Count)
        {
            Debug.LogWarning($"[{name}]: Conditions list and CompletedDone list do not match!");
            return false;
        }

        for (int i = 0; i < currentLevelData.conditions.Count; i++)
        {
            if (completedDone[i].count < currentLevelData.conditions[i].count)
                return false;
        }

        return true;
    }

    public int GetIndexOfLevelItem(LevelSO levelData) => levelListData.levels.IndexOf(levelData);
}
