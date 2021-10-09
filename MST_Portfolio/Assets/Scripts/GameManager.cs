using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public delegate void ReloadLevelDelegate();

    public static ReloadLevelDelegate ReloadLevelHandler;

    private ComponentContainer myComponent;

    private int _levelNumber;
    private int _powerIconAmount;
    private int _magnetPowerIconAmount;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
        Debug.Log(Application.persistentDataPath);
    }

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
    }

    public int GetLevelNumber()
    {
        return _levelNumber;
    }

    public int GetMagnetPowerIconAmount()
    {
        return _magnetPowerIconAmount;
    }

    public int GetPowerIconAmount()
    {
        return _powerIconAmount;
    }

    private void SaveData()
    {
        SaveData data = new SaveData();
        data.levelNumber = _levelNumber;
        data.powerIconAmount = _powerIconAmount;
        data.magnetPowerIconAmount = _magnetPowerIconAmount;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/MSTGameData.json", json);
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + "/MSTGameData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            _levelNumber = data.levelNumber;
            _powerIconAmount = data.powerIconAmount;
            _magnetPowerIconAmount = data.magnetPowerIconAmount;
        }
    }
}


[System.Serializable]
internal class SaveData
{
    public int levelNumber;
    public int powerIconAmount;
    public int magnetPowerIconAmount;
}