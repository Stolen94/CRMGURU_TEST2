//Интерфейс реализуется классами Country, Capital, Region
//Интерфейс предназначен для взаимодействия объектов классов с базой данных
namespace CRMGURU_TEST
{
    using System.Collections;
    using System.Data.SqlClient;

    public interface ISQL
    {
        ArrayList FindIfExist();//Поиск записи по заданному в реализации условию, считывание ее в ArrayList
        void InsertinDB();//Вставка объекта как записи в соответствующую таблицу
        ArrayList TransformResult(SqlDataReader dr); //Приведение считанной записи к виду объекта, и запись полученного объекта в ArrayList      
    }
}
