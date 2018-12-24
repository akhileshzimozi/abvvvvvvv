using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Blabyap.Common.Model;
using System.Collections.Generic;

namespace Blabyapp.API.ViewModels
{
    // Models used as parameters to AccountController actions.
   
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "DisplayName")]
        public string DisplayName { get; set; }

       [Required]
        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public static implicit operator RegisterInfoModel(RegisterBindingModel registerMdl)
        {
            return new RegisterInfoModel()
            {
                DisplayName = registerMdl.DisplayName,
                Birthday = registerMdl.Birthday,
                ConfirmPassword = registerMdl.ConfirmPassword,
                Password = registerMdl.ConfirmPassword,
                Email = registerMdl.Email,
                FullName = registerMdl.FullName,
                ImageUrl = registerMdl.ImageUrl
            };
        }

        public static implicit operator RegisterBindingModel(RegisterInfoModel regInfo)
        {
            return new RegisterBindingModel()
            {
                DisplayName = regInfo.DisplayName,
                Birthday = regInfo.Birthday,
                ConfirmPassword = regInfo.ConfirmPassword,
                Password = regInfo.ConfirmPassword,
                Email = regInfo.Email,
                FullName = regInfo.FullName,
                ImageUrl = regInfo.ImageUrl
            };
        }
    }

        public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    
    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class HeaderValue
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

    public class Headers
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "values")]
        public List<HeaderValue> Values { get; set; }
    }

    public class ApiStandardProfileRequest
    {
        [JsonProperty(PropertyName = "headers")]
        public Headers Headers { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class Company
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "industry")]
        public string Industry { get; set; }

        [JsonProperty(PropertyName = "size")]
        public string Size { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

    public class Country
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class Location
    {

        [JsonProperty(PropertyName = "country")]
        public Country Country { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class StartDate
    {

        [JsonProperty(PropertyName = "month")]
        public int Month { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }
    }

    public class Experience
    {
        [JsonProperty(PropertyName = "company")]
        public Company Company { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "isCurrent")]
        public bool IsCurrent { get; set; }

        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public StartDate StartDate { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }

    public class Positions
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "values")]
        public List<Experience> Values { get; set; }
    }

    public class SiteStandardProfileRequest
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
    public class LinkedInProfile
    {
        [JsonProperty(PropertyName = "apiStandardProfileRequest")]
        public ApiStandardProfileRequest ApiStandardProfileRequest { get; set; }

        [JsonProperty(PropertyName = "emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "formattedName")]
        public string FormattedName { get; set; }

        [JsonProperty(PropertyName = "headline")]
        public string Headline { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "industry")]
        public string Industry { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }

        [JsonProperty(PropertyName = "maidenName")]
        public string MaidenName { get; set; }

        [JsonProperty(PropertyName = "numConnections")]
        public int NumConnections { get; set; }

        [JsonProperty(PropertyName = "numConnectionsCapped")]
        public bool NumConnectionsCapped { get; set; }

        [JsonProperty(PropertyName = "pictureUrl")]
        public string PictureUrl { get; set; }

        [JsonProperty(PropertyName = "Positions")]
        public Positions Positions { get; set; }

        [JsonProperty(PropertyName = "publicProfileUrl")]
        public string PublicProfileUrl { get; set; }

        [JsonProperty(PropertyName = "siteStandardProfileRequest")]
        public SiteStandardProfileRequest SiteStandardProfileRequest { get; set; }

        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }
    }

    public class TokenResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Access_token { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int Expires_in { get; set; }

    }
}
