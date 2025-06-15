using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LogbookEntry : MonoBehaviour
{
    public string name = "";
    [TextArea(15, 20)]
    public string description = "";
    [SerializeField] private Text nameOutput, descriptionOutput;
    [Header("Entry not discovered")]
    [SerializeField] private Image outputGraphic;
    [SerializeField] private Color disabledColor;

    public void DisplayEntry()
    {
        if (Logbook.Instance.IsRecorded(name))
        {
            nameOutput.text = name;
            descriptionOutput.text = description;
        }
        else
        {
            nameOutput.text = "???";
            descriptionOutput.text = "ERROR 404";
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        outputGraphic.color = Logbook.Instance.IsRecorded(name) ? Color.white : disabledColor;
    }
}
