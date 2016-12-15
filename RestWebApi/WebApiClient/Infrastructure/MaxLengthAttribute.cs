using System.ComponentModel.DataAnnotations;

namespace WebApiClient.Infrastructure
{
    public class MaxLengthAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string str = (string)value;

            return str.Length < 2000;
        }
    }
}