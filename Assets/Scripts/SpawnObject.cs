using UnityEngine;
using Meta.XR.MRUtilityKit;
using UnityEngine.Serialization;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToSpawn;
    [SerializeField] private Transform rightHandRayStart;
    [SerializeField] private Transform leftHandRayStart;
    [SerializeField] private float rayLength = 1;
    [SerializeField] private MRUKAnchor.SceneLabels labelFilter;
    [SerializeField] private Transform playerCamera; // ← your serialized reference
    [SerializeField] private float maxHorizontalAngle = 20f;

    void Update()
    {
        Ray ray1 = new Ray(rightHandRayStart.position, rightHandRayStart.forward);
        Ray ray2 = new Ray(leftHandRayStart.position, leftHandRayStart.forward);

        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        bool hasHit1 = room.Raycast(
            ray1,
            rayLength,
            LabelFilter.FromEnum(labelFilter),
            out RaycastHit hit1,
            out MRUKAnchor anchor1
        );

        bool hasHit2 = room.Raycast(
            ray2,
            rayLength,
            LabelFilter.FromEnum(labelFilter),
            out RaycastHit hit2,
            out MRUKAnchor anchor2
        );

        if (hasHit1)
        {
            Vector3 hitPoint = hit1.point;
            Vector3 hitNormal = hit1.normal;

            // Horizontal check
            float angle = Vector3.Angle(hitNormal, Vector3.up);
            if (angle <= maxHorizontalAngle)
            {
                // Facing camera (upright)
                Vector3 direction = playerCamera.position - hitPoint;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);

                gameObjectToSpawn.SetActive(true);
                gameObjectToSpawn.transform.position = hitPoint;
                gameObjectToSpawn.transform.rotation = rotation;
                
                this.gameObject.SetActive(false);
            }
        }
        
        if (hasHit2)
        {
            Vector3 hitPoint = hit2.point;
            Vector3 hitNormal = hit2.normal;

            // Horizontal check
            float angle = Vector3.Angle(hitNormal, Vector3.up);
            if (angle <= maxHorizontalAngle)
            {
                // Facing camera (upright)
                Vector3 direction = playerCamera.position - hitPoint;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);

                gameObjectToSpawn.SetActive(true);
                gameObjectToSpawn.transform.position = hitPoint;
                gameObjectToSpawn.transform.rotation = rotation;
                
                this.gameObject.SetActive(false);
            }
        }
    }
}