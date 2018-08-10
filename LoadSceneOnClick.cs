using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Loads a new scene AND IMPORTANTLY, resets any global Physics changes in individual scenes.
public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.fixedDeltaTime = 0.01f;            //most scenes should run at this physics framerate - individual scenes can change it, but it should change back to this default.
        Physics.gravity = new Vector3(0, -9.81f, 0);    //gravity may have changed in the projectiles scene
        Time.timeScale = 1;                             //scene may have been paused and then exited
    }
}
