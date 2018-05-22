using MetaData.Data;
using RGiesecke.PlainCsv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MetaData
{
    //using CachedBaseMetaData = Dictionary<string, BaseMetaData>;

    using CachedIntTable = Dictionary<int, MetaDataGroup>;
    using CachedStringTable = Dictionary<string, Dictionary<string, MetaDataGroup>>;

    public class MetaDataMgr
    {
        static MetaDataMgr inst;
        public static MetaDataMgr Inst {
            get {
                if (inst == null)
                    inst = new MetaDataMgr();
                return inst;
            }
        }

        Dictionary<string, CachedTable> tableMetaDatas = new Dictionary<string, CachedTable>();

        class CachedTable
        {
            public CachedIntTable cachedIntTable = new CachedIntTable();
            public CachedStringTable cachedStringTable = new CachedStringTable();
        }

        public CachedIntTable GetTable(string tableName)
        {
            if (tableMetaDatas.ContainsKey(tableName))
                return tableMetaDatas[tableName].cachedIntTable;

            return null;
        }

        public MetaDataGroup GetMetaDataGroup(string tableName, int index)
        {
            if (!tableMetaDatas.ContainsKey(tableName))
                return null;

            if (!tableMetaDatas[tableName].cachedIntTable.ContainsKey(index))
                return null;

            return tableMetaDatas[tableName].cachedIntTable[index];
        }

        public void InitCSVMetaData(string namespaceStr, string path)
        {
            var csvList = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assm => assm.GetTypes())
                .Where(t => t.Namespace == namespaceStr);

            var csvClassDict = csvList.Where(t => t.IsClass == true)
                .OrderBy(t => t.Name)
                .ToDictionary(x => x.Name, x => x);

            //전체 파일의 주소를 확보
            var dirPath = System.Environment.CurrentDirectory + @path;
            //var dirPath = System.Environment.CurrentDirectory + @"\..\CsvDataTable";
            var di = new DirectoryInfo(dirPath);

            var parser = new PlainCsvReader();
            foreach (var fileInfo in di.GetFiles()) {
                var csv = File.ReadAllText(fileInfo.FullName);
                var result = parser.CsvToDictionaries(csv).ToList();

                var tableNames = fileInfo.Name.Split('.');
                tableMetaDatas[tableNames[0]] = new CachedTable();
                var twoTypeMetaDatas = tableMetaDatas[tableNames[0]];

                bool isFirst = true;
                int index = 0;
                foreach (var row in result) {
                    foreach (var info in row) {

                        var match = Regex.Match(info.Value, @"\d");

                        if (isFirst) {
                            index = Convert.ToInt32(info.Value);
                            twoTypeMetaDatas.cachedIntTable.Add(index, new MetaDataGroup(index));
                            isFirst = false;
                            continue;
                        } else {
                            var metaDataGroup = twoTypeMetaDatas.cachedIntTable[index];


                            if (!match.Success || match.Index != match.Length - 1) { //string
                                metaDataGroup.SetBaseData(info.Key, new BaseData(strValue: info.Value));
                            } else {
                                metaDataGroup.SetBaseData(info.Key, new BaseData(countValue: Convert.ToDouble(info.Value)));
                            }
                        }
                    }

                    isFirst = true;
                }
            }
        }
    }
}