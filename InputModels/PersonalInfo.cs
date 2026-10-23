using WestcoastEducation.Models;

namespace WestcoastEducation.InputModels;

public record PersonalInfo(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string SocialSecurityNumber,
    Address Address
);


