using System;
using System.Collections.Generic;

namespace MetaData.Data
{
    public interface IProperty
    {
        string StrValue { get; }
        double CountValue { get; }
    }

    public class BaseData : IProperty
    {
        public enum ValueType
        {
            Value,
            String
        }

        public string StrValue => strValue;
        public double CountValue => countValue;

        string strValue;
        double countValue;

        public BaseData(string strValue = "", double countValue = 0)
        {
            this.strValue = strValue;
            this.countValue = countValue;
        }

    }

    public class MetaDataGroup
    {
        Dictionary<string, BaseData> baseDatas = new Dictionary<string, BaseData>();

        public int IndexId { get; private set; }

        public MetaDataGroup(int indexId)
        {
            IndexId = indexId;
        }

        public void SetBaseData(string baseName, BaseData data)
        {
            baseDatas.Add(baseName, data);
        }

        public string GetString(string baseName)
        {
            if (baseDatas.ContainsKey(baseName)) {
                var baseData = baseDatas[baseName];

                if (string.IsNullOrEmpty(baseData.StrValue)) {
                    return baseData.CountValue.ToString();
                }

                return baseData.StrValue;

            } else
                Console.WriteLine("{0}, Error not found : {1}, {2}", "GetString", IndexId, baseName);

            return null;
        }


        public int GetInt(string baseName)
        {
            if (baseDatas.ContainsKey(baseName)) {
                var baseData = baseDatas[baseName];

                if (baseData.CountValue != 0) {
                    return Convert.ToInt32(baseData.CountValue);
                }

                return Convert.ToInt32(baseData.StrValue);

            } else
                Console.WriteLine("{0}, Error not found : {1}, {2}", "GetInt", IndexId, baseName);

            return 0;
        }
    }
}
