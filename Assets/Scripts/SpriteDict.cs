using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptable")]

public class SpriteDict : ScriptableObject
{
    [System.Serializable]
    public class SpriteDictDTO
    {
        public string id;
        public Sprite sprite;
    }

    public List<SpriteDictDTO> list;

    public Sprite GetSprite(string id)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].id == id) return list[i].sprite;
        }
        return null;
    }

}

