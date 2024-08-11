using UnityEngine;

public class loopingbacjground1 : MonoBehaviour
{
    private float length, Startpos;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp =(cam.transform.position.x * (1 - parallaxEffect));
        float diet = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(Startpos + diet, transform.position.y, transform.position.z);
        if (temp > Startpos + length) Startpos += length;
        else if (temp < Startpos - length) Startpos -= length;
    }
}
