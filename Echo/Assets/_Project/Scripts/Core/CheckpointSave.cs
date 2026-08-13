using UnityEngine;

public static class CheckpointSave
{
    private const string CheckpointKeyPrefix = "ECHO_Checkpoint_";

    public static void Save(string sceneName, Vector3 position, Quaternion rotation)
    {
        CheckpointData data = new CheckpointData
        {
            x = position.x,
            y = position.y,
            z = position.z,
            rotationY = rotation.eulerAngles.y
        };

        PlayerPrefs.SetString(
            CheckpointKeyPrefix + sceneName,
            JsonUtility.ToJson(data)
        );

        PlayerPrefs.Save();
    }

    public static bool TryLoad(
        string sceneName,
        out Vector3 position,
        out Quaternion rotation
    )
    {
        string key = CheckpointKeyPrefix + sceneName;

        if (!PlayerPrefs.HasKey(key))
        {
            position = default;
            rotation = default;
            return false;
        }

        CheckpointData data = JsonUtility.FromJson<CheckpointData>(
            PlayerPrefs.GetString(key)
        );

        position = new Vector3(data.x, data.y, data.z);
        rotation = Quaternion.Euler(0f, data.rotationY, 0f);
        return true;
    }

    [System.Serializable]
    private struct CheckpointData
    {
        public float x;
        public float y;
        public float z;
        public float rotationY;
    }
}