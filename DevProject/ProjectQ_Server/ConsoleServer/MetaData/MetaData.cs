using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Data
{
    public interface IBaseMeta
    {
        int Index { get; set; }
    }

    public class BaseMetaAttribute : Attribute
    {
        private string name;
        public BaseMetaAttribute(string name)
        {
            this.name = name;
        }

        public String Name {
            get { return name; }
            set { name = value; }
        }
    }

    public class BaseQuizMeta : IBaseMeta
    {
        public int Index { get; set; }
        public string QuestText { get; set; }
        public bool Result { get; set; }
        public int GetExp { get; set; }
        public int Difficult { get; set; }
    }

    public class BaseItemMeta : IBaseMeta
    {
        public int Index { get; set; }
        public string Desc { get; set; }
        public int ItemType { get; set; }
        public int Price { get; set; }
        public int GetExp { get; set; }
    }

    #region 퀴즈 데이터
    public class AnimalDataTable : BaseQuizMeta
    {

    }

    public class CommonDataTable : BaseQuizMeta
    {

    }

    public class EntertainmentDataTable : BaseQuizMeta
    {

    }

    public class GeneralDataTable : BaseQuizMeta
    {

    }

    public class HistoryDataTable : BaseQuizMeta
    {

    }

    public class ScienceDataTable : BaseQuizMeta
    {

    }

    public class SocietyDataTable : BaseQuizMeta
    {

    }

    public class SportsDataTable : BaseQuizMeta
    {

    }
    #endregion

    [BaseMeta("BaseQuizMeta")]
    public class QuizMapping<T> : ClassMap<T> where T : BaseQuizMeta
    {
        public QuizMapping()
        {
            Map(m => m.Index).Index(0);
            Map(m => m.QuestText).Index(1);
            Map(m => m.Result).Index(2);
            Map(m => m.GetExp).Index(3);
            Map(m => m.Difficult).Index(4);
        }
    }

    [BaseMeta("BaseItemMeta")]
    public class ItemMapping<T> : ClassMap<T> where T : BaseItemMeta
    {
        public ItemMapping()
        {
            Map(m => m.Index).Index(0);
            Map(m => m.Desc).Index(1);
            Map(m => m.ItemType).Index(2);
            Map(m => m.Price).Index(3);
            Map(m => m.GetExp).Index(4);
        }
    }

    #region 아이템 데이터
    public class ShopItem : BaseItemMeta
    {

    }
    #endregion


    //Map(m => m.Index).Index(0);
    //Map(m => m.Desc).Index(1);
    //Map(m => m.Result).Index(2);
    //Map(m => m.GetExp).ConvertUsing(row => new List<int> {
    //                row.GetField<int>(3),
    //                row.GetField<int>(4),
    //                row.GetField<int>(5),
    //            });
    //        Map(m => m.dict).ConvertUsing(row => new Dictionary<int, int> {

    //                { row.GetField<int>(3), row.GetField<int>(3) }

    //            });
}
