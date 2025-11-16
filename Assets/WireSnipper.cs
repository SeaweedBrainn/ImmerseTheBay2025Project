using UnityEngine;
using UnityEngine.Events;

public class WireSnipper : MonoBehaviour
{
    public Material defaultMaterial;
    public Material activeMaterial;

    public MeshRenderer[] meshRenderers;

    public GameObject VisualFeedback;

    public static WireSnipper Instance;
    public UnityEvent OnEnterSnipRange, OnExitSnipRange, OnSnip,
        OnEnterGoodSnipRange, OnExitGoodSnipRange, OnEnterBadSnipRange, OnExitBadSnipRange;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    public static void OnEnterSnipRangeEvent(bool GoodWire)
    {
        if (Instance.VisualFeedback != null)
        {
            Instance.VisualFeedback.SetActive(true);
        }
        Debug.Log("OnEnterSnipRangeEvent");
        if (Instance != null)
        {
            Instance.OnEnterSnipRange.Invoke();
            if (GoodWire)
            {
                Debug.Log("OnEnterGOODSnipRangeEvent");
                Instance.OnEnterGoodSnipRange.Invoke();
            }
            else
            {
                Debug.Log("OnEnterBADSnipRangeEvent");
                Instance.OnEnterBadSnipRange.Invoke();
            }
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

    public static void OnExitSnipRangeEvent(bool GoodWire)
    {
        if (Instance.VisualFeedback != null)
        {
            Instance.VisualFeedback.SetActive(false);
        }
        Debug.Log("OnExitSnipRangeEvent");
        if (Instance != null)
        {
            Instance.OnExitSnipRange.Invoke();
            if (GoodWire)
            {
                Debug.Log("OnExitGOODSnipRangeEvent");
                Instance.OnExitGoodSnipRange.Invoke();
            }
            else
            {
                Debug.Log("OnExitBADSnipRangeEvent");
                Instance.OnExitBadSnipRange.Invoke();
            }
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
