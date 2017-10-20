using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;

namespace FluentAssertions.BestPractices
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DictionaryShouldNotContainValueAnalyzer : FluentAssertionsAnalyzer
    {
        public const string DiagnosticId = Constants.Tips.Dictionaries.DictionaryShouldNotContainValue;
        public const string Category = Constants.Tips.Category;

        public const string Message = "Use {0} .Should() followed by .NotContainValue() instead.";

        protected override DiagnosticDescriptor Rule => new DiagnosticDescriptor(DiagnosticId, Title, Message, Category, DiagnosticSeverity.Info, true);
        protected override IEnumerable<FluentAssertionsCSharpSyntaxVisitor> Visitors
        {
            get
            {
                yield return new ContainsValueShouldBeFalse();
            }
        }

        private class ContainsValueShouldBeFalse : FluentAssertionsCSharpSyntaxVisitor
        {
            public ContainsValueShouldBeFalse() : base("ContainsValue", "Should", "BeFalse")
            {
            }
        }
    }

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(DictionaryShouldNotContainValueCodeFix)), Shared]
    public class DictionaryShouldNotContainValueCodeFix : FluentAssertionsCodeFixProvider
    {
        public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DictionaryShouldNotContainValueAnalyzer.DiagnosticId);

        protected override StatementSyntax GetNewStatement(ExpressionStatementSyntax statement, FluentAssertionsDiagnosticProperties properties)
        {
            var remove = NodeReplacement.RemoveAndExtractArguments("ContainsValue");
            var newStatement = GetNewStatement(statement, remove);

            return GetNewStatement(newStatement, NodeReplacement.RenameAndPrependArguments("BeFalse", "NotContainValue", remove.Arguments));
        }
    }
}