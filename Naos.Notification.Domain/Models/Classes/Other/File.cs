// --------------------------------------------------------------------------------------------------------------------
// <copyright file="File.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Diagnostics.CodeAnalysis;

    using Naos.CodeAnalysis.Recipes;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Represents a file.
    /// </summary>
    public partial class File : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        /// <param name="bytes">The bytes of the file.</param>
        /// <param name="fileName">OPTIONAL name of the file.  DEFAULT is to an unspecified name.</param>
        /// <param name="fileFormat">OPTIONAL format of the file.  DEFAULT is unspecified.</param>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bytes", Justification = NaosSuppressBecause.CA1720_IdentifiersShouldNotContainTypeNames_TypeNameAddsClarityToIdentifierAndAlternativesDegradeClarity)]
        public File(
            byte[] bytes,
            string fileName = null,
            FileFormat fileFormat = FileFormat.Unspecified)
        {
            new { bytes }.AsArg().Must().NotBeNullNorEmptyEnumerable();

            this.Bytes = bytes;
            this.FileName = fileName;
            this.FileFormat = fileFormat;
        }

        /// <summary>
        /// Gets the bytes of the file.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = NaosSuppressBecause.CA1819_PropertiesShouldNotReturnArrays_DataPayloadsAreCommonlyRepresentedAsByteArrays)]
        public byte[] Bytes { get; private set; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the format of the file.
        /// </summary>
        public FileFormat FileFormat { get; private set; }
    }
}