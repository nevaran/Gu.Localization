namespace Gu.Localization.Analyzers
{
    using System.Collections.Immutable;
    using System.Composition;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Gu.Roslyn.AnalyzerExtensions;
    using Gu.Roslyn.CodeFixExtensions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Rename;

    [Shared]
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(RenameResourceFix))]
    internal class RenameResourceFix : CodeFixProvider
    {
        public override ImmutableArray<string> FixableDiagnosticIds { get; } = ImmutableArray.Create(
            GULOC07KeyDoesNotMatch.DiagnosticId);

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var syntaxRoot = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var semanticModel = await context.Document.GetSemanticModelAsync(context.CancellationToken).ConfigureAwait(false);
            foreach (var diagnostic in context.Diagnostics)
            {
                if (syntaxRoot.TryFindNodeOrAncestor<PropertyDeclarationSyntax>(diagnostic, out var propertyDeclaration) &&
                    semanticModel.TryGetSymbol(propertyDeclaration, context.CancellationToken, out var property) &&
                    diagnostic.Properties.TryGetValue("Key", out var name) &&
                    !property.ContainingType.TryFindFirstMember(name, out _))
                {
                    context.RegisterCodeFix(
                        new PreviewCodeAction(
                            "Rename resource",
                            cancellationToken => Renamer.RenameSymbolAsync(context.Document.Project.Solution, property, name, null, cancellationToken),
                            cancellationToken => RenameAsync(context.Document.Project.Solution, property, name, cancellationToken)),
                        diagnostic);
                }
            }
        }

        private static Task<Solution> RenameAsync(Solution solution, IPropertySymbol property, string name, CancellationToken cancellationToken)
        {
            if (Resources.TryGetDefaultResx(property.ContainingType, out var resx))
            {
                Rename(resx, property.Name, name);
                foreach (var cultureResx in resx.Directory.EnumerateFiles($"{Path.GetFileNameWithoutExtension(resx.Name)}.*.resx", SearchOption.TopDirectoryOnly))
                {
                    Rename(cultureResx, property.Name, name);
                }

                return Renamer.RenameSymbolAsync(solution, property, name, null, cancellationToken);
            }

            return Task.FromResult(solution);
        }

        private static void Rename(FileInfo resx, string oldName, string newName)
        {
            var xDocument = XDocument.Load(resx.FullName);
            if (xDocument.Root is XElement root)
            {
                foreach (var candidate in root.Elements("data"))
                {
                    if (candidate.Attribute("name") is XAttribute attribute &&
                        attribute.Value == oldName)
                    {
                        attribute.Value = newName;
                        using (var stream = File.OpenWrite(resx.FullName))
                        {
                            xDocument.Save(stream);
                        }
                    }
                }
            }
        }
    }
}
