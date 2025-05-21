//using UnityEngine;
//using System.IO;
//using System.Xml.Serialization;
//using UnityEditor;

//public class saveSystem : MonoBehaviour
//{
//    Diana1 dianaSave;
//    HealthBar healthBar;

//    string savePath;
//    saveData data;

//    private void Awake()
//    {
//        dianaSave = FindAnyObjectByType<Diana1>();
//        healthBar = FindAnyObjectByType<HealthBar>();

//        savePath = Application.persistentDataPath + "/save.dat";
//        if (!File.Exists(savePath))
//        {
//            saveData newData = new saveData();
//            newData.playerPosition = Vector2.zero;
//            newData.playerHealth = 100;

//            SaveGame(newData);
//        }
//        data = LoadGame();
//    }
//    void SaveGame(saveData dataToSave)
//    {
//        string JsonData = JsonUtility.ToJson(dataToSave);
//        File.WriteAllText(savePath, JsonData);
//    }

//    saveData LoadGame()
//    {
//        string loaderData = File.ReadAllText(savePath);
//        saveData dataToReturn = JsonUtility.FromJson<saveData>(loaderData);

//        return dataToReturn;
//    }

//    public void SaveGameButton()
//    {
//        Vector2 playerPos = dianaSave.GetPosition();
//        float health = healthBar.GetHealth();

//        data.playerPosition = playerPos;
//        data.playerHealth = health;

//        SaveGame(data);
//    }

//    public void LoadGameButton()
//    {
//        Vector2 playerPos = data.playerPosition;
//        float health = data.playerHealth;

//        dianaSave.SetPosition(playerPos);
//        healthBar.SetHealth(health);
//    }
//}

//public class saveData
//{
//    public Vector2 playerPosition;
//    public float playerHealth;
//}
