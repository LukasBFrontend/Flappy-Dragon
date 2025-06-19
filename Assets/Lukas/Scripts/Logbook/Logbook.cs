using System;
using UnityEngine;

public class Logbook : Singleton<Logbook>
{
    [HideInInspector] public static Log[] entries = null;
    [SerializeField] private Log[] logs;

    private void Start()
    {
        entries ??= logs;
    }
    public void RecordEntry(string name)
    {
        int index = Array.FindIndex(entries, log => log.name == name);
        if (index == -1)
        {
            Debug.Log("Log with name " + name + " not found");
            return;
        }
        entries[index].found = true;
    }

    public bool IsRecorded(string name)
    {
        Log entry = Array.Find(entries, log => log.name == name);
        return entry.found;
    }

    [System.Serializable]
    public class Log
    {
        public string name;
        public bool found = false;
    }
}
