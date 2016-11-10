﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileJanitorMessageHandlerSettings.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FileJanitor.MessageBus.Contract
{
    /// <summary>
    /// Model object for Its.Configuration providing settings for the MessageHandlers..
    /// </summary>
    public class FileJanitorMessageHandlerSettings
    {
        /// <summary>
        /// Gets or sets the access key of a user to upload files to storage.
        /// </summary>
        public string UploadAccessKey { get; set; }

        /// <summary>
        /// Gets or sets the secret key of a user to upload files to storage.
        /// </summary>
        public string UploadSecretKey { get; set; }

        /// <summary>
        /// Gets or sets the access key of a user to download files from storage.
        /// </summary>
        public string DownloadAccessKey { get; set; }

        /// <summary>
        /// Gets or sets the secret key of a user to download files from storage.
        /// </summary>
        public string DownloadSecretKey { get; set; }
    }
}
