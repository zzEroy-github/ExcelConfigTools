using System;
using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;

namespace ExcelConfigTest
{
    class ExcelConfigTool
    {
        //配置Excel表存放目录
        static string excelDir = "..\\..\\ExcelFiles"; //执行路径以编译生成的.dll文件为根路径

        //导出的配置二进制文件目录
        static string exportBinaryDir = "..\\..\\ExportFiles\\binary";

        //导出的配置CShape类目录
        static string exportCSDir = "..\\..\\ExportFiles\\cs";

        //配置类的Template文件路径
        static string templatePath = "..\\..\\Template.txt";

        static string templateText = "";

        public static void StartUp()
        {
            
            //读取template文本
            using(StreamReader sr = new StreamReader(templatePath))
            {
                templateText = sr.ReadToEnd();
            }

            string[] excelFiles = Directory.GetFiles(excelDir, "*.xlsx");   //excel文件名称

            for (int i = 0; i < excelFiles.Length; i++)  //遍历目录内所有excel表
            {
                string fullExcelFileName = excelFiles[i];  //完整文件名，包含路径
                string[] fullExcelFileNameSplit = fullExcelFileName.Split('\\');  
                string excelFileName = fullExcelFileNameSplit[fullExcelFileNameSplit.Length - 1]; //文件名，索引^1表示倒数第1（C# 8.0以下不支持）

                if (!excelFileName.StartsWith("~$"))  //如果excel表在打开状态，其副本前缀为~$，需要跳过这个副本文件
                {
                    //复制一份excel表，防止excel表已打开导致占用报错
                    string copyFullExcelFileName = fullExcelFileName + ".copy";
                    File.Copy(fullExcelFileName, copyFullExcelFileName);
                    FileStream stream = File.Open(copyFullExcelFileName, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    //Console.WriteLine("文件名称:" + fullExcelFileName + " 行数：" + excelDataReader.RowCount + "  列数：" + excelDataReader.FieldCount);

                    // 忽略以#开头的excel文件
                    if (excelFileName.StartsWith("#"))
                    {
                        Console.WriteLine($"{fullExcelFileName} 被跳过");
                        continue;
                    }

                    if (excelDataReader.RowCount <= 2)
                    {
                        Console.WriteLine($"文件 {excelFileName} 内容不足3行，不进行导表操作！");
                    }
                    else
                    {
                        List<string[]> excelData = new List<string[]>();   //存储excel表数据的List
                        for (int j = 0; j < excelDataReader.RowCount; j++) //遍历表的所有行
                        {
                            excelDataReader.Read();
                            string[] rowData = new string[excelDataReader.FieldCount]; //存储一行的数据
                            for (int k = 0; k < excelDataReader.FieldCount; k++)       //遍历行的所有值
                            {

                                if (excelDataReader.GetValue(k) == null)
                                {
                                    rowData[k] = "";
                                }
                                else
                                {
                                    rowData[k] = excelDataReader.GetValue(k).ToString();
                                    //Console.WriteLine("存储每一行数据 其中一个：" + excelDataReader.GetValue(k).ToString());
                                }
                            }
                            excelData.Add(rowData);
                        }
                        CreateDynamicsType(excelData, excelFileName);
                        excelData.Clear();
                    }
                    stream.Close();
                    File.Delete(copyFullExcelFileName);
                }
            }
        }

        //以excel表数据动态创建类
        public static void CreateDynamicsType(List<string[]> data, string fileName)
        {
            string mainTypeName = fileName.Split('.')[0];       //主类类名，用excel文件名
            string dataTypeName = mainTypeName + "ConfigData";  //数据类名
            string mainTypeFieldName = "data";                  //主类数据字段名，结构为List<dataTypeName> data

            string[] dataTypes = data[0];     //第一行是数据类型
            string[] fieldNames = data[1];    //第二行是字段名

            string replaceConfigDataStr = "\n"; //用于导出配置类文件的ConfigData文本

            Console.WriteLine($"正在导出表type名：{mainTypeName}");

            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("ConfigAssembly"), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("ConfigModule");
            TypeBuilder dataClassBuilder = moduleBuilder.DefineType(dataTypeName, TypeAttributes.Public | TypeAttributes.Serializable);
            
            //创建动态数据类的字段
            for(int i = 0; i < fieldNames.Length; i++)
            {
                string dataType = dataTypes[i];  //数据类型
                string field = fieldNames[i];    //字段名
                
                Type fieldType = GetSystemDataType(dataType);
                if (fieldType == null)
                {
                    Console.WriteLine($"错误：表 {fieldNames} 中第{i + 1}个字段的类型 {dataType} 不受支持，无法导出！");
                }
                else
                {
                    dataClassBuilder.DefineField(field, fieldType, FieldAttributes.Public);
                    //Console.WriteLine($"表{fileName} 成功创建类型为{dataType}的字段{field}");
                }

                replaceConfigDataStr = replaceConfigDataStr + string.Format($"    public {dataType.ToLower()} {field};\n");
            }
            Type dataClass = dataClassBuilder.CreateType();


            //实例化数据类并写入数据
            List<object> dataList = new List<object>(); //存储数据的List
            Dictionary<int, object> dataDict = new Dictionary<int, object>(); //存储数据的字典

            for (int i = 2; i < data.Count; i++)        //从第3行开始，遍历数据行
            {
                int id = -1;
                Object dataClassInst = Activator.CreateInstance(dataClass);
                for (int j = 0; j < fieldNames.Length; j++) //遍历字段名
                {
                    Type valueType = GetSystemDataType(data[0][j]);
                    object value = Convert.ChangeType(data[i][j], valueType);
                    if (fieldNames[j].ToLower() == "id")
                    {
                        id = (int)value;
                    }
                    //Console.WriteLine($"写入动态类实例 行数：{i + 1}  字段名：{fieldNames[j]} 字段类型：{valueType}  数据：{value}  数据类型：{value.GetType()}");

                    FieldInfo dataClassFieldInfo = dataClass.GetField(fieldNames[j]);
                    dataClassFieldInfo.SetValue(dataClassInst, value);
                }
                dataDict.Add(id, dataClassInst);
                
            }


            // 创建主类，将dataDict保存到主类中
            TypeBuilder mainClassBuilder = moduleBuilder.DefineType(mainTypeName, TypeAttributes.Public | TypeAttributes.Serializable);
            mainClassBuilder.DefineField(mainTypeFieldName, typeof(Dictionary<int, object>), FieldAttributes.Public);
            Type mainType = mainClassBuilder.CreateType();
            Object instance = Activator.CreateInstance(mainType);
            FieldInfo fieldInfo = mainType.GetField(mainTypeFieldName);
            fieldInfo.SetValue(instance, dataDict);
            //Console.WriteLine($"写入实例dataList长度={dataDict.Count}  查看值：{fieldInfo.Name}  查看其他：{fieldInfo.ToString()}");
            

            ExportBinary(instance);
            dataDict.Clear();

            string[] ExportCShapeFileParam = new string[] { mainType.Name, replaceConfigDataStr };
            ExportCShapeFile(ExportCShapeFileParam);
        }

        //获取系统数据类型的类
        public static Type GetSystemDataType(string typeName)
        {
            typeName = typeName.ToLower();
            Type type = null;
            switch (typeName)
            {
                case "int":
                    type = Type.GetType("System.Int32");
                    break;

                case "string":
                    type = Type.GetType("System.String");
                    break;

                case "bool":
                    type = Type.GetType("System.Boolean");
                    break;

                default:
                    break;
            }
            return type;
        }

        //序列化，导出二进制文件
        public static void ExportBinary(object dataObj)
        {
            string typeName = dataObj.GetType().Name;
            string fileName = typeName + ".dat";
            Console.WriteLine("导出二进制文件 文件名：" + fileName);

            FileStream stream = new FileStream(Path.Combine(exportBinaryDir, fileName), FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, dataObj);
            stream.Close();
        }

        //导出CS类文件
        public static void ExportCShapeFile(string[] text)
        {
            //text  --[0] 主类名  --[1]数据类内容
            string mainClsName = text[0];
            string configDataText = text[1];

            string replace_1 = templateText.Replace("template_mainClass", mainClsName); //Replace方法不会改变原字符串
            string replace_2 = replace_1.Replace("template_configDataText", configDataText);

            string exportCSfile = Path.Combine(exportCSDir, mainClsName + ".cs");
            using(StreamWriter sw = new StreamWriter(exportCSfile))
            {
                sw.Write(replace_2);
                Console.WriteLine($"成功导出配置类文件:{mainClsName + ".cs"}");
            }
        }
    }
}
