﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Math source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Reflection.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Provides useful methods related to reflection.
    /// </summary>
#if !OBeautifulCodeReflectionRecipesProject
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Reflection", "See package version number")]
    internal
#else
    public
#endif
    static partial class ReflectionHelper
    {
        /// <summary>
        /// Default binding flags used for all methods.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags", Justification = "Correct name.")]
        public const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        /// <summary>
        /// Constructs an object of the specified type.
        /// </summary>
        /// <param name="type">The type of object to construct.</param>
        /// <param name="parameters">
        /// An array of arguments that match in number, order, and type the parameteres of the constructor to invoke.
        /// If an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns>
        /// A reference to the newely created object.
        /// </returns>
        /// <exception cref="Exception">Various exceptions thrown by <see cref="Activator.CreateInstance(Type, object[])"/>.</exception>
        public static object Construct(
            this Type type,
            params object[] parameters)
        {
            var result = Activator.CreateInstance(type, parameters);
            return result;
        }

        /// <summary>
        /// Constructs an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <param name="parameters">
        /// An array of arguments that match in number, order, and type the parameteres of the constructor to invoke.
        /// If an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns>
        /// A reference to the newely created object.
        /// </returns>
        /// <exception cref="Exception">Any exception thrown by <see cref="Activator.CreateInstance(Type, object[])"/>.</exception>
        public static T Construct<T>(
            params object[] parameters)
        {
            var result = typeof(T).Construct<T>(parameters);
            return result;
        }

        /// <summary>
        /// Constructs an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="type">The type of object to construct.</param>
        /// <param name="parameters">
        /// An array of arguments that match in number, order, and type the parameteres of the constructor to invoke.
        /// If an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns>
        /// A reference to the newely created object.
        /// </returns>
        /// <exception cref="Exception">Any exception thrown by <see cref="Activator.CreateInstance(Type, object[])"/>.</exception>
        /// <exception cref="InvalidCastException">The created object could not be cast to a <typeparamref name="T"/>.</exception>
        public static T Construct<T>(
            this Type type,
            params object[] parameters)
        {
            var objectResult = type.Construct(parameters);
            var result = (T)objectResult;
            return result;
        }

        /// <summary>
        /// Gets all types in an assembly that have an attribute of a specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        /// <param name="assembly">The assembly to search.</param>
        /// <param name="attributeFilter">
        /// Optional.  When provided, requires that this filter
        /// return true when attributes of the specified type are passed-in,
        /// before the type having the specified attribute is returned.
        /// </param>
        /// <returns>
        /// The types in an assembly where the specified attribute has been
        /// applied at least one, or an empty collection if none of the
        /// types in the assembly have that attribute.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is null.</exception>
        public static IReadOnlyCollection<Type> GetTypesHaving<TAttribute>(
            this Assembly assembly,
            Func<TAttribute, bool> attributeFilter = null)
            where TAttribute : Attribute
        {
            new { assembly }.Must().NotBeNull();

            var attributeType = typeof(TAttribute);
            var result = assembly.GetTypes()
                .Where(
                    _ =>
                        attributeFilter == null
                            ? _.GetCustomAttributes(attributeType, false).Any()
                            : _.GetCustomAttributes(attributeType, false).Cast<TAttribute>().Where(attributeFilter).Any())
                .ToList()
                .AsReadOnly();

            return result;
        }
    }
}
