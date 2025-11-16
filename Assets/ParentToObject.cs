using UnityEngine;

[ExecuteInEditMode]
public class ParentToObject : MonoBehaviour
{
    public string pathToObject;
    public Vector3 offset;
    public Vector3 rotationOffset;

    public GameObject objectToCheckPath;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying && objectToCheckPath != null)
        {
            string path = GetHierarchyPath(objectToCheckPath.transform);
            Debug.Log($"[ParentToObject] objectToCheckPath path: {path}", objectToCheckPath);
        }
    }

    private string GetHierarchyPath(Transform t)
    {
        if (t == null)
            return "";
        string path = t.name;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }
#endif
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Application.isPlaying)
        {
            if (!string.IsNullOrEmpty(pathToObject))
            {
                GameObject targetObject = GameObject.Find(pathToObject);
                if (targetObject != null)
                {
                    this.transform.SetParent(targetObject.transform);
                    transform.localPosition = offset;
                    transform.localRotation = Quaternion.Euler(rotationOffset);
                }
                else
                {
                    Debug.LogWarning($"ParentToObject: Object with path '{pathToObject}' not found in scene.");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
