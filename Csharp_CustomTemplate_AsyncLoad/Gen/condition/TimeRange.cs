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



namespace cfg.condition
{ 

public sealed partial class TimeRange :  condition.Condition 
{
    public TimeRange(JSONNode _json)  : base(_json) 
    {
        { if(!_json["date_time_range"].IsObject) { throw new SerializationException(); }  DateTimeRange = common.DateTimeRange.DeserializeDateTimeRange(_json["date_time_range"]);  }
        PostInit();
    }

    public TimeRange(common.DateTimeRange date_time_range )  : base() 
    {
        this.DateTimeRange = date_time_range;
        PostInit();
    }

    public static TimeRange DeserializeTimeRange(JSONNode _json)
    {
        return new condition.TimeRange(_json);
    }

    public common.DateTimeRange DateTimeRange { get; private set; }

    public const int __ID__ = 1069033789;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        DateTimeRange?.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
        DateTimeRange?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "DateTimeRange:" + DateTimeRange + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}