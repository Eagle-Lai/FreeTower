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

public sealed partial class EnemyData :  Bright.Config.BeanBase 
{
    public EnemyData(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["speed"].IsNumber) { throw new SerializationException(); }  Speed = _json["speed"]; }
        { if(!_json["hp"].IsNumber) { throw new SerializationException(); }  Hp = _json["hp"]; }
        { if(!_json["name"].IsString) { throw new SerializationException(); }  Name = _json["name"]; }
        { if(!_json["type"].IsNumber) { throw new SerializationException(); }  Type = _json["type"]; }
        { if(!_json["interval"].IsNumber) { throw new SerializationException(); }  Interval = _json["interval"]; }
        { if(!_json["intervalType"].IsNumber) { throw new SerializationException(); }  IntervalType = _json["intervalType"]; }
        PostInit();
    }

    public EnemyData(int id, float speed, float hp, string name, int type, int interval, int intervalType ) 
    {
        this.Id = id;
        this.Speed = speed;
        this.Hp = hp;
        this.Name = name;
        this.Type = type;
        this.Interval = interval;
        this.IntervalType = intervalType;
        PostInit();
    }

    public static EnemyData DeserializeEnemyData(JSONNode _json)
    {
        return new EnemyData(_json);
    }

    public int Id { get; private set; }
    /// <summary>
    /// 速度
    /// </summary>
    public float Speed { get; private set; }
    /// <summary>
    /// 血量
    /// </summary>
    public float Hp { get; private set; }
    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 敌人类型
    /// </summary>
    public int Type { get; private set; }
    /// <summary>
    /// 出现间隔
    /// </summary>
    public int Interval { get; private set; }
    /// <summary>
    /// 生成间隔类型
    /// </summary>
    public int IntervalType { get; private set; }

    public const int __ID__ = 953306930;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Speed:" + Speed + ","
        + "Hp:" + Hp + ","
        + "Name:" + Name + ","
        + "Type:" + Type + ","
        + "Interval:" + Interval + ","
        + "IntervalType:" + IntervalType + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}