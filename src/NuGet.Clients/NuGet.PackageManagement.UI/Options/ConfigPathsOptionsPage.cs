// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using Microsoft.VisualStudio.Shell;

namespace NuGet.PackageManagement.UI.Options
{
    [Guid("C17B308A-00BB-446E-9212-2D14E1005985")]

    public class ConfigPathsOptionsPage : UIElementDialogPage
    {
        private Lazy<ConfigPathsControl> _configPathsControl;

        /// <summary>
        /// Gets the Windows Presentation Foundation (WPF) child element to be hosted inside the Options dialog page.
        /// </summary>
        /// <returns>The WPF child element.</returns>
        protected override UIElement Child => _configPathsControl.Value;

        public ConfigPathsOptionsPage()
        {
            _configPathsControl = new Lazy<ConfigPathsControl>(() => new ConfigPathsControl(new ConfigPathsViewModel()));
        }

        /// <summary>
        /// This method is called when VS wants to activate this page.
        /// ie. when the user opens the tools options page.
        /// </summary>
        /// <param name="e">Cancellation event arguments.</param>
        protected override void OnActivate(CancelEventArgs e)
        {
            // The UI caches the settings even though the tools options page is closed.
            // This load call ensures we display data that was saved. This is to handle
            // the case when the user hits the cancel button and reloads the page.
            LoadSettingsFromStorage();
            base.OnActivate(e);
            DoCancelableOperationWithProgressUI(() =>
            {
                OnActivateAsync(e, CancellationToken);

            }, Resources.ConfigPathsOptions_OnActivated);
        }

        private void OnActivateAsync(CancelEventArgs e, CancellationToken cancellationToken)
        {
            _configPathsControl.Value.InitializeOnActivated(cancellationToken);
        }

        private ConfigPathsControl ConfigPathsControl => _configPathsControl.Value;
    }
}
