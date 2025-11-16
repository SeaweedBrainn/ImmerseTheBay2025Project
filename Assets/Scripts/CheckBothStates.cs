using UnityEngine;
using UnityEngine.Events;

public class CheckBothStates : MonoBehaviour
{
    [SerializeField] private bool wireComplete;
    [SerializeField] private bool buttonComplete;

    [SerializeField] private UnityEvent onBothComplete;
    void Update()
    {
        if (wireComplete && buttonComplete)
        {
            onBothComplete.Invoke();
            this.enabled = false;
        }
    }

    public void MarkWireComplete()
    {
        wireComplete = true;
    }

    public void MarkButtonComplete()
    {
        buttonComplete = true;
    }
}
