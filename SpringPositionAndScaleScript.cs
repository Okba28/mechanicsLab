using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPositionAndScaleScript : MonoBehaviour {

    public Transform spring;
    public Transform anchor;
    public Transform mass;

    public float original_length = 4.9f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 difference = (mass.position - anchor.position);
        Vector3 spring_pos = mass.position + difference/2;
        float spring_scale = difference.magnitude / original_length;
        spring.position = spring_pos;
        spring.localScale = new Vector3(1,spring_scale,1);
	}
}
