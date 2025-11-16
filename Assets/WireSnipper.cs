using UnityEngine;
using UnityEngine.Events;

public class WireSnipper : MonoBehaviour
{
    public Material defaultMaterial;
    public Material activeMaterial;

    public MeshRenderer[] meshRenderers;

    public static WireSnipper Instance;
    public UnityEvent OnEnterSnipRange, OnExitSnipRange, OnSnip;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    public static void OnEnterSnipRangeEvent()
    {
        Debug.Log("OnEnterSnipRangeEvent");
        if (Instance != null)
        {
            Instance.OnEnterSnipRange.Invoke();
            if (Instance.meshRenderers != null && Instance.activeMaterial != null)
            {
                foreach (var renderer in Instance.meshRenderers)
                {
                    if (renderer != null)
                    {
                        renderer.material = Instance.activeMaterial;
                    }
                }
            }
        }
    }

    public static void OnExitSnipRangeEvent()
    {
        Debug.Log("OnExitSnipRangeEvent");
        if (Instance != null)
        {
            Instance.OnExitSnipRange.Invoke();
            if (Instance.meshRenderers != null && Instance.defaultMaterial != null)
            {
                foreach (var renderer in Instance.meshRenderers)
                {
                    if (renderer != null)
                    {
                        renderer.material = Instance.defaultMaterial;
                    }
                }
            }
        }
    }

    public static void OnSnipEvent()
    {
        Debug.Log("WireSnipper: OnSnip");
        Instance.OnSnip.Invoke();
    }

}
