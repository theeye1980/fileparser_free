using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser
{
    /* Классы с входными данными */
    public class Root
    {
        public string marketplace_name { get; set; }
        public bool cache { get; set; }
    }

    public class Price_1_art_json
    {
        public bool cache { get; set; }
        public List<string> articles { get; set; }
        public string currency { get; set; }
        public int currency_code { get; set; }
    }
    public class Stocks_1_art_json
    {
        public bool cache { get; set; }
        public List<string> articles { get; set; }
        public string currency { get; set; }
        public int currency_code { get; set; }
    }
    public class Stocks_1_brand
    {
        public bool cache { get; set; }
        public string vendor_uuid { get; set; }
    }

    /* Классы с выходными данными */

    // *** Классы выходных данных метода маркетплейс 1С ***
    public class Datum
    {
        public string article { get; set; }
        public string show_artikul { get; set; }
        public double sale { get; set; }
        public double stock { get; set; }
        public double price { get; set; }
        public double price_sale { get; set; }
    }

    public class Root1
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
        public string marketplace_name { get; set; }
    }

    //Класс, чтобы десереализовать полученные данные по ценам от 1С
    public class Price_out
    {
        public string article { get; set; }
        public string sale { get; set; }
        public string price { get; set; }
        public string price_sale { get; set; }
        public string timestamp { get; set; }
        public string update_error { get; set; }
        public string vendor_uuid { get; set; }
        public string ЭтоПакет { get; set; }
    }
    //Класс, чтобы десереализовать полученные данные по остаткам от 1С

    public class Stocks_out
    {
        public string article { get; set; }
        public string shop_id { get; set; }
        public string stock { get; set; }
        public string vendor_uuid { get; set; }
        public string timestamp { get; set; }
        public string update_error { get; set; }
    }

    public class Brands_UUID
    {
        public string uuid { get; set; }
        public string name { get; set; }
        public string last_success_stocks_date { get; set; }
        public string last_success_price_date { get; set; }
    }

    // Классы для получения данных по остаткам по всему бренду из 1С
    public class Brand_stocks_out_Response
    {
        public string article { get; set; }
        public string shop_id { get; set; }
        public string stock { get; set; }
        public string vendor_uuid { get; set; }
        public string timestamp { get; set; }
        public string update_error { get; set; }
    }

    public class Brand_stocks_out
    {
        public string time { get; set; }
        public string timeout { get; set; }
        public string size { get; set; }
        public int records { get; set; }
        public string method { get; set; }
        public List<Brand_stocks_out_Response> response { get; set; }
    }

    // *** Классы для десеарилизации ***

    public class Single_price
    {
        public string article { get; set; }
        public int availible { get; set; }

        public int stocks_23 { get; set; }
        public int sum_oll_stocks { get; set; }

        public string vendor_name { get; set; }
        public double price { get; set; }
        public double price_sale { get; set; }
        public int published { get; set; }
        public int deleted { get; set; }
    }

    public class Site_prices
    {
        public List<Single_price> results { get; set; }
        public int total { get; set; }
    }

    //Классы для получения информации по брендам на сайте
    public class Object
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public object updatedon { get; set; }
        public object createdon { get; set; }
        public string total_prices { get; set; }
        public string total_stocks { get; set; }
    }

    public class sync1c_sync // Класс получения информации по количеству остатков по брендам на сайте
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<Object> @object { get; set; }
        public int code { get; set; }
    }

    //Класс получения списка артикулов

    public class Response
    {
        public object array { get; set; }
    }

    public class ListOf1CArts
    {
        public string method { get; set; }
        public Response response { get; set; }
    }
    public class PhotobankParts
    {

        public string[] folders = {
            "3D-модели",
            "PNG для примерки",
           // "WEB семинары",
           // "ВИДЕОБАНК",
           // "Выгрузки",
            "Инструкции по монтажу",
            "Инструкции по сборке",
            "Интерьеры-портфолио",
           // "Каталоги",
           // "Контент Instagram",
           // "Листовки для отдела продаж",
           // "ПРАЙС-ЛИСТЫ",
           // "Презентации",
           // "Проекты дизайнеров",
           // "Рекламные материалы",
            "Руководства по эксплуатации",
           // "Сертификаты",
           // "Спецификации продукции",
           // "Тех.характеристики",
            "Технические рисунки (JPG)",
            "Технические рисунки (схемы)",
            "Технические рисунки SVG (схемы)",
           // "Фирменный стиль",
            "ФОТОБАНК"
            //"Фотометрические данные"
        };




    }
    //Класс для получения данных от Моделей машинного обучения
    public class InternalTorchObj
    {
        public object isTorch { get; set; }
    }

    public class TorcheData
    {
        public bool success { get; set; }
        public string message { get; set; }
        public InternalTorchObj @object { get; set; }
        public int code { get; set; }
    }

    





}
namespace Dubli {
    //Класс для получения данных магазинов
    public class Stores
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<Object> @object { get; set; }
        public int code { get; set; }
    }

    public class Object
    {
        public int id { get; set; }
        public int page_id { get; set; }
        public int posting_day { get; set; }
        public int region_id { get; set; }
        public string createdon { get; set; }
        public object updatedon { get; set; }
        public bool active { get; set; }
        public bool partner { get; set; }
        public bool filter_show { get; set; }
        public string filter_name { get; set; }
        public string page { get; set; }
        public string region_name { get; set; }
        public string total_storages { get; set; }
        public string total_products { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public string contentType { get; set; }
        public string pagetitle { get; set; }
        public string longtitle { get; set; }
        public string description { get; set; }
        public string alias { get; set; }
        public bool alias_visible { get; set; }
        public string link_attributes { get; set; }
        public bool published { get; set; }
        public int pub_date { get; set; }
        public int unpub_date { get; set; }
        public int parent { get; set; }
        public bool isfolder { get; set; }
        public string introtext { get; set; }
        public bool richtext { get; set; }
        public int template { get; set; }
        public int menuindex { get; set; }
        public bool searchable { get; set; }
        public bool cacheable { get; set; }
        public int createdby { get; set; }
        public int editedby { get; set; }
        public string editedon { get; set; }
        public bool deleted { get; set; }
        public int deletedon { get; set; }
        public int deletedby { get; set; }
        public object publishedon { get; set; }
        public int publishedby { get; set; }
        public string menutitle { get; set; }
        public bool donthit { get; set; }
        public bool privateweb { get; set; }
        public bool privatemgr { get; set; }
        public int content_dispo { get; set; }
        public bool hidemenu { get; set; }
        public string class_key { get; set; }
        public string context_key { get; set; }
        public int content_type { get; set; }
        public string uri { get; set; }
        public int uri_override { get; set; }
        public int hide_children_in_tree { get; set; }
        public int show_in_tree { get; set; }
    }


}
