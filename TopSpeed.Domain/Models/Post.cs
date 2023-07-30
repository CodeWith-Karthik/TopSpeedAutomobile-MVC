using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Domain.ApplicationEnums;
using TopSpeed.Domain.Common;

namespace TopSpeed.Domain.Models
{
    public class Post : BaseModel
    {
        [Display(Name = "Brand")]
        public Guid BrandId { get; set; }

        [ValidateNever]
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        [Display(Name = "Vehicle Type")]
        public Guid VehicleTypeId { get; set; }

        [ValidateNever]
        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleType { get; set; }

        public string Name { get; set; }

        [Display(Name = "Select Engine/Fuel Type")]
        public EngineAndFuelType EngineAndFuelType { get; set; }

        [Display(Name = "Select Transmission Mode")]
        public Transmission Transmission { get; set; }

        public int Engine { get; set; }

        public int TopSpeed { get; set; }

        public int Mileage { get; set; }

        public int Range { get; set; }

        [Display(Name = "Seating Capacity")]
        public string SeatingCapacity { get; set; }

        [Display(Name = "Base Price")]
        public double PriceFrom { get; set; }

        [Display(Name = "Top-End Price")]
        public double PriceTo { get; set; }


        [Range(1, 5, ErrorMessage = "Rating Should be from 1 to 5 only")]
        public int Ratings { get; set; }

        [Display(Name = "Upload Vehicle Image")]
        public string VehicleImage { get; set; }
    }
}
