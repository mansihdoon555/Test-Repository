using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;
using Microsoft.Ajax.Utilities;

namespace _729solutions.Web.Models
{
    [MetadataType(typeof(ProductEntityMetadata))]
    public partial class ProductEntity
    {
    }
    public class ProductEntityMetadata
    {
        [Display(Name = "ID")]
        //[Range(1, double.MaxValue, ErrorMessage = "Only integers are allowed")]
        public int ID { get; set; }
        
        [Display(Name ="Name")]
        //[RequiredIf("Active", true, ErrorMessageResourceName = "Active", ErrorMessageResourceType = typeof(ResourceStrings))]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Name can contain only alphabets and spaces")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "{0} should be between 5 to 30 Character long")]
        public string Name { get; set; }

        [Display(Name ="Price")]
        [Range(1, double.MaxValue, ErrorMessage = "Price can not be 0 or negative")]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        [MaxLength(2048, ErrorMessage = "Description cannot be more than 2048 characters long")]
        public string Description { get; set; }

        [Display(Name = "Active")]
        [Required(ErrorMessage = "Please select Status")]
        public bool Active { get; set; }

        [Display(Name = "Stock")]
        [Range(1, double.MaxValue, ErrorMessage = "Stock cannot be 0 or negative")]
        public int Stock { get; set; }

        [Display(Name = "Next Revision Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Next revision date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CurrentDate(ErrorMessage = "Next revision date should be greater than current date")]
        public DateTime? NextRevisionDate { get; set; }
    }
    
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt > DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }

    public class RequiredIfAttribute : RequiredAttribute
    {
        private String PropertyName { get; set; }
        private Object DesiredValue { get; set; }

        public RequiredIfAttribute(String propertyName, Object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue.ToString() == DesiredValue.ToString())
            {
                ValidationResult result = base.IsValid(value, context);
                return result;
            }
            return ValidationResult.Success;
        }
    }
}