﻿// Copyright (c) 2014, The Outercurve Foundation. The software is licensed under the (the "License"); you may not use the software except in compliance with the License.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Outercurve.SQLiteCreateTree.AlterTable.Action;
using Outercurve.SQLiteCreateTree.Nodes;
using Outercurve.SQLiteCreateTree.Nodes.ColumnConstraint;
using Outercurve.SQLiteParser;

namespace Outercurve.SQLiteCreateTree.AlterTable
{
    public interface IAlterTableAdapter
    {
        IEnumerable<string> AlterTableStatements(AlterTableCommand command);
    }


    public class AlterTableAdapter : IAlterTableAdapter
    {
        public AlterTableAdapter(CreateTableNode node, IEnumerable<CreateIndexNode> indexNodes)
        {
            CreateTableNode = node;
            CreateIndexNodes = indexNodes.ToList();
        }

        public AlterTableAdapter(string createTableStmt, IEnumerable<string> creatIndexStmts)
        {
            CreateTableNode = SQLiteParseVisitor.ParseString<CreateTableNode>(createTableStmt);
            CreateIndexNodes = creatIndexStmts.Where(i => !String.IsNullOrEmpty(i)).Select(SQLiteParseVisitor.ParseString<CreateIndexNode>).ToList();
        }
        internal CreateTableNode CreateTableNode { get; private set; }
        internal IList<CreateIndexNode> CreateIndexNodes { get; private set; }

        public IEnumerable<string> AlterTableStatements(AlterTableCommand command)
        {
            var native = new NativeAlterTableProcessor();
            var result = native.CreateSqlStatements(command);
            if (!result.Any())
            {
                var simulated = new SimulatedAlterTableProcessor(CreateTableNode, CreateIndexNodes);
                result = simulated.CreateSqlStatements(command);
            }

            return result;
        }
    }
    
    
}
