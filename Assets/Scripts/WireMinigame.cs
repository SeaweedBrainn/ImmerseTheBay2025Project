using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WireMinigame : MonoBehaviour
{
    [SerializeField] private int maxWires;
    [SerializeField] private UnityEvent onFinished;
    private List<Wire> _wires;

    public void ProcessWire(Wire wire)
    {
        if(wire.Snipped) _wires.Add(wire);
        if(_wires.Count >= maxWires) onFinished.Invoke();
    }
}
