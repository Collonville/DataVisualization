using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CSVData : MonoBehaviour {
    private const string CSV_PATH = "SolarSystemAndEarthquakes";

    public Dictionary<int, Dictionary<string, string>> data;
    private List<string> columnName;

    // Use this for initialization
    void Start () {
        columnName = getColumnName();
        data = getAllData(columnName);

        /*
        for(int i = 0; i < data.Keys.Count; i++)
        {
            var da = data.ElementAt(i).Value;
            Debug.Log(da["earthquake.mag"]);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private Dictionary<int, Dictionary<string, string>> getAllData(List<string> columnName)
    {
        var allData = new Dictionary<int, Dictionary<string, string>>();
        int rowCount = 0;
        TextAsset csv = Resources.Load(CSV_PATH) as TextAsset;
        StringReader reader = new StringReader(csv.text);

        reader.ReadLine(); //Skip Fisrt Row
        while (reader.Peek() > -1)
        {
            string[] rowData = reader.ReadLine().Split(',');

            Dictionary<string, string> rowDictionary = new Dictionary<string, string>();
            for (int i = 0; i < columnName.Count; i++)
            {
                rowDictionary.Add(columnName[i], rowData[i]);
            }

            allData.Add(rowCount, rowDictionary);
            rowCount++;
        }

        reader.Close();

        return allData;
    }

    private List<string> getColumnName()
    {
        TextAsset csv = Resources.Load(CSV_PATH) as TextAsset;
        StringReader reader = new StringReader(csv.text);

        string[] firstLine = reader.ReadLine().Split(',');

        reader.Close();

        return new List<string>(firstLine);
    }
}
