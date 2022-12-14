using UnityEditor;

public static class APICenterCreateScriptEditor
{
    private const string TPL_API_CENTER_SCRIPT_PATH = "TplScripts/APICenter/TplAPICenter.cs.txt";
    private const string TPL_API_BASE_SCRIPT_PATH = "TplScripts/APICenter/TplAPIBase.cs.txt";

    // find current file path
    private static string pathFinder
    {
        get
        {
            var g = AssetDatabase.FindAssets("t:Script APICenterCreateScriptEditor");
            return AssetDatabase.GUIDToAssetPath(g[0]);
        }
    }

    #region APICenter Script Create
    [MenuItem(itemName: "Assets/Create/OxGFrame/APICenter/TplScripts/TplAPIBase.cs (API)", isValidateFunction: false, priority: 51)]
    public static void CreateScriptTplAPIBase()
    {
        string currentPath = pathFinder;
        string finalPath = currentPath.Replace("APICenterCreateScriptEditor.cs", "") + TPL_API_BASE_SCRIPT_PATH;

        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(finalPath, "NewTplAPIBase.cs");
    }

    [MenuItem(itemName: "Assets/Create/OxGFrame/APICenter/TplScripts/TplAPICenter.cs (APICenter Manager)", isValidateFunction: false, priority: 51)]
    public static void CreateScriptTplAPICenter()
    {
        string currentPath = pathFinder;
        string finalPath = currentPath.Replace("APICenterCreateScriptEditor.cs", "") + TPL_API_CENTER_SCRIPT_PATH;

        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(finalPath, "NewTplAPICenter.cs");
    }
    #endregion
}