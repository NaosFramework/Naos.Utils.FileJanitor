﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetadataItem.cs" company="Naos">
//    Copyright (c) Naos 2017. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FileJanitor.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using Spritely.Recipes;

    /// <summary>
    /// Model object to hold a metadata entry and allowing sharing a collection of them where <see cref="IReadOnlyDictionary{TKey,TValue}" /> cannot be used.
    /// </summary>
    public class MetadataItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataItem"/> class.
        /// </summary>
        /// <param name="key">Key value.</param>
        /// <param name="value">Value value.</param>
        public MetadataItem(string key, string value)
        {
            new { key }.Must().NotBeNull().OrThrowFirstFailure();

            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Gets the metadata key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the metadata value.
        /// </summary>
        public string Value { get; private set; }
    }

    /// <summary>
    /// Extensions for converting metadata items in and out of a <see cref="IReadOnlyDictionary{TKey,TValue}" />.
    /// </summary>
    public static class MetadataItemExtensions
    {
        /// <summary>
        /// Convert an collection of metadata items into a <see cref="IReadOnlyDictionary{TKey,TValue}" />.
        /// </summary>
        /// <param name="metadataItems">Items to convert.</param>
        /// <returns>Converted collection.</returns>
        public static IReadOnlyDictionary<string, string> ToReadOnlyDictionary(this IReadOnlyCollection<MetadataItem> metadataItems)
        {
            var ret = metadataItems.ToDictionary(k => k.Key, v => v.Value);
            return ret;
        }

        /// <summary>
        /// Convert an dictionary of metadata items into a <see cref="IReadOnlyCollection{TValue}" />.
        /// </summary>
        /// <param name="metadata">Items to convert.</param>
        /// <returns>Converted collection.</returns>
        public static IReadOnlyCollection<MetadataItem> ToReadOnlyCollection(this IReadOnlyDictionary<string, string> metadata)
        {
            var ret = metadata.Select(_ => new MetadataItem(_.Key, _.Value)).ToList();
            return ret;
        }
    }
}