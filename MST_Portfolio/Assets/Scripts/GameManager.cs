using UnityEngine;
using System.IO;

/// <summary>
/// TODO:
/// engeller koy
/// �ok oyunculu
/// 
/// </summary>
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
    private int _magPowerAndBombIconAmount;

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
        _magPowerAndBombIconAmount = 1;

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
        _powerIconAmount += 7;

        if (_levelNumber < 4)
            _magPowerAndBombIconAmount = 2;
        else if (_levelNumber < 9)
            _magPowerAndBombIconAmount = 3;
        else if (_levelNumber < 20)
            _magPowerAndBombIconAmount = 4;
        else
            _magPowerAndBombIconAmount = 5;

        SaveData();
        ReloadLevelHandler(_levelNumber, _powerIconAmount, _magPowerAndBombIconAmount);
    }

    public void ReloadLevel()
    {
        ReloadLevelHandler(_levelNumber, _powerIconAmount, _magPowerAndBombIconAmount);
    }

    #region Data Functions

    private void SaveData()
    {
        SaveData data = new SaveData();
        data.levelNumber = _levelNumber;
        data.powerIconAmount = _powerIconAmount;
        data.magPowerAndBombIconAmount = _magPowerAndBombIconAmount;

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
            _magPowerAndBombIconAmount = data.magPowerAndBombIconAmount;
        }
    }

    #endregion
}


[System.Serializable]
internal class SaveData
{
    public int levelNumber;
    public int powerIconAmount;
    public int magPowerAndBombIconAmount;
}