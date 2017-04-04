using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Esthetic.Models
{
    public class ConfigurationModel
    {
        [DisplayName("Mostrar fotos en portada")]
        public bool ShowCoverGallery { get; set; }

        [DisplayName("Texto principal en portada")]
        public string CoverMainText { get; set; }

        [DisplayName("Texto descriptivo en portada")]
        public string CoverDescriptionText { get; set; }

        [DisplayName("Imagen de portada")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        [DisplayName("Usuario (o Fanpage) de Facebook")]
        public string FacebookFanpage { get; set; }

        [DisplayName("Usuario (o Fanpage) alternativa de Facebook")]
        public string FacebookFanpageAlternative { get; set; }

        [DisplayName("Usuario de Instagram")]
        public string InstagramUser { get; set; }

        [DisplayName("Usuario de Instagram alternativo")]
        public string InstagramUserAlternative { get; set; }

        [DisplayName("Usuario de Tweeter")]
        public string TweeterUser { get; set; }

        [DisplayName("Usuario de Tweeter alternativo")]
        public string TweeterUserAlternative { get; set; }

        [DisplayName("Cantidad de fotos en galería")]
        [Range(0, float.MaxValue, ErrorMessage = "Ingrese un número válido")]
        public int GalleryCount { get; set; }
    }
}