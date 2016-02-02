﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;
using Portable.Xaml.ComponentModel;

namespace Core2D
{
    /// <summary>
    /// Path geometry.
    /// </summary>
    [ContentProperty(nameof(Figures))]
    [TypeConverter(typeof(XPathGeometryTypeConverter))]
    public sealed class XPathGeometry
    {
        /// <summary>
        /// Gets or sets figures collection.
        /// </summary>
        public IList<XPathFigure> Figures { get; set; }

        /// <summary>
        /// Gets or sets fill rule.
        /// </summary>
        public XFillRule FillRule { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XPathGeometry"/> class.
        /// </summary>
        public XPathGeometry()
        {
            Figures = new List<XPathFigure>();
        }

        /// <summary>
        /// Creates a new <see cref="XPathGeometry"/> instance.
        /// </summary>
        /// <param name="figures">The figures collection.</param>
        /// <param name="fillRule">The fill rule.</param>
        /// <returns>The new instance of the <see cref="XPathGeometry"/> class.</returns>
        public static XPathGeometry Create(IList<XPathFigure> figures, XFillRule fillRule)
        {
            return new XPathGeometry()
            {
                Figures = figures,
                FillRule = fillRule
            };
        }

        private string ToString(IList<XPathFigure> figures)
        {
            if (figures.Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < figures.Count; i++)
            {
                sb.Append(figures[i]);
                if (i != figures.Count - 1)
                {
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string figuresString = string.Empty;

            if (Figures != null)
            {
                figuresString = ToString(Figures);
            }

            if (FillRule != XFillRule.EvenOdd)
            {
                return "F1" + figuresString;
            }
            else
            {
                return figuresString;
            }
        }
    }
}
