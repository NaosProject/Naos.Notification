// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionThrownFailure.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

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