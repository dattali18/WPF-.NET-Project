using static DalApi.DalConfig;
using System.Reflection;

namespace DalApi;

public static class Factory
{
    public static IDal? Get()
    {
        string dalType = s_dalName ?? throw new DalConfigException($"DAL name is not extracted from the configuration");
        string dal = s_dalPackages[dalType] ?? throw new DalConfigException($"Package for {dalType} is not found in packages list");
        Dictionary<string, string?> dalPackagesAttributes = s_dalPackagesAttributes ?? throw new DalConfigException($"The attributes of the elment {dal} was not loaded corectly");

        bool asAttributes = dalPackagesAttributes["namespace"] == string.Empty || dalPackagesAttributes["namespace"] == null || dalPackagesAttributes["class"] == string.Empty || dalPackagesAttributes["class"] == null;
        try
        {
            Assembly.Load(dal ?? throw new DalConfigException($"Package {dal} is null"));
        }
        catch (Exception)
        {
            throw new DalConfigException($"Failed to load {dal}.dll package");
        }
        Type? type;
        if (asAttributes)
            type = Type.GetType($"Dal.{dal}, {dal}")
                ?? throw new DalConfigException($"Class Dal.{dal} was not found in {dal}.dll");
        else
            type = Type.GetType($"{dalPackagesAttributes["namespace"]}.{dalPackagesAttributes["class"]}, {dalPackagesAttributes["class"]}")
                ?? throw new DalConfigException($"Class {dalPackagesAttributes["namespace"]}.{dalPackagesAttributes["class"]},  was not found in {dal}.dll");

        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) as IDal ?? throw new DalConfigException($"Class {dal} is not singleton or Instance property not found");
    }
}

