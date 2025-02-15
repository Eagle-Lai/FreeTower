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



namespace cfg.common
{ 

public sealed partial class GlobalConfig :  Bright.Config.BeanBase 
{
    public GlobalConfig(JSONNode _json) 
    {
        { if(!_json["bag_capacity"].IsNumber) { throw new SerializationException(); }  BagCapacity = _json["bag_capacity"]; }
        { if(!_json["bag_capacity_special"].IsNumber) { throw new SerializationException(); }  BagCapacitySpecial = _json["bag_capacity_special"]; }
        { if(!_json["bag_temp_expendable_capacity"].IsNumber) { throw new SerializationException(); }  BagTempExpendableCapacity = _json["bag_temp_expendable_capacity"]; }
        { if(!_json["bag_temp_tool_capacity"].IsNumber) { throw new SerializationException(); }  BagTempToolCapacity = _json["bag_temp_tool_capacity"]; }
        { if(!_json["bag_init_capacity"].IsNumber) { throw new SerializationException(); }  BagInitCapacity = _json["bag_init_capacity"]; }
        { if(!_json["quick_bag_capacity"].IsNumber) { throw new SerializationException(); }  QuickBagCapacity = _json["quick_bag_capacity"]; }
        { if(!_json["cloth_bag_capacity"].IsNumber) { throw new SerializationException(); }  ClothBagCapacity = _json["cloth_bag_capacity"]; }
        { if(!_json["cloth_bag_init_capacity"].IsNumber) { throw new SerializationException(); }  ClothBagInitCapacity = _json["cloth_bag_init_capacity"]; }
        { if(!_json["cloth_bag_capacity_special"].IsNumber) { throw new SerializationException(); }  ClothBagCapacitySpecial = _json["cloth_bag_capacity_special"]; }
        { var _j = _json["bag_init_items_drop_id"]; if (_j.Tag != JSONNodeType.None && _j.Tag != JSONNodeType.NullValue) { { if(!_j.IsNumber) { throw new SerializationException(); }  BagInitItemsDropId = _j; } } else { BagInitItemsDropId = null; } }
        { if(!_json["mail_box_capacity"].IsNumber) { throw new SerializationException(); }  MailBoxCapacity = _json["mail_box_capacity"]; }
        { if(!_json["damage_param_c"].IsNumber) { throw new SerializationException(); }  DamageParamC = _json["damage_param_c"]; }
        { if(!_json["damage_param_e"].IsNumber) { throw new SerializationException(); }  DamageParamE = _json["damage_param_e"]; }
        { if(!_json["damage_param_f"].IsNumber) { throw new SerializationException(); }  DamageParamF = _json["damage_param_f"]; }
        { if(!_json["damage_param_d"].IsNumber) { throw new SerializationException(); }  DamageParamD = _json["damage_param_d"]; }
        { if(!_json["role_speed"].IsNumber) { throw new SerializationException(); }  RoleSpeed = _json["role_speed"]; }
        { if(!_json["monster_speed"].IsNumber) { throw new SerializationException(); }  MonsterSpeed = _json["monster_speed"]; }
        { if(!_json["init_energy"].IsNumber) { throw new SerializationException(); }  InitEnergy = _json["init_energy"]; }
        { if(!_json["init_viality"].IsNumber) { throw new SerializationException(); }  InitViality = _json["init_viality"]; }
        { if(!_json["max_viality"].IsNumber) { throw new SerializationException(); }  MaxViality = _json["max_viality"]; }
        { if(!_json["per_viality_recovery_time"].IsNumber) { throw new SerializationException(); }  PerVialityRecoveryTime = _json["per_viality_recovery_time"]; }
        PostInit();
    }

    public GlobalConfig(int bag_capacity, int bag_capacity_special, int bag_temp_expendable_capacity, int bag_temp_tool_capacity, int bag_init_capacity, int quick_bag_capacity, int cloth_bag_capacity, int cloth_bag_init_capacity, int cloth_bag_capacity_special, int? bag_init_items_drop_id, int mail_box_capacity, float damage_param_c, float damage_param_e, float damage_param_f, float damage_param_d, float role_speed, float monster_speed, int init_energy, int init_viality, int max_viality, int per_viality_recovery_time ) 
    {
        this.BagCapacity = bag_capacity;
        this.BagCapacitySpecial = bag_capacity_special;
        this.BagTempExpendableCapacity = bag_temp_expendable_capacity;
        this.BagTempToolCapacity = bag_temp_tool_capacity;
        this.BagInitCapacity = bag_init_capacity;
        this.QuickBagCapacity = quick_bag_capacity;
        this.ClothBagCapacity = cloth_bag_capacity;
        this.ClothBagInitCapacity = cloth_bag_init_capacity;
        this.ClothBagCapacitySpecial = cloth_bag_capacity_special;
        this.BagInitItemsDropId = bag_init_items_drop_id;
        this.MailBoxCapacity = mail_box_capacity;
        this.DamageParamC = damage_param_c;
        this.DamageParamE = damage_param_e;
        this.DamageParamF = damage_param_f;
        this.DamageParamD = damage_param_d;
        this.RoleSpeed = role_speed;
        this.MonsterSpeed = monster_speed;
        this.InitEnergy = init_energy;
        this.InitViality = init_viality;
        this.MaxViality = max_viality;
        this.PerVialityRecoveryTime = per_viality_recovery_time;
        PostInit();
    }

    public static GlobalConfig DeserializeGlobalConfig(JSONNode _json)
    {
        return new common.GlobalConfig(_json);
    }

    /// <summary>
    /// 背包容量
    /// </summary>
    public int BagCapacity { get; private set; }
    public int BagCapacitySpecial { get; private set; }
    public int BagTempExpendableCapacity { get; private set; }
    public int BagTempToolCapacity { get; private set; }
    public int BagInitCapacity { get; private set; }
    public int QuickBagCapacity { get; private set; }
    public int ClothBagCapacity { get; private set; }
    public int ClothBagInitCapacity { get; private set; }
    public int ClothBagCapacitySpecial { get; private set; }
    public int? BagInitItemsDropId { get; private set; }
    public bonus.DropInfo BagInitItemsDropId_Ref { get; private set; }
    public int MailBoxCapacity { get; private set; }
    public float DamageParamC { get; private set; }
    public float DamageParamE { get; private set; }
    public float DamageParamF { get; private set; }
    public float DamageParamD { get; private set; }
    public float RoleSpeed { get; private set; }
    public float MonsterSpeed { get; private set; }
    public int InitEnergy { get; private set; }
    public int InitViality { get; private set; }
    public int MaxViality { get; private set; }
    public int PerVialityRecoveryTime { get; private set; }

    public const int __ID__ = -848234488;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        this.BagInitItemsDropId_Ref = this.BagInitItemsDropId != null ? (_tables["bonus.TbDrop"] as  bonus.TbDrop).GetOrDefault(BagInitItemsDropId.Value) : null;
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "BagCapacity:" + BagCapacity + ","
        + "BagCapacitySpecial:" + BagCapacitySpecial + ","
        + "BagTempExpendableCapacity:" + BagTempExpendableCapacity + ","
        + "BagTempToolCapacity:" + BagTempToolCapacity + ","
        + "BagInitCapacity:" + BagInitCapacity + ","
        + "QuickBagCapacity:" + QuickBagCapacity + ","
        + "ClothBagCapacity:" + ClothBagCapacity + ","
        + "ClothBagInitCapacity:" + ClothBagInitCapacity + ","
        + "ClothBagCapacitySpecial:" + ClothBagCapacitySpecial + ","
        + "BagInitItemsDropId:" + BagInitItemsDropId + ","
        + "MailBoxCapacity:" + MailBoxCapacity + ","
        + "DamageParamC:" + DamageParamC + ","
        + "DamageParamE:" + DamageParamE + ","
        + "DamageParamF:" + DamageParamF + ","
        + "DamageParamD:" + DamageParamD + ","
        + "RoleSpeed:" + RoleSpeed + ","
        + "MonsterSpeed:" + MonsterSpeed + ","
        + "InitEnergy:" + InitEnergy + ","
        + "InitViality:" + InitViality + ","
        + "MaxViality:" + MaxViality + ","
        + "PerVialityRecoveryTime:" + PerVialityRecoveryTime + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
