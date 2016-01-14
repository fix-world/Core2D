﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Linq;

namespace Core2D
{
    /// <summary>
    /// Factory used to create new projects, documents and containers.
    /// </summary>
    public class ProjectFactory : IProjectFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Library{ShapeStyle}"/> class.
        /// </summary>
        /// <returns>The new instance of the <see cref="Library{ShapeStyle}"/>.</returns>
        public static Library<ShapeStyle> DefaultStyleLibrary()
        {
            var sgd = Library<ShapeStyle>.Create("Default");

            var builder = sgd.Items.ToBuilder();
            builder.Add(ShapeStyle.Create("Black", 255, 0, 0, 0, 80, 0, 0, 0, 2.0));
            builder.Add(ShapeStyle.Create("Red", 255, 255, 0, 0, 80, 255, 0, 0, 2.0));
            builder.Add(ShapeStyle.Create("Green", 255, 0, 255, 0, 80, 0, 255, 0, 2.0));
            builder.Add(ShapeStyle.Create("Blue", 255, 0, 0, 255, 80, 0, 0, 255, 2.0));
            builder.Add(ShapeStyle.Create("Cyan", 255, 0, 255, 255, 80, 0, 255, 255, 2.0));
            builder.Add(ShapeStyle.Create("Magenta", 255, 255, 0, 255, 80, 255, 0, 255, 2.0));
            builder.Add(ShapeStyle.Create("Yellow", 255, 255, 255, 0, 80, 255, 255, 0, 2.0));
            sgd.Items = builder.ToImmutable();

            sgd.Selected = sgd.Items.FirstOrDefault();

            return sgd;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Library{ShapeStyle}"/> class.
        /// </summary>
        /// <returns>The new instance of the <see cref="Library{ShapeStyle}"/>.</returns>
        public static Library<ShapeStyle> LinesStyleLibrary()
        {
            var sgdl = Library<ShapeStyle>.Create("Lines");

            var solid = ShapeStyle.Create("Solid", 255, 0, 0, 0, 80, 0, 0, 0, 2.0);
            solid.Dashes = default(string);
            solid.DashOffset = 0.0;

            var dash = ShapeStyle.Create("Dash", 255, 0, 0, 0, 80, 0, 0, 0, 2.0);
            dash.Dashes = "2 2";
            dash.DashOffset = 1.0;

            var dot = ShapeStyle.Create("Dot", 255, 0, 0, 0, 80, 0, 0, 0, 2.0);
            dot.Dashes = "0 2";
            dot.DashOffset = 0.0;

            var dashDot = ShapeStyle.Create("DashDot", 255, 0, 0, 0, 80, 0, 0, 0, 2.0);
            dashDot.Dashes = "2 2 0 2";
            dashDot.DashOffset = 1.0;

            var dashDotDot = ShapeStyle.Create("DashDotDot", 255, 0, 0, 0, 80, 0, 0, 0, 2.0);
            dashDotDot.Dashes = "2 2 0 2 0 2";
            dashDotDot.DashOffset = 1.0;

            var builder = sgdl.Items.ToBuilder();
            builder.Add(solid);
            builder.Add(dash);
            builder.Add(dot);
            builder.Add(dashDot);
            builder.Add(dashDotDot);
            sgdl.Items = builder.ToImmutable();

            sgdl.Selected = sgdl.Items.FirstOrDefault();

            return sgdl;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Library{ShapeStyle}"/> class.
        /// </summary>
        /// <returns>The new instance of the <see cref="Library{ShapeStyle}"/>.</returns>
        public static Library<ShapeStyle> TemplateStyleLibrary()
        {
            var sgt = Library<ShapeStyle>.Create("Template");
            var gs = ShapeStyle.Create("Grid", 255, 222, 222, 222, 255, 222, 222, 222, 1.0);

            var builder = sgt.Items.ToBuilder();
            builder.Add(gs);
            sgt.Items = builder.ToImmutable();

            sgt.Selected = sgt.Items.FirstOrDefault();

            return sgt;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Template"/> class.
        /// </summary>
        /// <param name="project">The new container owner project.</param>
        /// <param name="name">The new container name.</param>
        /// <returns>The new instance of the <see cref="Template"/>.</returns>
        private Template CreateGridTemplate(Project project, string name)
        {
            var template = GetTemplate(project, name);

            var style = project
                .StyleLibraries.FirstOrDefault(g => g.Name == "Template")
                .Items.FirstOrDefault(s => s.Name == "Grid");
            var layer = template.Layers.FirstOrDefault();
            var builder = layer.Shapes.ToBuilder();
            var grid = XRectangle.Create(
                0, 0,
                template.Width, template.Height,
                style,
                project.Options.PointShape);
            grid.IsStroked = false;
            grid.IsFilled = false;
            grid.IsGrid = true;
            grid.OffsetX = 30.0;
            grid.OffsetY = 30.0;
            grid.CellWidth = 30.0;
            grid.CellHeight = 30.0;
            grid.State.Flags &= ~ShapeStateFlags.Printable;
            builder.Add(grid);
            layer.Shapes = builder.ToImmutable();

            return template;
        }

        /// <inheritdoc/>
        public Template GetTemplate(Project project, string name)
        {
            var template = Template.Create(name);
            template.Background = ArgbColor.Create(0xFF, 0xFF, 0xFF, 0xFF);
            return template;
        }

        /// <inheritdoc/>
        public Page GetPage(Project project, string name)
        {
            var container = Page.Create(name);
            container.Template = project.CurrentTemplate ?? GetTemplate(project, "Empty");
            return container;
        }

        /// <inheritdoc/>
        public Document GetDocument(Project project, string name)
        {
            var document = Document.Create(name);
            return document;
        }

        /// <inheritdoc/>
        public Project GetProject()
        {
            var project = Project.Create();

            // Group Libraries
            var glBuilder = project.GroupLibraries.ToBuilder();
            glBuilder.Add(Library<XGroup>.Create("Default"));
            project.GroupLibraries = glBuilder.ToImmutable();

            project.SetCurrentGroupLibrary(project.GroupLibraries.FirstOrDefault());

            // Style Libraries
            var sgBuilder = project.StyleLibraries.ToBuilder();
            sgBuilder.Add(DefaultStyleLibrary());
            sgBuilder.Add(LinesStyleLibrary());
            sgBuilder.Add(TemplateStyleLibrary());
            project.StyleLibraries = sgBuilder.ToImmutable();

            project.SetCurrentStyleLibrary(project.StyleLibraries.FirstOrDefault());

            // Templates
            var templateBuilder = project.Templates.ToBuilder();
            templateBuilder.Add(GetTemplate(project, "Empty"));
            templateBuilder.Add(CreateGridTemplate(project, "Grid"));
            project.Templates = templateBuilder.ToImmutable();

            project.SetCurrentTemplate(project.Templates.FirstOrDefault(t => t.Name == "Grid"));

            // Documents and Pages
            var document = GetDocument(project, "Document");
            var page = GetPage(project, "Page");

            var pageBuilder = document.Pages.ToBuilder();
            pageBuilder.Add(page);
            document.Pages = pageBuilder.ToImmutable();

            var documentBuilder = project.Documents.ToBuilder();
            documentBuilder.Add(document);
            project.Documents = documentBuilder.ToImmutable();

            project.Selected = document.Pages.FirstOrDefault();

            // Databases
            var db = Database.Create("Db");
            var columnsBuilder = db.Columns.ToBuilder();
            columnsBuilder.Add(Column.Create(db, "Column0"));
            columnsBuilder.Add(Column.Create(db, "Column1"));
            db.Columns = columnsBuilder.ToImmutable();
            project.Databases = project.Databases.Add(db);

            project.SetCurrentDatabase(db);

            return project;
        }
    }
}