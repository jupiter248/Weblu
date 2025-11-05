using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Contributors
{
    public class ContributorErrorCodes
    {
        public const string ContributorNotFound = "CONTRIBUTOR_NOT_FOUND";
        public const string ContributorFirstNameRequired = "CONTRIBUTOR_FIRST_NAME_REQUIRED";
        public const string ContributorLastNameRequired = "CONTRIBUTOR_LAST_NAME_REQUIRED";
        public const string ContributorRoleRequired = "CONTRIBUTOR_ROLE_REQUIRED";
        public const string ContributorFirstNameMaxLength = "CONTRIBUTOR_FIRST_NAME_MAX_LENGTH";
        public const string ContributorLastNameMaxLength = "CONTRIBUTOR_LAST_NAME_MAX_LENGTH";
        public const string ContributorRoleMaxLength = "CONTRIBUTOR_ROLE_MAX_LENGTH";
        public const string ContributorBioMaxLength = "CONTRIBUTOR_BIO_MAX_LENGTH";
        public const string ContributorEmailInvalid = "CONTRIBUTOR_EMAIL_INVALID";
        public const string ContributorGithubUrlInvalid = "CONTRIBUTOR_GITHUB_URL_INVALID";
        public const string ContributorLinkedInUrlInvalid = "CONTRIBUTOR_LINKED_IN_URL_INVALID";



    }
}