//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg
{ 

public sealed partial class TBEnemyData
{
    private readonly Dictionary<int, EnemyData> _dataMap;
    private readonly List<EnemyData> _dataList;
    
    public TBEnemyData(JSONNode _json)
    {
        _dataMap = new Dictionary<int, EnemyData>();
        _dataList = new List<EnemyData>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = EnemyData.DeserializeEnemyData(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, EnemyData> DataMap => _dataMap;
    public List<EnemyData> DataList => _dataList;

    public EnemyData GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public EnemyData Get(int key) => _dataMap[key];
    public EnemyData this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    
    partial void PostInit();
    partial void PostResolve();
}

}