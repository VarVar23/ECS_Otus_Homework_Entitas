using UnityEditor;
using UnityEngine;

/// <summary>
/// Entitas 1.14.1 (DesperateDevs.Unity.Editor) ищет встроенные стили Unity по
/// историческим именам с опечаткой: "ToolbarSeachTextField" и "ToolbarSeachCancelButton".
/// В современных Unity они называются "ToolbarSearchTextField" / "ToolbarSearchCancelButton",
/// поэтому FindStyle возвращает null, GUILayout.TextField падает с NullReferenceException,
/// и отрисовка инспектора сущности обрывается целиком.
///
/// Скрипт добавляет в редакторные скины алиасы со старыми именами.
/// </summary>
[InitializeOnLoad]
public static class EntitasLegacySearchStyleFix
{
    const string LegacyTextField = "ToolbarSeachTextField";
    const string LegacyCancelButton = "ToolbarSeachCancelButton";

    static readonly string[] ModernTextFieldNames =
    {
        "ToolbarSearchTextField",
        "ToolbarSeachTextField",
        "SearchTextField"
    };

    static readonly string[] ModernCancelButtonNames =
    {
        "ToolbarSearchCancelButton",
        "ToolbarSeachCancelButton",
        "SearchCancelButton"
    };

    static EntitasLegacySearchStyleFix()
    {
        Patch(EditorSkin.Inspector);
        Patch(EditorSkin.Scene);
        Patch(EditorSkin.Game);
    }

    static void Patch(EditorSkin editorSkin)
    {
        GUISkin skin;
        try
        {
            skin = EditorGUIUtility.GetBuiltinSkin(editorSkin);
        }
        catch
        {
            return;
        }

        if (skin == null)
            return;

        var styles = skin.customStyles ?? new GUIStyle[0];
        var added = 0;

        if (!Has(styles, LegacyTextField))
        {
            styles = Append(styles, Alias(skin, ModernTextFieldNames, skin.textField, LegacyTextField));
            added++;
        }

        if (!Has(styles, LegacyCancelButton))
        {
            styles = Append(styles, Alias(skin, ModernCancelButtonNames, skin.button, LegacyCancelButton));
            added++;
        }

        if (added > 0)
            skin.customStyles = styles;
    }

    static bool Has(GUIStyle[] styles, string name)
    {
        foreach (var style in styles)
            if (style != null && style.name == name)
                return true;

        return false;
    }

    static GUIStyle Alias(GUISkin skin, string[] candidates, GUIStyle fallback, string name)
    {
        foreach (var candidate in candidates)
        {
            var found = skin.FindStyle(candidate);
            if (found != null)
                return new GUIStyle(found) { name = name };
        }

        return new GUIStyle(fallback) { name = name };
    }

    static GUIStyle[] Append(GUIStyle[] styles, GUIStyle style)
    {
        var result = new GUIStyle[styles.Length + 1];
        styles.CopyTo(result, 0);
        result[styles.Length] = style;
        return result;
    }
}
