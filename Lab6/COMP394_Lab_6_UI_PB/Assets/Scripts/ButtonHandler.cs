using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonLabel;

    static readonly int ID_GlowPower = Shader.PropertyToID("_GlowPower");

    // Start is called before the first frame update
    void Start()
    {
        if (_buttonLabel == null)
        {
            _buttonLabel = GetComponent<TextMeshProUGUI>();
        }

        _buttonLabel.fontMaterial.DisableKeyword("GLOW_ON");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonHoverEnter()
    {
        Debug.Log($"{gameObject.name}: Hover Enter");
        _buttonLabel.fontMaterial.EnableKeyword("GLOW_ON");
        _buttonLabel.material.SetFloat("_GlowOuter", 1f);
    } 
    
    public void OnButtonHoverExit()
    {
        Debug.Log($"{gameObject.name}: Hover Exit");
        _buttonLabel.fontMaterial.DisableKeyword("GLOW_ON");
        _buttonLabel.material.SetFloat("_GlowOuter", 0f);
    }
}
