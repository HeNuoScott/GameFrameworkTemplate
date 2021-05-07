// -----------------------------------------------
// Copyright © HeNuo. All rights reserved.
// CreateTime: 2020/8/4   10:13:45
// -----------------------------------------------
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class CustomScriptsTemplate : UnityEditor.AssetModificationProcessor
{
    // 当添加脚本时 调用
    private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string allText = File.ReadAllText(path);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("// -----------------------------------------------");
            stringBuilder.AppendLine("// Copyright © Sirius. All rights reserved.");
            stringBuilder.AppendLine(string.Format("// CreateTime: {0}/{1}/{2}   {3}:{4}:{5}",
                System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second));
            stringBuilder.AppendLine("// -----------------------------------------------");
            allText = stringBuilder.ToString() + allText;
            File.WriteAllText(path, allText);
        }
    }
}