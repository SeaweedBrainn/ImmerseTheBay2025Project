using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BombHandler : MonoBehaviour
{
    [Header("Strike Objects")]
    public GameObject[] strikeObjects;
    [Header("Replacement Material")]
    public Material strikeMaterial;
    [Header("Win Material")]
    public Material winMaterial;
    
    public float countdownTime = 0f;
    public TextMeshPro timerText;

    public UnityEvent OnExplodeEvent;
    public UnityEvent OnWinEvent;

    private int strikesRemaining = 3;
    private float currentTime;
    private bool hasExploded = false;
    private bool hasWon = false;
    private bool[] objectReplaced;

    void Start()
    {
        currentTime = countdownTime;
        strikesRemaining = strikeObjects.Length;
        objectReplaced = new bool[strikeObjects.Length];
        UpdateDisplay();
    }

    void Update()
    {
        if (countdownTime > 0 && !hasExploded && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                Explode();
            }
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        if (hasWon)
        {
            timerText.text = "Clear";
            return;
        }
        if (hasExploded)
        {
            timerText.text = "Boom!";
            return;
        }
        if (timerText)
        {
            timerText.text = $"{currentTime:F1}s";
        }
    }

    public void Strike()
    {
        if (hasExploded || hasWon) return;

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
        if (hasExploded || hasWon) return;
        if (strikeIndex < 0 || strikeIndex >= strikeObjects.Length) return;
        if (objectReplaced[strikeIndex]) return;
        if (strikeObjects[strikeIndex] && strikeMaterial)
        {
            MeshRenderer mr = strikeObjects[strikeIndex].GetComponent<MeshRenderer>();
            mr.material = strikeMaterial;
            objectReplaced[strikeIndex] = true;
            Debug.Log("Object replaced: " + objectReplaced[strikeIndex]);
            strikesRemaining--;
            if (strikesRemaining <= 0)
            {
                hasExploded = true;
                UpdateDisplay();
                Explode();
            }
        }
    }

    public void ReplaceStrikeObject(GameObject strikeObject)
    {
        if (hasExploded || hasWon) return;
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
        if (hasExploded || hasWon) return;
        hasExploded = true;
        for (int i = 0; i < strikeObjects.Length; i++)
        {
            if (strikeObjects[i] && !objectReplaced[i])
            {
                MeshRenderer mr = strikeObjects[i].GetComponent<MeshRenderer>();
                mr.material = strikeMaterial;
            }
        }
        UpdateDisplay();
        OnExplodeEvent.Invoke();
    }

    public void TriggerExplosion()
    {
        if (!hasExploded && !hasWon)
        {
            Explode();
        }
    }

    public void Win()
    {
        if (hasExploded || hasWon) return;
        hasWon = true;
        for (int i = 0; i < strikeObjects.Length; i++)
        {
            if (strikeObjects[i] && winMaterial)
            {
                MeshRenderer mr = strikeObjects[i].GetComponent<MeshRenderer>();
                if (mr != null)
                {
                    mr.material = winMaterial;
                }
            }
        }
        OnWinEvent.Invoke();
    }
}
