﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;

namespace FluentAssertions.BestPractices
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CollectionShouldHaveCountLessOrEqualToAnalyzer : FluentAssertionsAnalyzer
    {
        public const string DiagnosticId = Constants.Tips.Collections.CollectionShouldHaveCountLessOrEqualTo;
        public const string Category = Constants.Tips.Category;

        public const string Message = "Use {0} .Should() followed by ### instead.";

        protected override DiagnosticDescriptor Rule => new DiagnosticDescriptor(DiagnosticId, Title, Message, Category, DiagnosticSeverity.Info, true);

        protected override Diagnostic AnalyzeExpressionStatement(ExpressionStatementSyntax statement)
        {
            return null;
            var visitor = new CollectionShouldHaveCountLessOrEqualToSyntaxVisitor();
            statement.Accept(visitor);

            if (visitor.IsValid)
            {
                var properties = new Dictionary<string, string>
                {
                    [Constants.DiagnosticProperties.VariableName] = visitor.VariableName,
                    [Constants.DiagnosticProperties.Title] = Title
                }.ToImmutableDictionary();
				throw new System.NotImplementedException();

                return Diagnostic.Create(
                    descriptor: Rule,
                    location: statement.Expression.GetLocation(),
                    properties: properties,
                    messageArgs: visitor.VariableName);
            }
            return null;
        }
    }

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(CollectionShouldHaveCountLessOrEqualToCodeFix)), Shared]
    public class CollectionShouldHaveCountLessOrEqualToCodeFix : FluentAssertionsCodeFixProvider
    {
        public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(CollectionShouldHaveCountLessOrEqualToAnalyzer.DiagnosticId);

        protected override StatementSyntax GetNewStatement(FluentAssertionsDiagnosticProperties properties)
        {
			throw new System.NotImplementedException();
		}
    }

    public class CollectionShouldHaveCountLessOrEqualToSyntaxVisitor : FluentAssertionsCSharpSyntaxVisitor
    {
        public CollectionShouldHaveCountLessOrEqualToSyntaxVisitor() : base("###")
        {
			throw new System.NotImplementedException();
        }
    }
}
