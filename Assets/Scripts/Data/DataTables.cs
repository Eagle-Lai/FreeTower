using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using System;
using FTProject;

namespace cfg
{
    public class DataTables : MonoBehaviour
    {
        public TBEnemyData TBEnemyData { get; set; }
        public TBRoundData TBRoundData { get; set; }
        public TBEnemyList TBEnemyList { get; set; }
        public TBSceneInfo TBSceneInfo { get; set; }
        public TBTowerInfo TBTowerInfo { get; set; }

        private void Awake()
        {
            var tables = new System.Collections.Generic.Dictionary<string, object>();
            StartCoroutine(Reader("tbenemydata", (node) => {
                TBEnemyData = new TBEnemyData(node); 
                tables.Add("TBEnemyData", TBEnemyData);
                TBEnemyData.Resolve(tables);
            }));
            StartCoroutine(Reader("tbrounddata", (node) => {
                TBRoundData = new TBRoundData(node);
                tables.Add("TBRoundData", TBRoundData);
                TBRoundData.Resolve(tables);
            }));
            StartCoroutine(Reader("tbenemylist", (node) => {
                TBEnemyList = new TBEnemyList(node);
                tables.Add("TBEnemyList", TBEnemyList);
                TBEnemyList.Resolve(tables);
            }));
            StartCoroutine(Reader("tbsceneinfo", (node) => {
                TBSceneInfo = new TBSceneInfo(node);
                tables.Add("TBSceneInfo", TBSceneInfo);
                TBSceneInfo.Resolve(tables);
            }));
            StartCoroutine(Reader("tbtowerinfo", (node) => {
                TBTowerInfo = new TBTowerInfo(node);
                tables.Add("TBTowerInfo", TBTowerInfo);
                TBTowerInfo.Resolve(tables);
            }));
        }

        private IEnumerator Reader(string fileName, Action<JSONNode> callback)
        {
            string path = FTProjectUtils.StreamingAssetsPathPath + "/json/" + fileName + ".json";
            WWW www = new WWW(path);
            while (www.isDone == false)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
            string data = www.text;
            JSONNode node = JSONNode.Parse(data);
            callback(node);
            //Debug.LogError(data);
        }

        public void TranslateText(System.Func<string, string, string> translator)
        {
            TBEnemyData.TranslateText(translator);
            TBRoundData.TranslateText(translator);
            TBEnemyList.TranslateText(translator);
            TBSceneInfo.TranslateText(translator);
            TBTowerInfo.TranslateText(translator);
        }
    }

}

                