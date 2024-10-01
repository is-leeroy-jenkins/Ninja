﻿using System;
using System.Globalization;
using System.Windows.Data;
using Ninja.Localization;
using Ninja.Models.RemoteDesktop;

namespace Ninja.Converters
{
    using Localization;
    using Models.RemoteDesktop;

    /// <summary>
    ///     Convert <see cref="KeyboardHookMode" /> to translated <see cref="string" /> or wise versa.
    /// </summary>
    public sealed class RemoteDesktopKeyboardHookModeToStringConverter : IValueConverter
    {
        /// <summary>
        ///     Convert <see cref="KeyboardHookMode" /> to translated <see cref="string" />.
        /// </summary>
        /// <param name="value">Object from type <see cref="KeyboardHookMode" />.</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Translated <see cref="KeyboardHookMode" />.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not KeyboardHookMode keyboardHookMode
                ? "-/-"
                : ResourceTranslator.Translate(ResourceIdentifier.RemoteDesktopKeyboardHookMode, keyboardHookMode);
        }

        /// <summary>
        ///     !!! Method not implemented !!!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}