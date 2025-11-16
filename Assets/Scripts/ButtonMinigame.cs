using UnityEngine;
using UnityEngine.Events;

public class ButtonMinigame : MonoBehaviour
{
    [SerializeField] private float requiredHoldTime = 3f;
    [SerializeField] private float releaseWindow = 0.5f;
    [SerializeField] private UnityEvent onFinished;
    [SerializeField] private UnityEvent onFailed;
    [SerializeField] private UnityEvent timeEvent;
    private float _holdTime = 0f;
    private bool _isHolding = false;
    private bool _isCompleted = false;
    private bool _hasReachedRequiredTime = false;

    private void Update()
    {
        if (_isHolding && !_isCompleted)
        {
            _holdTime += Time.deltaTime;
            
            if (_holdTime >= requiredHoldTime && !_hasReachedRequiredTime)
            {
                _hasReachedRequiredTime = true;
                timeEvent.Invoke();
            }
        }
    }
    public void OnButtonPressed()
    {
        if (!_isCompleted)
        {
            _isHolding = true;
            _holdTime = 0f;
            _hasReachedRequiredTime = false;
        }
    }
    public void OnButtonReleased()
    {
        _isHolding = false;
        
        if (!_isCompleted)
        {
            if (_hasReachedRequiredTime && _holdTime <= requiredHoldTime + releaseWindow)
            {
                _isCompleted = true;
                onFinished.Invoke();
            }
            else
            {
                onFailed.Invoke();
            }
            _holdTime = 0f;
            _hasReachedRequiredTime = false;
        }
    }
    public float GetProgress()
    {
        if (requiredHoldTime <= 0) return 0f;
        return Mathf.Clamp01(_holdTime / requiredHoldTime);
    }
}
