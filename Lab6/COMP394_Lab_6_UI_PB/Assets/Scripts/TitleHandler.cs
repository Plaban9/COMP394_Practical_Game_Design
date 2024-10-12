using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UIElements;


public class TitleHandler : MonoBehaviour
{
    [SerializeField] private AnimationCurve _alphaCurve;
    [SerializeField] private TextMeshProUGUI _titleText;

    [SerializeField] private float _currentAlpha;
    [SerializeField] private float _effectDurationPerLoopInSecs = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAlpha();
    }

    private void UpdateAlpha()
    {
        if (_alphaCurve != null)
        {
            var currentTimeSlice = Mathf.PingPong(Time.time, _effectDurationPerLoopInSecs) / _effectDurationPerLoopInSecs;
            _currentAlpha = _alphaCurve.Evaluate(currentTimeSlice);
            _titleText.color = GettitleColor(_currentAlpha);
        }
    }

    private Color GettitleColor(float alpha)
    {
        return new Color(_titleText.color.r, _titleText.color.g, _titleText.color.b, alpha);
    }
}
