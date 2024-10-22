﻿using System.Collections.Generic;
using System.Windows;

namespace Ninja.Validators
{
    public class IsNameUniqueDependencyObjectWrapper : DependencyObject
    {
        public static readonly DependencyProperty UsedNamesProperty = DependencyProperty.Register("UsedNames",
            typeof(List<string>),
            typeof(IsNameUniqueDependencyObjectWrapper));

        public List<string> UsedNames
        {
            get => GetValue(UsedNamesProperty) as List<string>;
            set => SetValue(UsedNamesProperty, value);
        }
    }
}