using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private int NombreIA = 0;

    public GameObject GO;

    private float SpawnRadius;

	void Start ()
    {
        SpawnRadius = transform.localScale.x / 2;
        GetComponent<MeshRenderer>().enabled = false;
		Spawn();
	}
	
	void Update ()
    {
     
	}

    private void Spawn()
    {
		for (int i = 0; i <= NombreIA; ++i)
        {
            Vector3 newPos = Random.insideUnitCircle * SpawnRadius;
			newPos.z = newPos.y;
			Debug.Log("X = " + newPos.x + ", Y = " + newPos.y + ", Z = " + newPos.z);
			GameObject tmp = GameObject.Instantiate(GO, this.transform.position + newPos, GO.transform.rotation) as GameObject;
			tmp.transform.position = new Vector3(tmp.transform.position.x, 1, tmp.transform.position.z);
        }
    }
}
