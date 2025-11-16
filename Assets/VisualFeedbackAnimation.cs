using UnityEngine;

public class VisualFeedbackAnimation : MonoBehaviour
{
    int active = 0;

    public GameObject feedback1;
    public GameObject feedback2;
    public GameObject feedback3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateAnimation();
    }

    // Update is called once per frame
    void UpdateAnimation()
    {
        Invoke("UpdateAnimation", .15f);
        active++;
        if (active > 2)
        {
            active = 0;
        }
        feedback1.SetActive(active == 0);
        feedback2.SetActive(active == 1);
        feedback3.SetActive(active == 2);
    }
}
