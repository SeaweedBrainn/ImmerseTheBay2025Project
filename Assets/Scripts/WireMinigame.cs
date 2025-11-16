using UnityEngine;
using UnityEngine.Events;

public class WireMinigame : MonoBehaviour
{
    [SerializeField] private Wire[] wires;
    [SerializeField] private UnityEvent correctWireSnipEvent;
    [SerializeField] private UnityEvent wrongWireSnipEvent;
    [SerializeField] private UnityEvent victoryEvent;
    
    private int _nextWireIndex = 0;

    public void Snip(Wire wire)
    {
        if (wire == wires[_nextWireIndex])
        {
            correctWireSnipEvent?.Invoke();
            _nextWireIndex++;
            if(_nextWireIndex >= wires.Length) victoryEvent?.Invoke();
        }
        else
        {
            wrongWireSnipEvent?.Invoke();
        }
    }
}
