using UnityEngine;
using UnityEngine.Events;

public class ButtonMinigame : MonoBehaviour
{
    [SerializeField] private float requiredHoldTime = 3f;
    [SerializeField] private UnityEvent onFinished;
    [SerializeField] private UnityEvent onFailed;
    [SerializeField] private UnityEvent timeEvent;
    [SerializeField] private UnityEvent onHover;
    [SerializeField] private UnityEvent onUnhover;
    [SerializeField] private UnityEvent onSelect;
    [SerializeField] private UnityEvent onUnselect;
    private float _holdTime = 0f;
    private bool _isHolding = false;
    private bool _isCompleted = false;

    private void Update()
    {
        if (_isHolding && !_isCompleted)
        {
            _holdTime += Time.deltaTime;
            if (_holdTime >= requiredHoldTime && _holdTime <= requiredHoldTime - .5f)
            {
                _isCompleted = true;
                timeEvent.Invoke();
                onFinished.Invoke();
            }
            else 
            {
                onFailed.Invoke();
            }
        }
    }
    public void OnButtonPressed()
    {
        if (!_isCompleted)
        {
            _isHolding = true;
        }
    }
    public void OnButtonReleased()
    {
        if (!_isCompleted)
        {
            _isHolding = false;
            onFailed.Invoke();
        }
    }
    public float GetProgress()
    {
        if (requiredHoldTime <= 0) return 0f;
        return Mathf.Clamp01(_holdTime / requiredHoldTime);
    }
}
