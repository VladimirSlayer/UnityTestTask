using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum UIState { Gameplay, Note, Log, GameOver }

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _joysticksLayer;
    [SerializeField] private GameObject _notePanel;
    [SerializeField] private TMP_Text _noteText;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _logPanel;
    [SerializeField] private GameObject _openLogButton;
    [SerializeField] private TMP_Text _hpText;

    private bool isRestarting = false;

    public static UIManager Instance { get; private set; }

    public static event System.Action<bool> OnPauseStateChanged;

    public UIState State { get; private set; } = UIState.Gameplay;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        SetUIState(UIState.Gameplay);
    }

    public void RestartLevel()
    {
        if (isRestarting) return;
        isRestarting = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RefreshHealth(float currentHP)
    {
        _hpText.text = $"HP: {currentHP}";
    }

    public void SetUIState(UIState newState)
    {
        State = newState;
        _notePanel.SetActive(newState == UIState.Note);
        _logPanel.SetActive(newState == UIState.Log);
        _gameOverScreen.SetActive(newState == UIState.GameOver);
        _joysticksLayer.SetActive(newState == UIState.Gameplay);
        _openLogButton.SetActive(newState == UIState.Gameplay || newState == UIState.Log);
        bool paused = newState != UIState.Gameplay;
        OnPauseStateChanged?.Invoke(paused);
        Time.timeScale = paused ? 0f : 1f;
    }

    public void ToggleLogPanel()
    {
        SetUIState(State == UIState.Log ? UIState.Gameplay : UIState.Log);
    }

    public void ShowNote(string noteText)
    {
        _noteText.text = noteText;
        SetUIState(UIState.Note);
    }

    public void CloseNote() => SetUIState(UIState.Gameplay);
    public void ShowGameOverScreen() => SetUIState(UIState.GameOver);
}
