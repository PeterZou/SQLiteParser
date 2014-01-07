﻿using Antlr4.Runtime;

namespace Outercurve.SQLiteCreateTree.Nodes
{
    public class ConflictClauseNode : SQLiteParseTreeNode
    {
        public ConflictClauseNode()
        {
            
        }

        public ConflictClauseNode(ParserRuleContext context) : base(context)
        {
            
        }

        public ConflictChoice Choice { get; set; }

        public override TResult Accept<TResult>(ILogicalParseTreeVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }
    }

    public enum ConflictChoice
    {
        Rollback,
        Abort,
        Fail,
        Ignore,
        Replace
    }
}
