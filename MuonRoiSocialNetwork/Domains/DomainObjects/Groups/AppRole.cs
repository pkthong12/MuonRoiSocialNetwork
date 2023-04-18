using MuonRoi.Social_Network.Roles;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using BaseConfig.EntityObject.EntityObject;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using BaseConfig.EntityObject.Entity;

namespace MuonRoiSocialNetwork.Domains.DomainObjects.Groups
{
    /// <summary>
    /// AppRole
    /// </summary>
    public class AppRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Group id
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// GroupUserMember
        /// </summary>
        public GroupUserMember? GroupUserMember { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        protected List<ErrorResult> _errorMessages = new();

        /// <summary>
        /// ErrorMessages
        /// </summary>
        [JsonIgnore]
        public IReadOnlyCollection<ErrorResult> ErrorMessages => _errorMessages;
        /// <summary>
        /// GetAssembly
        /// </summary>
        /// <returns></returns>
        public Assembly? GetAssembly()
        {
            return GetType().Assembly;
        }
        /// <summary>
        /// IsValid
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            ValidationContext validationContext = new(this, null, null);
            List<ValidationResult> list = new();
            if (!Validator.TryValidateObject(this, validationContext, list, validateAllProperties: true))
            {
                foreach (ValidationResult item in list)
                {
                    ErrorResult errorResult = new ErrorResult
                    {
                        ErrorCode = item.ErrorMessage
                    };
                    errorResult.ErrorMessage = Helpers.GetErrorMessage(item.ErrorMessage, GetAssembly());
                    foreach (string memberName in item.MemberNames)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        PropertyInfo property = validationContext.ObjectType.GetProperty(memberName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        object value = property.GetValue(validationContext.ObjectInstance, null);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        errorResult.ErrorValues.Add(Helpers.GenerateErrorResult(memberName, value));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }

                    _errorMessages.Add(errorResult);
                }
            }

            return _errorMessages.Count == 0;
        }
    }
}
