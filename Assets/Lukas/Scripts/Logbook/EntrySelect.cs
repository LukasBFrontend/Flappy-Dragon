using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class EntrySelect : MonoBehaviour, ISelectHandler// required interface when using the OnSelect method.
{
    //Do this when the selectable UI object is selected.
    [SerializeField] private LogbookEntry logbookEntry;
    public void OnSelect(BaseEventData eventData)
    {
        logbookEntry.DisplayEntry();
    }
}
