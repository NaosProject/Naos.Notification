﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompressionDummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Compression.Test source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Compression.Test
{
    using OBeautifulCode.AutoFakeItEasy;

    /// <summary>
    /// A Dummy Factory for types in <see cref="OBeautifulCode.Compression"/>.
    /// </summary>
#if !OBeautifulCodeCompressionSolution
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Compression.Test", "See package version number")]
    internal
#else
    public
#endif
    class CompressionDummyFactory : DefaultCompressionDummyFactory
    {
        public CompressionDummyFactory()
        {
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(CompressionKind.Invalid);
        }
    }
}
