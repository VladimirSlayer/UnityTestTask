using System.Text;
using TMPro;
using UnityEngine;

public class LogPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _logText;
    [SerializeField] private TMP_Text _logButtonText;

    private void OnEnable()
    {
        if (UIManager.Instance != null && UIManager.Instance.State == UIState.Log)
            RefreshLog();
    }

    private void RefreshLog()
    {
        var entries = PickupLogManager.Instance.GetLogEntries();
        var sb = new StringBuilder(entries.Count);
        foreach (var entry in entries) sb.AppendLine(entry);
        _logText.text = sb.ToString();
    }

    public void ToggleLog()
    {
        bool opening = UIManager.Instance.State != UIState.Log;
        if (opening) RefreshLog();

        _logButtonText.text = opening ? "Закрыть лог" : "Открыть лог";
        UIManager.Instance.ToggleLogPanel();
    }
}
