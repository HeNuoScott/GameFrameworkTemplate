// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/4/26   10:44:26
// -----------------------------------------------

using UnityEditor;

namespace Sirius.Editor
{
    public sealed class ToolsMenu
    {
        //生成数据表
        [MenuItem("Tools/DataTables/Generate DataTables")]
        private static void GenerateDataTables()
        {
            DataTableCodeBinary.GenerateDataTables();
        }
        //配置Configs转Bytes
        [MenuItem("Tools/DataTables/ConfigsToBinary")]
        private static void SelectionConfigTextToBinary()
        {
            DataTableCodeBinary.ConfigTextToBinary();
        }
    }
}