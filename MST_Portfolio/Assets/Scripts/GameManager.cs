using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public delegate void ReloadLevelDelegate(int levelNumber, int powerIconAmount, int magnetPowerIconAmount);

    public delegate void StartGameDelegate(bool isStarted);

    public static ReloadLevelDelegate ReloadLevelHandler;
    public static StartGameDelegate StartGameHandler;

    private ComponentContainer myComponent;

    private int _levelNumber;
    private int _powerIconAmount;
    private int _magnetPowerIconAmount;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _levelNumber = 1;
        _powerIconAmount = 20;
        _magnetPowerIconAmount = 1;

        LoadData();
        Debug.Log(Application.persistentDataPath);
    }

    private void Start()
    {
        ReloadLevel();
    }

    public void NextLevel()
    {
        _levelNumber++;
        _powerIconAmount += 5;
        // TODO: burası nasıl olacak dön bak :D
        _magnetPowerIconAmount += _levelNumber / 3;
        SaveData();
        ReloadLevelHandler(_levelNumber, _powerIconAmount, _magnetPowerIconAmount);
    }

    public void ReloadLevel()
    {
        ReloadLevelHandler(_levelNumber, _powerIconAmount, _magnetPowerIconAmount);
    }

    #region Data Functions

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
    #endregion
}


[System.Serializable]
internal class SaveData
{
    public int levelNumber;
    public int powerIconAmount;
    public int magnetPowerIconAmount;
}
