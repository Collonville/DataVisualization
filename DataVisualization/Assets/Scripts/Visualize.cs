using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Visualize : MonoBehaviour {
    int index = 0;

    // Use this for initialization
    void Start () {
        var data = GameObject.Find("CSV").GetComponent<CSVData>().getRowData(0);

        setAllPlanets(0);
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            setAllPlanets(index++);

    }

    //極座標系から直交座標系へ変換
    //dec:赤緯, rec:赤経, r:半径
    private Vector3 Polar2Orthogonal(float dec, float rec, float r = 1.0f)
    {
        Vector3 orthogonal = new Vector3();

        orthogonal.x = r * Mathf.Cos(rec * Mathf.Deg2Rad) * Mathf.Cos(dec * Mathf.Deg2Rad);
        orthogonal.y = -r * Mathf.Sin(rec * Mathf.Deg2Rad) * Mathf.Cos(dec * Mathf.Deg2Rad);
        orthogonal.z = r * Mathf.Sin(dec * Mathf.Deg2Rad);

        return orthogonal;
    }

    private void setAllPlanets(int index)
    {
        var data = GameObject.Find("CSV").GetComponent<CSVData>().getRowData(index);

        GameObject obj = GameObject.Find("Sun").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Sun.azimuth"]));
        obj = GameObject.Find("Mercury").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Mercury.azimuth"]));
        obj = GameObject.Find("Venus").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Venus.azimuth"]));

        obj = GameObject.Find("Moon").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Moon.azimuth"]));
        obj = GameObject.Find("Mars").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Mars.azimuth"]));
        obj = GameObject.Find("Jupiter").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Jupiter.azimuth"]));
        obj = GameObject.Find("Saturn").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Saturn.azimuth"]));
        obj = GameObject.Find("Uranus").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Uranus.azimuth"]));
        obj = GameObject.Find("Neptune").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Neptune.azimuth"]));
        obj = GameObject.Find("Pluto").gameObject;
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, 1, 0), float.Parse(data["Pluto.azimuth"]));
        /*
        obj.transform.position = (Polar2Orthogonal(float.Parse(data["Mercury.declination"]), float.Parse(data["Mercury.rectascension"])) - sunPosition) * 16;
        obj = GameObject.Find("Venus").gameObject;
        obj.transform.position = (Polar2Orthogonal(float.Parse(data["Venus.declination"]), float.Parse(data["Venus.rectascension"])) - sunPosition) * 32;
        obj = GameObject.Find("Earth").gameObject;
        obj.transform.position =  sunPosition * 32;
        obj = GameObject.Find("Moon").gameObject;
        obj.transform.position = (Polar2Orthogonal(float.Parse(data["Moon.declination"]), float.Parse(data["Moon.rectascension"])) - sunPosition) * 50;
        obj = GameObject.Find("Mars").gameObject;
        obj.transform.position = (Polar2Orthogonal(float.Parse(data["Mars.declination"]), float.Parse(data["Mars.rectascension"])) - sunPosition) * 54;
        obj = GameObject.Find("Jupiter").gameObject;
        obj.transform.position = (Polar2Orthogonal(float.Parse(data["Jupiter.declination"]), float.Parse(data["Jupiter.rectascension"])) - sunPosition) * 70;
        obj = GameObject.Find("Saturn").gameObject;
        obj.transform.position = (Polar2Orthogonal(float.Parse(data["Saturn.declination"]), float.Parse(data["Saturn.rectascension"])) - sunPosition) * 86;
        obj = GameObject.Find("Uranus").gameObject;
        obj.transform.position = (Polar2Orthogonal(float.Parse(data["Uranus.declination"]), float.Parse(data["Uranus.rectascension"])) - sunPosition) * 102;
        obj = GameObject.Find("Neptune").gameObject;
        obj.transform.position = (Polar2Orthogonal(float.Parse(data["Neptune.declination"]), float.Parse(data["Neptune.rectascension"])) - sunPosition) * 118;
        obj = GameObject.Find("Pluto").gameObject;
        obj.transform.position = (Polar2Orthogonal(float.Parse(data["Pluto.declination"]), float.Parse(data["Pluto.rectascension"])) - sunPosition) * 134;
        */

    }
}
