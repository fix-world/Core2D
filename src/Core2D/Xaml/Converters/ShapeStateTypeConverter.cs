﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Core2D.Shape;
using Portable.Xaml.ComponentModel;
using System;
using System.Globalization;

namespace Core2D.Xaml.Converters
{
    /// <summary>
    /// Defines <see cref="ShapeState"/> type converter.
    /// </summary>
    public sealed class ShapeStateTypeConverter : TypeConverter
    {
        /// <inheritdoc/>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <inheritdoc/>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <inheritdoc/>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return ShapeState.Parse((string)value);
        }

        /// <inheritdoc/>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var state = value as ShapeState;
            if (state != null)
            {
                return state.ToString();
            }
            throw new NotSupportedException();
        }
    }
}