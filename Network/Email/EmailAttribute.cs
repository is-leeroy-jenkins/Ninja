// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="EmailAttribute.cs" company="Terry D. Eppler">
//    Badger is data analysis and reporting tool for EPA Analysts.
//    Copyright ©  2024  Terry D. Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the “Software”),
//    to deal in the Software without restriction,
//    including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense,
//    and/or sell copies of the Software,
//    and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
// 
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
// 
//    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//    DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//    DEALINGS IN THE SOFTWARE.
// 
//    You can contact me at: terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   EmailAttribute.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    /// <inheritdoc />
    /// <summary>
    /// An attribute that validates the syntax of an email address.
    /// </summary>
    /// <remarks>
    /// An attribute that validates the syntax of an email address.
    /// </remarks>
    [ AttributeUsage( AttributeTargets.Property
        | AttributeTargets.Field
        | AttributeTargets.Parameter ) ]
    [ SuppressMessage( "ReSharper", "MissingBlankLines" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "GrammarMistakeInComment" ) ]
    public class EmailAttribute : ValidationAttribute
    {
        /// <summary>
        /// Get or set whether or not the validator
        /// should allow top-level domains.
        /// </summary>
        /// <remarks>
        /// Gets or sets whether or not the validator
        /// should allow top-level domains.
        /// </remarks>
        /// <value><c>true</c> if top-level domains
        /// should be allowed;
        /// otherwise, <c>false</c>.</value>
        private bool AllowTopLevelDomains { get; set; }

        /// <summary>
        /// Get or set whether or not the validator s
        /// should allow international characters.
        /// </summary>
        /// <remarks>
        /// Gets or sets whether or not the validator
        /// should allow international characters.
        /// </remarks>
        /// <value><c>true</c> if international characters
        /// should be allowed; otherwise,
        /// <c>false</c>.</value>
        private bool AllowInternational { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Instantiates a new instance of
        /// <see cref="T:Ninja.EmailAttribute" />.
        /// </summary>
        /// <remarks>
        /// Creates a new
        /// <see cref="T:Ninja.EmailAttribute" />.
        /// </remarks>
        /// <param name="allowTopLevelDomains"><c>true</c>
        /// if the validator should allow addresses
        /// at top-level domains; otherwise,
        /// <c>false</c>.</param>
        /// <param name="allowInternational"><c>true</c>
        /// if the validator should allow
        /// international characters; otherwise,
        /// <c>false</c>.</param>
        public EmailAttribute( bool allowTopLevelDomains = false, bool allowInternational = false )
        {
            AllowTopLevelDomains = allowTopLevelDomains;
            AllowInternational = allowInternational;
        }

        /// <inheritdoc />
        /// <summary>
        /// Validates the value.
        /// </summary>
        /// <remarks>
        /// Checks whether or not the email address
        /// provided is syntactically correct.
        /// </remarks>
        /// <returns>
        /// The validation result.
        /// </returns>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <param name="context">
        /// The validation context.
        /// </param>
        protected override ValidationResult IsValid( object value, ValidationContext context )
        {
            var _memberNames = new[ ]
            {
                context?.MemberName ?? nameof( value )
            };

            if( value == null
                || EmailValidator.Validate( (string)value, AllowTopLevelDomains,
                    AllowInternational ) )
            {
                return ValidationResult.Success;
            }

            return new ValidationResult( "Email invalid", _memberNames );
        }

        /// <inheritdoc />
        /// <summary>
        /// Validates the value.
        /// </summary>
        /// <remarks>
        /// Checks whether or not the email address
        /// provided is syntactically correct.
        /// </remarks>
        /// <returns><c>true</c> if the value is a valid email address;
        /// otherwise, <c>false</c>.</returns>
        /// <param name="value">The value to validate.</param>
        public override bool IsValid( object value )
        {
            return value == null
                || EmailValidator.Validate( (string)value, AllowTopLevelDomains,
                    AllowInternational );
        }
    }
}