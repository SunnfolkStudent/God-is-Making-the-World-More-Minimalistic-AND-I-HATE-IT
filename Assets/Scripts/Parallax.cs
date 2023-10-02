using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float _length, _startpos;

    public GameObject cam;

    public float parallaxEffect;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _startpos = transform.position.x;
        //_length = GetComponent<SpriteRenderer>().bounds.size.x;
        print(gameObject.name + ": " + _length);
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(_startpos + dist, transform.position.y, transform.position.z);

        if (temp > _startpos + 18.4465f)
        {
            _startpos += 18.4465f;
        }
        else if (temp < _startpos - 18.4465f)
        {
            _startpos -= 18.4465f;
        }
    }
}
