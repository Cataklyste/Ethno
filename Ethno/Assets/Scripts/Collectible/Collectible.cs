using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Collectible : MonoBehaviour {

    Highlighter highlighter;

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
        highlighter = GetComponentInChildren<Highlighter>();
        highlighter.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnMouseEnter()
    {
        highlighter.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        highlighter.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        menu.AddButton(image, 1 << value);
        Destroy(this.gameObject);
    }
}
