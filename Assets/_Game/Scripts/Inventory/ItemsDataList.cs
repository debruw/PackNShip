using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "ItemsDataList", menuName = "Data/Items Data List", order = 0)]
    public class ItemsDataList : ScriptableObject
    {
        [Sirenix.OdinInspector.FilePath]
        public string ItemTypeFilePath;
        [Sirenix.OdinInspector.FolderPath]
        public string SOFilePath;
        public ItemData fillerData;
        public List<ItemData> itemDatas = new List<ItemData>();
        
#if UNITY_EDITOR
        async UniTask Generate()
        {
            await UniTask.Delay(2000);
            using (StreamWriter streamWriter = new StreamWriter(ItemTypeFilePath))
            {
                streamWriter.WriteLine("public enum ItemType");
                streamWriter.WriteLine("{");
                for (int i = 0; i < itemSprites.Count; i++)
                {
                    streamWriter.WriteLine("\t" + itemSprites[i].name.Replace(" ", "") + ",");
                }
                streamWriter.WriteLine("}");
            }
            
            AssetDatabase.Refresh();
            
            await UniTask.Delay(500);
            
            for (int i = 0; i < itemDatas.Count; i++)
            {
                ItemData data = itemDatas[i];
                data.itemType = (ItemType)i;
                data.itemName = data.itemIcon.name;
                
                Debug.Log(i + ") " + data.name);

                data.width = data.itemIcon.texture.width / 512;
                data.height = data.itemIcon.texture.height / 512;
            }
            
            Debug.LogError("FINISHED!");
        }

        [InfoBox("Add all images to the list. Click Generate SO for sprites button.")]
        
        public List<Sprite> itemSprites = new List<Sprite>();

        [Button("Generate SO for sprites")]
        public void GenerateSO()
        {
            Debug.LogError("PLEASE WAIT!");
            itemDatas.Clear();
            
            for (int i = 0; i < itemSprites.Count; i++)
            {
                CreateSO(itemSprites[i]);
            }
            
            Generate();
        }

        void CreateSO(Sprite sprite)
        {
            ItemData example = ScriptableObject.CreateInstance<ItemData>();
            example.name = sprite.name;
            example.itemName = sprite.name;
            example.itemIcon = sprite;
            string path = SOFilePath + "/" + example.itemName.Replace(" ", "") + ".asset";
            AssetDatabase.CreateAsset(example, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            itemDatas.Add(example);
        }
#endif
    }
}