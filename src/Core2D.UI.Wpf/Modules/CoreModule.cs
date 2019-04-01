﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Autofac;
using Core2D.Data;
using Core2D.Editor;
using Core2D.Editor.Bounds;
using Core2D.Editor.Factories;
using Core2D.Interfaces;

namespace Core2D.UI.Wpf.Modules
{
    /// <summary>
    /// Core components module.
    /// </summary>
    public class CoreModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProjectEditor>().As<ProjectEditor>().InstancePerLifetimeScope();
            builder.RegisterType<Factory>().As<IFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ContainerFactory>().As<IContainerFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ShapeFactory>().As<IShapeFactory>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(EditorToolBase).Assembly).As<EditorToolBase>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(PathToolBase).Assembly).As<PathToolBase>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<HitTest>().As<HitTest>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(HitTestBase).Assembly).As<HitTestBase>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DataFlow>().As<IDataFlow>().InstancePerLifetimeScope();
        }
    }
}
