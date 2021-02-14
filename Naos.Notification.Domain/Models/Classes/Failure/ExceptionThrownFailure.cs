// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionThrownFailure.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Naos.CodeAnalysis.Recipes;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// An exception was thrown.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ExceptionThrownFailure : FailureBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionThrownFailure"/> class.
        /// </summary>
        /// <param name="exceptionToString">The <see cref="Exception.ToString"/> of the <see cref="Exception"/> that was thrown.</param>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = NaosSuppressBecause.CA1720_IdentifiersShouldNotContainTypeNames_TypeNameAddsClarityToIdentifierAndAlternativesDegradeClarity)]
        public ExceptionThrownFailure(
            string exceptionToString)
        {
            new { exceptionToString }.AsArg().Must().NotBeNullNorWhiteSpace();

            this.ExceptionToString = exceptionToString;
        }

        /// <summary>
        /// Gets the <see cref="Exception.ToString"/> of the <see cref="Exception"/> that was thrown.
        /// </summary>
        public string ExceptionToString { get; private set; }
    }
}