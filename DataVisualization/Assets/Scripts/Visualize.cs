using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Visualize : MonoBehaviour {
	// Use this for initialization
	void Start () {
        var data = GameObject.Find("CSV").GetComponent<CSVData>().getRowData(0);

        for (int i = 0; i < data.Keys.Count; i++)
        {
            var da = data.ElementAt(i).Value;
            Debug.Log(da);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
