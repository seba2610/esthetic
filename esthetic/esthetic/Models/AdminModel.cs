﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Esthetic.Models
{
    public class AdminModel
    {
        public List<Category> Categories = new List<Category>();
        public Category Category = new Category();

        [DisplayName("Nombre")]
        public string NewCategoryName { get; set; }

        [DisplayName("Descripción")]
        public string NewCategoryDescription { get; set; }

        [DisplayName("Nombre")]
        public string EditCategoryName { get; set; }

        [DisplayName("Descripción")]
        public string EditCategoryDescription { get; set; }

        [DisplayName("Título")]
        public string EditImageTitle { get; set; }

        [DisplayName("Descripción")]
        public string EditImageDescription { get; set; }

        [DisplayName("Categoría superior")]
        public int? CategorySelected { get; set; }
        public List<int> CategoriesSelected { get; set; }

        public List<Image> Images = new List<Image>();
        public Image Image = new Image();

        public List<Image> NewImages = new List<Image>();

        public string ImageAbsolutePath = String.Empty;
        public string ImageThumbnailAbsolutePath = String.Empty;

        public Service Service = new Service();

        [DisplayName("Tipo de servicio")]
        public int ServiceTypeSelected { get; set; }

        public ServiceType ServiceType = new ServiceType();
        public Service NewService = new Service();

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre no puede ser vacío")]
        public string NewServiceName { get; set; }

        [DisplayName("Descripción")]
        public string NewServiceDescription { get; set; }

        [DisplayName("Costo")]
        public string NewServiceCost { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre no puede ser vacío")]
        public string EditServiceName { get; set; }

        [DisplayName("Descripción")]
        public string EditServiceDescription { get; set; }

        [DisplayName("Costo")]
        public string EditServiceCost { get; set; }

        [DisplayName("Tipo de servicio")]
        public int EditServiceType { get; set; }

        public List<Service> Services = new List<Service>();
        public List<ServiceType> ServicesType = new List<ServiceType>();
        public List<Feature> Features = new List<Feature>();

        [DisplayName("Activa")]
        public bool? ActiveFeature { get; set; }

        [DisplayName("Descripción")]
        public string DescriptionFeature { get; set; }
        public string EditFeatureDescription { get; set; }
        public bool EditFeatureActive { get; set; }
        public Feature Feature { get; set; }

        public AdminModel()
        {
            CategorySelected = -1;
        }
    }

    public class CategorySelection
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}