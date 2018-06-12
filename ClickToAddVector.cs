using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToAddVector : MonoBehaviour {

    public GameObject vectorSprite_prefab;      //the vector sprite which will be added to the scene
    public float offset_x;
    public float offset_z;

    //VectorPositionAndRotation will access this variable in order to set the colour of the next drawn vector
    private int vector_count;       //count the number of vectors that are drawn in order to alternate the label positions
    
	// Use this for initialization
	void Start () {
        vector_count = 0;   //this should be iterated everytime the right mouse button is clicked - ie a vector is drawn
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            vector_count += 1;
            if(vector_count >= 9)   // there are 9 possible colours, so if we get to higher than 9 reset the count
            {
                vector_count = 0;
            }
            GameObject vector = Instantiate(vectorSprite_prefab) as GameObject;
            Vector3 newPosition = new Vector3((Camera.main.ScreenToWorldPoint(Input.mousePosition)).x - offset_x, 1f, (Camera.main.ScreenToWorldPoint(Input.mousePosition)).z - offset_z);
            vector.transform.position = newPosition;
        }
	}

    //Get the vector count variable so as to set the colour of the vector in the VectorPositionAndRotation script
    public int GetVectorCount()
    {
        return vector_count;
    }
}
