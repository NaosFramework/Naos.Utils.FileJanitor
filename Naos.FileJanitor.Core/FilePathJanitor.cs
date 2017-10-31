﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilePathJanitor.cs" company="Naos">
//    Copyright (c) Naos 2017. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FileJanitor.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Naos.FileJanitor.Domain;

    using Spritely.Recipes;

    /// <summary>
    /// Tools for helping with cleaning up paths.
    /// </summary>
    public static class FilePathJanitor
    {
        /// <summary>
        /// Removes old files.
        /// </summary>
        /// <param name="rootPath">The root path to evaluate (must be a directory).</param>
        /// <param name="retentionWindow">The time to retain files (in format dd:hh:mm).</param>
        /// <param name="recursive">Whether or not to evaluate files recursively on the path.</param>
        /// <param name="deleteEmptyDirectories">Whether or not to delete directories that are or become empty during cleanup.</param>
        /// <param name="dateRetrievalStrategy">The date retrieval strategy to use on files.</param>
        public static void Cleanup(
            string rootPath,
            TimeSpan retentionWindow,
            bool recursive,
            bool deleteEmptyDirectories,
            DateRetrievalStrategy dateRetrievalStrategy)
        {
            if (!File.GetAttributes(rootPath).HasFlag(FileAttributes.Directory))
            {
                throw new ArgumentException("Root path must be a directory.");
            }

            if (!Directory.Exists(rootPath))
            {
                throw new ArgumentException("Root path: " + rootPath + " does not exist.");
            }

            var searchOptions = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var files = Directory.GetFiles(
                rootPath,
                "*",
                searchOptions);

            var cutoff = DateTime.UtcNow.Subtract(retentionWindow);
            var filesToDelete = files.BeforeCutOff(cutoff, dateRetrievalStrategy);

            foreach (var fileToDelete in filesToDelete)
            {
                Console.WriteLine("File: " + fileToDelete + " is being removed because it's outside of the retention window.");
                File.Delete(fileToDelete);
            }

            if (deleteEmptyDirectories)
            {
                foreach (var directoryPath in Directory.GetDirectories(rootPath, "*", searchOptions))
                {
                    var directory = new DirectoryInfo(directoryPath);
                    if (!directory.GetFiles().Any())
                    {
                        Console.WriteLine("Directory: " + directoryPath + " is being removed because it's empty.");
                        directory.Delete(recursive);
                    }
                }
            }
        }

        private static string[] BeforeCutOff(this string[] filePaths, DateTime cutoffInUtc, DateRetrievalStrategy dateRetrievalStrategy)
        {
            var ret = new List<string>();
            foreach (var filePath in filePaths)
            {
                var file = new FileInfo(filePath);
                DateTime compareDateUtc;
                switch (dateRetrievalStrategy)
                {
                    case DateRetrievalStrategy.LastUpdateDate:
                        compareDateUtc = file.LastWriteTimeUtc;
                        break;
                    case DateRetrievalStrategy.LastAccessDate:
                        compareDateUtc = file.LastAccessTimeUtc;
                        break;
                    case DateRetrievalStrategy.CreateDate:
                        compareDateUtc = file.CreationTimeUtc;
                        break;
                    default:
                        throw new ArgumentException("Unsupported DateRetreivalStrategy: " + dateRetrievalStrategy);
                }

                if (compareDateUtc < cutoffInUtc)
                {
                    ret.Add(filePath);
                }
            }

            return ret.ToArray();
        }
    }
}