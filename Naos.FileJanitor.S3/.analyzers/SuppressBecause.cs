// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuppressBecause.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in Naos.Build source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FileJanitor.S3
{
    using System.CodeDom.Compiler;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Standard justifications for analysis suppression.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [GeneratedCode("Naos.Build.Analyzers", "See package version number")]
    internal static class SuppressBecause
    {
        /// <summary>
        /// Console executable does not need the [assembly: CLSCompliant(true)] as it should not be shared as an assembly for reference.
        /// </summary>
        public const string CA1014_MarkAssembliesWithClsCompliant_ConsoleExeDoesNotNeedToBeClsCompliant = "Console executable does not need the [assembly: CLSCompliant(true)] as it should not be shared as an assembly for reference.";

        /// <summary>
        /// We prefer to read <see cref="System.Guid" />'s string representation as lowercase.
        /// </summary>
        public const string CA1308_NormalizeStringsToUppercase_PreferGuidLowercase = "We prefer to read System.Guid's string representation as lowercase.";
    }
}
