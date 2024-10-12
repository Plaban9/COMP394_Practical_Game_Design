using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHover : MonoBehaviour
{

    public TextMeshProUGUI text;
    public GameObject tooltip;

    void Start()
    {
        // Ensure the TMP_TextEventHandler is on the same object
        if (text.TryGetComponent<TMP_TextEventHandler>(out var textEventHandler))
        {
            // Subscribe to the link click event
            textEventHandler.onLinkSelection.AddListener(OnLinkClicked);
        }
    }

    // This method is called when a link is clicked
    void OnLinkClicked(string linkID, string linkText, int linkIndex)
    {
        Debug.Log(string.Format("LinkID:{0}, Linktext: {1}, linkIndex: {2}", linkID, linkText, linkIndex));
        if (linkID.Equals("showTooltip"))
        {
            Tooltip();
        }
        else
        {
            Tooltip();
        }
    }

    public void Tooltip()
    {
        tooltip.SetActive(true);
    }


}
