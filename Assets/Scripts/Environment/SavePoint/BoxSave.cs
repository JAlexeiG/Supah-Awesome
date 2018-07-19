using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSave : MonoBehaviour {
    private Transform trans;
    private void Start()
    {
        trans = transform; 
    }
    public struct Box
    {
        public string name;
        public Vector3 position;
    }
    public Box GetBox()
    {
        Box box = new Box();
        box.name = name;
        box.position = transform.position;
        return box;
    }

    public void SaveXMLPlayer(Box box)
    { 
        trans.position = box.position;
    }
}
