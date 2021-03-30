using UnityEngine;

public class GrassRandomPlacement : MonoBehaviour
{
    public GizmosManager gizmos;

    [Header("Grass group spawner properties")]
    public GameObject grassPrefab;
    public GameObject grassVolumeBox;
    [Range(0,500),Tooltip("It will spawn the grassPrefab + the extraGrass")]
    public int extraGrass;
    [Range(0.1f, 5.0f)]
    public float grassRange;
    [Range(0.1f, 1.0f)]
    public float VolumeBoxColliderOffset;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < extraGrass; i++)
        {
            // spawn a prefab
            GameObject objName = (GameObject)Instantiate(grassPrefab, this.transform.position, transform.rotation);

            // set as child of the grass group empty object (for orginization)
            objName.transform.parent = transform;

            // calculate new random position
            Vector2 randVector2 = new Vector2(Random.Range(-grassRange, grassRange), Random.Range(-grassRange, grassRange));

            // apply position to new prefab
            objName.transform.position = new Vector3(grassPrefab.transform.position.x + randVector2.x, grassPrefab.transform.position.y, grassPrefab.transform.position.z + randVector2.y);
        }

        float VolumeBoxScale = Mathf.Clamp((grassRange * 2.0f) - VolumeBoxColliderOffset, 0.25f, Mathf.Infinity);
        grassVolumeBox.transform.localScale = new Vector3(VolumeBoxScale, 1, VolumeBoxScale);
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = gizmos.foliageGizmo;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), new Vector3(grassRange * 2.0f, 1.0f, grassRange * 2.0f));
	}
}
