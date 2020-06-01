using UnityEngine;

public class InteractPoint : MonoBehaviour
{
    [Range(1,4)]
    public float radius = 2f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
