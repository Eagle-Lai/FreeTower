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

public sealed partial class TbEquip
{
    private readonly Dictionary<int, Equip> _dataMap;
    private readonly List<Equip> _dataList;
    
    public TbEquip(JSONNode _json)
    {
        _dataMap = new Dictionary<int, Equip>();
        _dataList = new List<Equip>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = Equip.DeserializeEquip(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, Equip> DataMap => _dataMap;
    public List<Equip> DataList => _dataList;

    public Equip GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Equip Get(int key) => _dataMap[key];
    public Equip this[int key] => _dataMap[key];

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