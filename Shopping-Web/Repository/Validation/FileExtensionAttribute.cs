using System.ComponentModel.DataAnnotations;

namespace Shopping_Web.Repository.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is FormFile file) {
            var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jgp", "png", "jpeg" };
                bool result = extension.Any(x => extension.EndsWith(x));
                if (!result)
                {
                    return new ValidationResult("Allowed extentions are jpg or ng of jpeg");
                }
            }
            return ValidationResult.Success;

        }
    }
}
