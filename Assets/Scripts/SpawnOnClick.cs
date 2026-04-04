using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    [Tooltip("Folder inside 'Resources' to load prefabs from")]
    public string resourceFolder = "PrefabsToSpawn";

    [Tooltip("List of props to spawn")]
    public GameObject[] props;

    public Camera mainCamera;         // Assign your main camera in the inspector
    public GameObject propToSpawn;    // The prefab to spawn   

    // Define the spawn area origin and bounds
    public Vector3 spawnCenter = Vector3.zero;
    public Vector3 spawnArea = new Vector3(10f, 0f, 10f);

void Start(){

    // Ensure the Mousepointer is in the Game view
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;

    // Load all prefabs from the specified Resources subfolder
    props = Resources.LoadAll<GameObject>(resourceFolder);
}

void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && propToSpawn != null)
//        if (Physics.Raycast(ray, out hit))
        {
                Vector3 randomPosition = GetRandomPositionWithinArea();
            
            //if(propToSpawn != null){
              //  Instantiate(propToSpawn, randomPosition, Quaternion.identity);
            //}
            // ...or spawn them all
            //else{

                foreach (GameObject prefab in props)
                {
                    if (prefab != null)
                    {
                    Instantiate(prefab, randomPosition, Quaternion.identity);
                    }
                }
            }
        }
}
    // Get a random Point inside the Spawn area
    Vector3 GetRandomPositionWithinArea()
{
    Vector3 randomOffset = new Vector3(

        // Enable for scaled offset
        //Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
        //Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
        //Random.Range(-spawnArea.z / 2, spawnArea.z / 2)


        Random.Range(-spawnArea.x, spawnArea.x),
        Random.Range(-spawnArea.y, spawnArea.y),
        Random.Range(-spawnArea.z, spawnArea.z)
    );

    return spawnCenter + randomOffset;
}

//Show the spawn area in the Scene view
void OnDrawGizmosSelected()
{
    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(spawnCenter, spawnArea);
}
}
