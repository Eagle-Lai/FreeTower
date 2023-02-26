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



namespace cfg.ai
{ 

public sealed partial class UeWaitBlackboardTime :  ai.Task 
{
    public UeWaitBlackboardTime(JSONNode _json)  : base(_json) 
    {
        { if(!_json["blackboard_key"].IsString) { throw new SerializationException(); }  BlackboardKey = _json["blackboard_key"]; }
        PostInit();
    }

    public UeWaitBlackboardTime(int id, string node_name, System.Collections.Generic.List<ai.Decorator> decorators, System.Collections.Generic.List<ai.Service> services, bool ignore_restart_self, string blackboard_key )  : base(id,node_name,decorators,services,ignore_restart_self) 
    {
        this.BlackboardKey = blackboard_key;
        PostInit();
    }

    public static UeWaitBlackboardTime DeserializeUeWaitBlackboardTime(JSONNode _json)
    {
        return new ai.UeWaitBlackboardTime(_json);
    }

    public string BlackboardKey { get; private set; }

    public const int __ID__ = 1215378271;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "NodeName:" + NodeName + ","
        + "Decorators:" + Bright.Common.StringUtil.CollectionToString(Decorators) + ","
        + "Services:" + Bright.Common.StringUtil.CollectionToString(Services) + ","
        + "IgnoreRestartSelf:" + IgnoreRestartSelf + ","
        + "BlackboardKey:" + BlackboardKey + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}