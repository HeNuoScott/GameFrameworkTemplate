// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
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
        string extension = Path.GetExtension(path);//获取后缀名称
        string filename = Path.GetFileName(path);//获取文件名称

        // 排除 通过代买生成的 数据表文件
        if (extension == ".cs" && !filename.StartsWith("DR"))
        {
            string allText = File.ReadAllText(path);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("// -----------------------------------------------");
            stringBuilder.AppendLine("// Copyright © GameFramework. All rights reserved.");
            stringBuilder.AppendLine(string.Format("// CreateTime: {0}/{1}/{2}   {3}:{4}:{5}",
                System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second));
            stringBuilder.AppendLine("// -----------------------------------------------");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("using GameEntry = GameFrame.Main.GameEntry;");
            allText = stringBuilder.ToString() + allText;
            File.WriteAllText(path, allText);
        }
    }
}