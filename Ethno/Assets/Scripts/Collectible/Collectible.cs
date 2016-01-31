using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Collectible : MonoBehaviour {

    [SerializeField]
    private int value;
    [SerializeField]
    private Sprite image;
    
    private CirculareMenu menu;

	// Use this for initialization
	void Start ()
    {

        GameObject go = GameObject.FindGameObjectWithTag("CircularMenu");
        menu = go.GetComponent<CirculareMenu>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			image = Resources.Load<Sprite>("Icons/" + value);
			menu.AddButton(image, 1 << (value - 1), value);
            other.transform.FindChild("GameObject/UnlockedIcon").GetComponent<SpriteRenderer>().sprite = image;
            other.transform.FindChild("GameObject/UnlockedIcon").GetComponent<SignPreview>().ResetTimer();
			Destroy(this.gameObject);
		}
	}
}
