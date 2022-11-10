//using EffectSystem.SCRO;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//namespace Editor
//{
//    public class GenerateIDForEffects : MonoBehaviour
//    {
//        private int counterId = -1;

//        [SerializeField] private List<SCRO_Effect> effects = new List<SCRO_Effect>();

//        [ContextMenu("GenerateID")]
//        public void GenerateID()
//        {
//            FindAllEffects();

//            for (int i = 0; i < effects.Count; i++)
//            {
//                if (effects[i].Id > counterId)
//                {
//                    counterId = effects[i].Id;
//                }
//            }

//            counterId++;
//            Debug.Log(counterId);

//            for (int i = 0; i < effects.Count; i++)
//            {
//                if (effects[i].Id == 0)
//                {
//                    effects[i].Id = counterId;
//                    counterId++;

//#if UNITY_EDITOR
//                    EditorUtility.SetDirty(effects[i]);
//                    AssetDatabase.SaveAssets();
//                    AssetDatabase.Refresh();
//#endif
//                }
//            }
//        }

//        private void FindAllEffects()
//        {
//            foreach (var asset in AssetDatabase.FindAssets("t:SCRO_Effect effect", new[] { "Assets/5_SCRO_Objects/Effects/FightCards" }))
//            {
//                var path = AssetDatabase.GUIDToAssetPath(asset);
//                SCRO_Effect effect = (SCRO_Effect)AssetDatabase.LoadMainAssetAtPath(path);

//                if (!effects.Contains(effect))
//                {
//                    effects.Add(effect);
//                }
//            }

//            Debug.Log($"Найдено эффектов - {effects.Count}");
//        }
//    }
//}