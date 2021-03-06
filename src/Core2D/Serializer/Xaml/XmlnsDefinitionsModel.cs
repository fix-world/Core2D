﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Portable.Xaml.Markup;
using Core2D.Serializer.Xaml;

[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D", AssemblyName = "Core2D.Model")]
[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D.Containers", AssemblyName = "Core2D.Model")]
[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D.Data", AssemblyName = "Core2D.Model")]
[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D.Renderer", AssemblyName = "Core2D.Model")]
[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D.Renderer.Presenters", AssemblyName = "Core2D.Model")]
[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D.History", AssemblyName = "Core2D.Model")]
[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D.Interfaces", AssemblyName = "Core2D.Model")]
[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D.Path", AssemblyName = "Core2D.Model")]
[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D.Shapes", AssemblyName = "Core2D.Model")]
[assembly: XmlnsDefinition(XamlConstants.ModelNamespace, "Core2D.Style", AssemblyName = "Core2D.Model")]

[assembly: XmlnsPrefix(XamlConstants.ModelNamespace, "m")]
