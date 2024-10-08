﻿using System.Windows;
using System.Windows.Controls;

namespace Ninja.Profiles
{
    public class ProfileViewInfo
    {
        public ProfileViewInfo(ProfileName name, Canvas icon, ProfileGroup group)
        {
            Name = name;
            Icon = icon;
            Group = group;
        }

        public ProfileViewInfo(ProfileName name, UIElement uiElement, ProfileGroup group)
        {
            Name = name;
            var canvas = new Canvas();
            canvas.Children.Add(uiElement);
            Icon = canvas;
            Group = group;
        }

        public ProfileName Name { get; set; }

        public Canvas Icon { get; set; }

        public ProfileGroup Group { get; set; }
    }
}