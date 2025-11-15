using UnityEngine;
using TMPro;

public class BombHandler : MonoBehaviour
{
    [Header("Strike Objects")]
    public GameObject[] strikeObjects = new GameObject[3];
    [Header("Replacement Prefab")]
    public GameObject replacementPrefab;
    
    public float countdownTime = 0f;
    public TextMeshPro timerText;

    private int strikesRemaining = 3;
    private float currentTime;
    private bool hasExploded = false;
    private bool[] objectReplaced = new bool[3];

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
        if (timerText != null)
        {
            if (countdownTime > 0)
            {
                timerText.text = $"Strikes: {strikesRemaining} | Time: {currentTime:F1}s";
            }
            else
            {
                timerText.text = $"Strikes Remaining: {strikesRemaining}";
            }
        }
    }

    public void HandleStrike()
    {
        if (hasExploded) return;

        for (int i = 0; i < strikeObjects.Length; i++)
        {
            if (!objectReplaced[i] && strikeObjects[i] != null)
            {
                ReplaceStrikeObject(i);
                return;
            }
        }
    }

    public void ReplaceStrikeObject(int strikeIndex)
    {
        if (hasExploded) return;
        if (strikeIndex < 0 || strikeIndex >= strikeObjects.Length) return;
        if (objectReplaced[strikeIndex]) return;
        if (strikeObjects[strikeIndex] != null && replacementPrefab != null)
        {
            Vector3 position = strikeObjects[strikeIndex].transform.position;
            Quaternion rotation = strikeObjects[strikeIndex].transform.rotation;
            Transform parent = strikeObjects[strikeIndex].transform.parent;

            GameObject replacement = Instantiate(replacementPrefab, position, rotation, parent);

            Destroy(strikeObjects[strikeIndex]);
            strikeObjects[strikeIndex] = null;
            objectReplaced[strikeIndex] = true;

            strikesRemaining--;
            UpdateDisplay();

            if (strikesRemaining <= 0)
            {
                Explode();
            }
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
            if (strikeObjects[i] != null && replacementPrefab != null)
            {
                Vector3 position = strikeObjects[i].transform.position;
                Quaternion rotation = strikeObjects[i].transform.rotation;
                Transform parent = strikeObjects[i].transform.parent;

                GameObject replacement = Instantiate(replacementPrefab, position, rotation, parent);
                Destroy(strikeObjects[i]);
            }
        }
        if (timerText != null)
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
