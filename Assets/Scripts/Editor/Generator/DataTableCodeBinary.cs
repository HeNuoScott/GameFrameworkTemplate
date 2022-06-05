// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/6   15:21:19
// -----------------------------------------------
using Sirius.Editor.DataTableTools;
using Sirius;
using GameFramework;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Sirius.Editor
{
    public class DataTableCodeBinary
    {
        public static void GenerateDataTables()
        {
            foreach (string dataTableName in Constant.DataTableName.DataTableNames)
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    UnityEngine.Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }

        public static void ConfigTextToBinary()
        {
            char[] DataSplitSeparators = new char[] { '\t' };
            char[] DataTrimSeparators = new char[] { '\"' };
            char[] DataLineSeparators = new char[] { '\r', '\n' };
            string configTextPath = AssetUtility.GetConfigAsset("DefaultConfig", false);
            string configBinaryPath = AssetUtility.GetConfigAsset("DefaultConfig", true);

            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(configTextPath);
            string[] texts = textAsset.text.Split(DataLineSeparators);

            try
            {
                using (FileStream fileStream = new FileStream(configBinaryPath, FileMode.Create))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(fileStream, Encoding.UTF8))
                    {
                        for (int j = 0; j < texts.Length; j++)
                        {
                            if (texts[j].Length <= 0 || texts[j][0] == '#')
                            {
                                continue;
                            }
                            string[] splitLine = texts[j].Split(DataSplitSeparators, System.StringSplitOptions.None);
                            for (int k = 0; k < splitLine.Length; k++)
                            {
                                if (k == 0 || k == 2) continue;
                                binaryWriter.Write(splitLine[k].Trim(DataTrimSeparators));
                            }

                        }
                    }
                }
                Debug.Log(Utility.Text.Format("Parse config  '{0}',Create.", configBinaryPath));
            }
            catch (System.Exception exception)
            {
                Debug.LogError(Utility.Text.Format("Parse config  '{0}',exception is  '{1}'", configBinaryPath, exception.Message));
            }

            AssetDatabase.Refresh();
        }
    }
}