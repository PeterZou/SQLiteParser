﻿// Copyright (c) 2014, The Outercurve Foundation. The software is licensed under the (the "License"); you may not use the software except in compliance with the License.
using System.Collections.Generic;
using Antlr4.Runtime;

namespace Outercurve.SQLiteCreateTree.Nodes.TableConstraint
{
    public class IndexClauseNode : TableConstraintNode
    {
        public IndexClauseNode()
        {

        }

        public IndexClauseNode(ParserRuleContext context) : base(context)
        {

        }

        public PrimaryKeyOrUnique IndexUniqueness { get; set; }

        public IList<IndexedColumnNode> IndexColumns { get; set; }

        public ConflictClauseNode ConflictClauseNode { get; set; }
        public override TResult Accept<TResult>(ILogicalParseTreeVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }


    }

    public enum PrimaryKeyOrUnique
    {
        PrimaryKey,
        Unique
    }
}