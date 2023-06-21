using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGallery : MonoBehaviour
{
    public void LoadSceneGallery(){
        SceneTransition.SwitchToScene("Gallery");
    }
}
