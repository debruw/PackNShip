using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "ItemsDataList", menuName = "Data/Items Data List", order = 0)]
    public class ItemsDataList : ScriptableObject
    {
        public ItemData fillerData;
        public List<ItemData> itemDatas = new List<ItemData>();
        
#if UNITY_EDITOR
        [InfoBox("Set all items image and add to list. Click Apply Items button. System is creating and setting necessery details")]
        
        [Button("Apply Items")]
        public void Generate()
        {
            string filePathAndName = "Assets/_Game/Scripts/ItemType.cs"; //The folder is expected to exist

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                streamWriter.WriteLine("public enum ItemType");
                streamWriter.WriteLine("{");
                for (int i = 0; i < itemDatas.Count; i++)
                {
                    streamWriter.WriteLine("\t" + itemDatas[i].name + ",");
                }
                streamWriter.WriteLine("}");
            }
            
            AssetDatabase.Refresh();
            
            for (int i = 0; i < itemDatas.Count; i++)
            {
                ItemData data = itemDatas[i];
                data.itemType = (ItemType)i;
                data.itemName = data.itemIcon.name;

                data.width = (int)(data.itemIcon.texture.width / 512f);
                data.height = (int)(data.itemIcon.texture.height / 512f);
            }
        }
#endif
    }
}