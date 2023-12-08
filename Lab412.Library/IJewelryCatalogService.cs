namespace Lab412.Library
{
    internal interface IJewelryCatalogService
    {
        // Добавление данных о новом ювелирном изделии
        public int AddData(string jewelryName, string category, string material, int manufacturerId);

        // Обновление данных существующего ювелирного изделия
        public int UpdateData(int jewelryId, string updatedName, string updatedCategory, string updatedMaterial);

        // Чтение данных о ювелирном изделии по его названию или категории
        public int ReadData(string jewelryNameOrCategory);

        // Удаление данных ювелирного изделия по его названию
        public int RemoveData(string jewelryName);
    }
}
