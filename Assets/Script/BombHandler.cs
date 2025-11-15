using UnityEngine;
using TMPro;

public class BombHandler : MonoBehaviour
{
    [Header("Strike Objects")]
    public GameObject[] strikeObjects;
    [Header("Replacement Material")]
    public Material strikeMaterial;
    
    public float countdownTime = 0f;
    public TextMeshPro timerText;

    private int strikesRemaining = 3;
    private float currentTime;
    private bool hasExploded = false;
    private bool[] objectReplaced;

    void Start()
    {
        currentTime = countdownTime;
        strikesRemaining = strikeObjects.Length;
        UpdateDisplay();
    }

    void Update()
    {
        if (countdownTime > 0 && !hasExploded && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateDisplay();
            if (currentTime <= 0)
            {
                currentTime = 0;
                Explode();
            }
        }
    }

    void UpdateDisplay()
    {
        if (timerText)
        {
            timerText.text = $"{currentTime:F1}s";
        }
    }

    public void Strike()
    {
        if (hasExploded) return;

        for (int i = 0; i < strikeObjects.Length; i++)
        {
            if (!objectReplaced[i] && strikeObjects[i])
            {
                ReplaceStrikeObject(i);
                return;
            }
        }
    }

    private void ReplaceStrikeObject(int strikeIndex)
    {
        if (hasExploded) return;
        if (strikeIndex < 0 || strikeIndex >= strikeObjects.Length) return;
        if (objectReplaced[strikeIndex]) return;
        if (strikeObjects[strikeIndex] && strikeMaterial)
        {
            MeshRenderer mr = strikeObjects[strikeIndex].GetComponent<MeshRenderer>();
            mr.material = strikeMaterial;
            
            strikeObjects[strikeIndex] = null;
            objectReplaced[strikeIndex] = true;
            strikesRemaining--;
        }
    }

    public void ReplaceStrikeObject(GameObject strikeObject)
    {
        if (hasExploded) return;
        for (int i = 0; i < strikeObjects.Length; i++)
        {
            if (strikeObjects[i] == strikeObject)
            {
                ReplaceStrikeObject(i);
                return;
            }
        }
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;
        for (int i = 0; i < strikeObjects.Length; i++)
        {
            if (strikeObjects[i])
            {
                MeshRenderer mr = strikeObjects[i].GetComponent<MeshRenderer>();
                mr.material = strikeMaterial;
            }
        }
        if (timerText)
        {
            timerText.text = "EXPLODED!";
        }
    }

    public void TriggerExplosion()
    {
        if (!hasExploded)
        {
            Explode();
        }
    }
}
