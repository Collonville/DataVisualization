using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class Visualize : MonoBehaviour {
    public Text time;
    public Text magText;
    public Slider slider;
    int index = 0;

    private List<Vector3> defaultPotision = new List<Vector3>();

    // Use this for initialization
    void Start () {
        defaultPotision.Add(GameObject.Find("Sun").gameObject.transform.position);
        defaultPotision.Add(GameObject.Find("Mercury").gameObject.transform.position);
        defaultPotision.Add(GameObject.Find("Venus").gameObject.transform.position);
        defaultPotision.Add(GameObject.Find("Moon").gameObject.transform.position);
        defaultPotision.Add(GameObject.Find("Mars").gameObject.transform.position);
        defaultPotision.Add(GameObject.Find("Jupiter").gameObject.transform.position);
        defaultPotision.Add(GameObject.Find("Saturn").gameObject.transform.position);
        defaultPotision.Add(GameObject.Find("Uranus").gameObject.transform.position);
        defaultPotision.Add(GameObject.Find("Neptune").gameObject.transform.position);
        defaultPotision.Add(GameObject.Find("Pluto").gameObject.transform.position);

        setVisualize(0);
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            setVisualize(index++);
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index--;
            if (index < 0)
                index = 0;
            setVisualize(index);
        }
            

    }

    public void setsetVisualizeFromSlider()
    {
        setVisualize((int)slider.value);
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

    private void setVisualize(int index)
    {
        if (index >= 0 && index < GameObject.Find("CSV").GetComponent<CSVData>().getDataSize())
        {
            setUI(index);
            setAllPlanets(index);
        }
    }

    private void setUI(int index)
    {
        var data = GameObject.Find("CSV").GetComponent<CSVData>().getRowData(index);
        string[] olgTime = data["earthquake.time"].Replace('-', '/').Split('T');
        time.text = "Time : " + olgTime[0] + " " + olgTime[1];

        magText.text = data["earthquake.mag"];
        Renderer magBack = GameObject.Find("MagBack").GetComponent<Renderer>();
        magBack.material.color = Color.HSVToRGB(Mathf.Clamp01(float.Parse(data["earthquake.mag"]) * 0.1f + 0.1f), 1.0f, 0.6f);
    }

    private void setAllPlanets(int index)
    {
        const float resizeHight = 5.0f; 
        var data = GameObject.Find("CSV").GetComponent<CSVData>().getRowData(index);

        GameObject obj = GameObject.Find("Sun").gameObject;
        obj.transform.position = defaultPotision[0] + new Vector3(0, float.Parse(data["Sun.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Sun.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));

        obj = GameObject.Find("Mercury").gameObject;
        obj.transform.position = defaultPotision[1] + new Vector3(0, float.Parse(data["Mercury.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Mercury.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));

        obj = GameObject.Find("Venus").gameObject;
        obj.transform.position = defaultPotision[2] + new Vector3(0, float.Parse(data["Venus.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Venus.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));

        obj = GameObject.Find("Moon").gameObject;
        obj.transform.position = defaultPotision[3] + new Vector3(0, float.Parse(data["Moon.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Moon.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));

        obj = GameObject.Find("Mars").gameObject;
        obj.transform.position = defaultPotision[4] + new Vector3(0, float.Parse(data["Mars.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Mars.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));

        obj = GameObject.Find("Jupiter").gameObject;
        obj.transform.position = defaultPotision[5] + new Vector3(0, float.Parse(data["Jupiter.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Jupiter.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));

        obj = GameObject.Find("Saturn").gameObject;
        obj.transform.position = defaultPotision[6] + new Vector3(0, float.Parse(data["Saturn.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Saturn.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));

        obj = GameObject.Find("Uranus").gameObject;
        obj.transform.position = defaultPotision[7] + new Vector3(0, float.Parse(data["Uranus.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Uranus.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));

        obj = GameObject.Find("Neptune").gameObject;
        obj.transform.position = defaultPotision[8] + new Vector3(0, float.Parse(data["Neptune.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Neptune.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));

        obj = GameObject.Find("Pluto").gameObject;
        obj.transform.position = defaultPotision[9] + new Vector3(0, float.Parse(data["Pluto.height"]) / resizeHight, 0);
        obj.transform.RotateAround(GameObject.Find("Earth").gameObject.transform.position, new Vector3(0, -1, 0), float.Parse(data["Pluto.azimuth"]));
        obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
        obj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(obj.transform.position.x, 0, obj.transform.position.z));
    }
}
