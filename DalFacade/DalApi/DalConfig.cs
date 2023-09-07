using System.Xml.Linq;

namespace DalApi;

static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, string> s_dalPackages;
    internal static Dictionary<string, string?> s_dalPackagesAttributes = new();

    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml") ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements() ?? throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);

        // getting the XElement that we choose from the dal-packages tag 
        var e = packages.Where(e => e.Name == s_dalName).ToList()[0];
        // putting the attribut "namespace" and "class" of the element in a Dictionary so we can accessit latter
        s_dalPackagesAttributes.Add(e.Attribute("namespace")?.Name.ToString(), e.Attribute("namespace")?.Value);
        s_dalPackagesAttributes.Add(e.Attribute("class")?.Name.ToString(), e.Attribute("class")?.Value);

    }
}
