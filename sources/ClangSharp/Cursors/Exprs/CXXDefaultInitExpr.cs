// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Diagnostics;
using ClangSharp.Interop;

namespace ClangSharp;

public sealed class CXXDefaultInitExpr : Expr
{
    private readonly Lazy<FieldDecl> _field;
    private readonly Lazy<IDeclContext?> _usedContext;

    internal CXXDefaultInitExpr(CXCursor handle) : base(handle, CXCursorKind.CXCursor_UnexposedExpr, CX_StmtClass.CX_StmtClass_CXXDefaultInitExpr)
    {
        Debug.Assert(NumChildren is 0);

        _field = new Lazy<FieldDecl>(() => TranslationUnit.GetOrCreate<FieldDecl>(Handle.Referenced));
        _usedContext = new Lazy<IDeclContext?>(() => TranslationUnit.GetOrCreate<Decl>(Handle.UsedContext) as IDeclContext);
    }

    public Expr Expr => Field.InClassInitializer;

    public FieldDecl Field => _field.Value;

    public IDeclContext? UsedContext => _usedContext.Value;
}
