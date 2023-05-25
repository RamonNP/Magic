using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
   // Create a field for the save file.

    public static InventoryFileJson ReadFileInventoryFileJson(string keyItemList)
    {
        string saveFile = Application.persistentDataPath + keyItemList + ".json";
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            return JsonUtility.FromJson<InventoryFileJson>(fileContents);
        }
        return null;
    }    
    public static Attributes ReadFileAttributesFileJson(string keyItemList)
    {
        string saveFile = Application.persistentDataPath + keyItemList + ".json";
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            return JsonUtility.FromJson<Attributes>(fileContents);
        }
        return null;
    }

    public static void WriteFileInventoryFileJson(string keyItemList, InventoryFileJson gameData)
    {
        //TODO Criptografar
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(gameData);

        // Write JSON to file.
        File.WriteAllText(Application.persistentDataPath + keyItemList + ".json", jsonString);
        Debug.Log(Application.persistentDataPath + keyItemList + ".json Saved");
    }    
    public static void WriteFileAttributesFileJson(string keyAtributs, Attributes gameData)
    {
        //TODO Criptografar
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(gameData);

        // Write JSON to file.
        File.WriteAllText(Application.persistentDataPath + keyAtributs + ".json", jsonString);
        Debug.Log(Application.persistentDataPath + keyAtributs + ".json Saved");
    }

    public static Sprite GetInventorySprite(Item item)
    {
        string path;
        if (item.TypeItem == TypeItem.Armor)
        {
            path = "ItensInventory/" + item.TypeItem + "/icon" + item.SpriteItem;
        }
        else
        {
            path = "ItensInventory/" + item.TypeItem + "/" + item.SpriteItem.ToString();
        }
        return Resources.Load<Sprite>(path);
    }


}
