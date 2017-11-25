using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Visualize : MonoBehaviour {
    public Text time;
    public Text place;
    public Text magText;
    public Slider slider;

    public Text detailText1, detailText2, moonText;
    int index = 0;

    private List<Vector3> defaultPotision = new List<Vector3>();
    private Dictionary<string, string> data;

    public GameObject circle1, circle2;
    private string[] selectedPlanet = new string[2] {"Earth", "Earth" };

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
        SelectData();
        SetPlanetDetail();
    }

    private void SetPlanetDetail()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        StringBuilder strbui = new StringBuilder();
        if (Physics.Raycast(ray, out hit, 1000))
        {
            string objectName = hit.collider.gameObject.name;
            
            strbui.AppendLine(objectName);
            strbui.AppendLine("Longitude : " + data[objectName + ".longitude"]);
            strbui.AppendLine("Latitude  : " + data[objectName + ".latitude"]);
            strbui.AppendLine("Rectascension : " + data[objectName + ".rectascension"]);
            strbui.AppendLine("Declination : " + data[objectName + ".declination"]);
            strbui.AppendLine("Azimuth : " + data[objectName + ".azimuth"]);
            strbui.AppendLine("Height  : " + data[objectName + ".height"]);
            strbui.AppendLine("Speed   : " + data[objectName + ".speed"]);
            strbui.AppendLine("House   : " + data[objectName + ".house"]);
            strbui.AppendLine("HouseNumber : " + data[objectName + ".housenumber"]);

            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                selectedPlanet[0] = objectName;
                circle1.gameObject.transform.position = hit.collider.gameObject.transform.position;
                detailText1.text = strbui.ToString();
            }
            else if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
            {
                selectedPlanet[1] = objectName;
                circle2.gameObject.transform.position = hit.collider.gameObject.transform.position;
                detailText2.text = strbui.ToString();
            }
        }

        
    }

    private void SelectData()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            index++;
            if (index > 3645)
                index = 3645;

            slider.value = index;
            setVisualize(index);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index--;
            if (index < 0)
                index = 0;

            slider.value = index;
            setVisualize(index);
        }
    }

    public void setsetVisualizeFromSlider()
    {
        index = (int)slider.value;
        setVisualize(index);
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

    private Dictionary<string, string> GetData(int index)
    {
        return GameObject.Find("CSV").GetComponent<CSVData>().getRowData(index);
    }

    private void setVisualize(int index)
    {
        if (index >= 0 && index < GameObject.Find("CSV").GetComponent<CSVData>().getDataSize())
        {
            data = GetData(index);
            setUI();
            setAllPlanets();
        }
    }

    private void setUI()
    {
        string[] olgTime = data["earthquake.time"].Replace('-', '/').Split('T');
        time.text = olgTime[0] + " " + olgTime[1];

        place.text = data["earthquake.place"];

        magText.text = "M" + data["earthquake.mag"];
        Image magBack = GameObject.Find("MagBack").GetComponent<Image>();
        magBack.color = Color.HSVToRGB(Mathf.Clamp01(float.Parse(data["earthquake.mag"]) * 0.1f + 0.1f), 1.0f, 0.8f);

        StringBuilder strbui = new StringBuilder();
        strbui.AppendLine("Earthquake Detail");
        strbui.AppendLine("Latitude : " + data["earthquake.latitude"]);
        strbui.AppendLine("Longitude : " + data["earthquake.longitude"]);
        strbui.AppendLine("");
        strbui.AppendLine("Moon Detail");
        strbui.AppendLine("Dynamic : " + data["MoonPhase.dynamic"]);
        strbui.AppendLine("Value : " + data["MoonPhase.value"]);
        strbui.AppendLine("Total : " + data["MoonPhase.total"]);
        strbui.AppendLine("Percent : " + data["MoonPhase.percent"]);
        strbui.AppendLine("Illumination : " + data["MoonPhase.illumination"]);

        moonText.text = strbui.ToString();
    }

    private void setAllPlanets()
    {
        const float resizeHight = 5.0f; 

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

        circle1.gameObject.transform.position = GameObject.Find(selectedPlanet[0]).gameObject.transform.position;
        circle2.gameObject.transform.position = GameObject.Find(selectedPlanet[1]).gameObject.transform.position;
    }
}
