using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PickupLogManager : MonoBehaviour
{
    public static PickupLogManager Instance { get; private set; }

    private List<string> logEntries = new List<string>();
    private string logFilePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        logFilePath = Path.Combine(Application.persistentDataPath, "pickups_log.txt");

        if (File.Exists(logFilePath))
        {
            logEntries = new List<string>(File.ReadAllLines(logFilePath));
        }

        Debug.Log(logFilePath);
    }

    public void AddEntry(string entry)
    {
        string line = $"{System.DateTime.Now} : {entry}";
        logEntries.Add(line);
        File.AppendAllText(logFilePath, line + "\n");
    }

    public List<string> GetLogEntries()
    {
        return new List<string>(logEntries);
    }
}
