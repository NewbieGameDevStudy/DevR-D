using CsvHelper;
using MetaData.Data;
using RGiesecke.PlainCsv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MetaData
{
    public class MetaDataMgr
    {
        //var testGet = MetaDataMgr.Inst.GetMetaData<ShopItem>(0);
        static MetaDataMgr inst;
        public static MetaDataMgr Inst {
            get {
                if (inst == null)
                    inst = new MetaDataMgr();
                return inst;
            }
        }

        Dictionary<Type, List<IBaseMeta>> cachedMetaDatas = new Dictionary<Type, List<IBaseMeta>>();        

        public void InitMetaData(string namespaceStr, string path)
        {
            var csvList = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assm => assm.GetTypes())
                .Where(t => t.Namespace == namespaceStr);

            var csvClassDict = csvList.Where(t => t.IsClass == true && t.IsGenericType == false && !t.Name.Contains("Base"))
                .OrderBy(t => t.Name)
                .ToDictionary(x => x.Name, x => new Tuple<Type, String>(x, x.BaseType.Name));

            var genericDict = csvList.Where(t => t.IsClass == true && t.IsGenericType == true && t.CustomAttributes.Count() == 1)
                .OrderBy(t => t.Name)
                .ToDictionary(x => x.CustomAttributes.FirstOrDefault().ConstructorArguments.FirstOrDefault().Value, x => x);

            var dirPath = Environment.CurrentDirectory + @path;
            var di = new DirectoryInfo(dirPath);

            var parser = new PlainCsvReader();
            foreach (var fileInfo in di.GetFiles()) {
                using (var reader = new StreamReader(fileInfo.FullName, System.Text.Encoding.GetEncoding(51949))) {
                    var tableNames = fileInfo.Name.Split('.');
                    if (!csvClassDict.ContainsKey(tableNames[0])) {
                        Console.WriteLine("Not Found MetaData Key : {0}", tableNames[0]);
                        continue;
                    }

                    var csvType = csvClassDict[tableNames[0]];
                    if (!genericDict.ContainsKey(csvType.Item2)) {
                        Console.WriteLine("Not Found MetaData BaseName Key : {0}", csvType);
                        continue;
                    }

                    var mapType = genericDict[csvType.Item2];

                    var csv = new CsvReader(reader);
                    csv.Read();
                    csv.Configuration.HasHeaderRecord = true;
                    var generic = Activator.CreateInstance(mapType.MakeGenericType(csvType.Item1), true);
                    csv.Configuration.RegisterClassMap(generic.GetType());
                    try {
                        cachedMetaDatas[csvType.Item1] = new List<IBaseMeta>();
                        do {
                            var record = csv.GetRecord(csvType.Item1);
                            cachedMetaDatas[csvType.Item1].Add((IBaseMeta)record);
                        } while (csv.Read());

                    } catch {
                        Console.WriteLine("Error Parse MetaData : {0}", fileInfo.Name);
                    }

                    Console.WriteLine("MetaData Load : {0}", fileInfo.Name);
                }
            }
        }

        public T GetMetaData<T>(int id)
        {
            var type = typeof(T);
            var objectList = cachedMetaDatas[type];
            var find = objectList.Find(x => x.Index == id);
            return (T)find;
        }

        public int GetMetaDataSize<T>()
        {
            var type = typeof(T);
            var objectList = cachedMetaDatas[type];

            return objectList.Count;
        }
    }
}