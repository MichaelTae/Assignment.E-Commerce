﻿namespace Assignment.StoreApi.Models.CreateModels
{
    public class CreateProductModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public int SubCategoryId { get; set; }
    }
}
