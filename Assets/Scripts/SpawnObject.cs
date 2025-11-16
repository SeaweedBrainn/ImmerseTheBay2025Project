using UnityEngine;
using Meta.XR.MRUtilityKit;
using UnityEngine.Serialization;

public class SpawnObject : MonoBehaviour
{
    [Header("Raycasting")]
    [SerializeField] private Transform rightHandRayStart;
    [SerializeField] private Transform leftHandRayStart;
    [SerializeField] private float rayLength = 1;
    [SerializeField] private float maxHorizontalAngle = 20f;
    
    [Header("Spawning")]
    [SerializeField] private bool isSpawning = false;
    [SerializeField] GameObject[] gameObjectsToSpawn;
    [SerializeField] private Transform targetLookPosition;
    [SerializeField] private MRUKAnchor.SceneLabels labelFilter;

    private int _nextSpawnIndex = 0;
    private int _totalNumSpawns;
    

    public void SetSpawningStatus(bool status)
    {
        isSpawning = status;
        _totalNumSpawns = gameObjectsToSpawn.Length;
    }
    
    void Update()
    {
        if(!isSpawning) return;
        TryHandSpawn(leftHandRayStart);
        if(!isSpawning) return;
        TryHandSpawn(rightHandRayStart);
    }

    void TryHandSpawn(Transform handRayStart)
    {
        Ray ray = new Ray(handRayStart.position, handRayStart.forward);
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        bool hasHit = room.Raycast(
            ray,
            rayLength,
            LabelFilter.FromEnum(labelFilter),
            out RaycastHit hit,
            out MRUKAnchor anchor
        );
        if (!hasHit) return;

        Vector3 hitPoint = hit.point;
        Vector3 hitNormal = hit.normal;

        // Horizontal check
        float angle = Vector3.Angle(hitNormal, Vector3.up);
        if (angle > maxHorizontalAngle) return;
        
        // Facing camera (upright)
        Vector3 direction = targetLookPosition.position - hitPoint;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject gameObjectToSpawn = gameObjectsToSpawn[_nextSpawnIndex];
        gameObjectToSpawn.SetActive(true);
        gameObjectToSpawn.transform.position = hitPoint;
        gameObjectToSpawn.transform.rotation = rotation;

        if (_nextSpawnIndex != 0)
        {
            gameObjectsToSpawn[_nextSpawnIndex-1].SetActive(false);
        }

        isSpawning = false;
        UpdateNextSpawnIndex();
    }

    void UpdateNextSpawnIndex()
    {
        _nextSpawnIndex++;
        if(_nextSpawnIndex >= _totalNumSpawns) _nextSpawnIndex = 0;
    }
}