using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager
{
    public static void SaveData (AllCharacterStats characterStats, Inventory inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Data.bacon";
        FileStream stream = new FileStream(path, FileMode.Create);

        //var json = JsonUtility.ToJson(characterStats, inventory);
        SavedData data = new SavedData(characterStats, inventory);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SavedData LoadData()
	{
        string path = Application.persistentDataPath + "/Data.bacon";

		if (File.Exists(path))
		{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedData data = formatter.Deserialize(stream) as SavedData;
            //var json = formatter.Deserialize(stream) as SavedData;
            stream.Close();

            return data;
		}
		else
		{
            Debug.LogError("Save file not found in: " + path);
            return null;
		}
	}
}
