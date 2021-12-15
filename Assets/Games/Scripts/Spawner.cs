using UnityEngine;

public class Spawner : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(1, 3, 1));
    }
}
