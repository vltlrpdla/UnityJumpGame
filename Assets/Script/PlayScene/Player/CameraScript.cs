using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float m_fSpeed = 10.0f;
    public GameObject PlayerObject = null;
    public float m_fPlayerInterval = 3.0f;
   

    void Awake()
    {
        PlayerObject = GameObject.Find("Player");
    }

     //Update is called once per frame
    void Update () {

        if ( null != PlayerObject) {
            transform.position = new Vector3(PlayerObject.transform.position.x + m_fPlayerInterval, 0, transform.position.z);
        }
    }
}
