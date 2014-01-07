﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using ExpectedObjects;
using Outercurve.SQLiteCreateTree;
using Outercurve.SQLiteCreateTree.Nodes;
using Outercurve.SQLiteCreateTree.Nodes.ColumnConstraint;
using Outercurve.SQLiteCreateTree.Nodes.TableConstraint;
using Outercurve.SQLiteParser;
using Xunit;

namespace SQLiteParseTreeTest
{
    public class SQLiteCreateTableTest
    {
        [Fact]
        public void TestCreateTable()
        {
            
            var parser = RunParser("create TABLE t1(a, b PRIMARY KEY);");
            var expected = new
            {
                ColumnDefinitions =
                    new List<object>
                    {
                        new  {ColumnName = "a"},
                        new  {ColumnName = "b", ColumnConstraints = new List<object>
                        {
                            new PrimaryKeyConstraintNode {}
                        }}
                    },
                TableName = "t1",
                Temp = false
            }.ToExpectedObject().AddTreeNode();

            expected.ShouldMatch(parser);


        }


        [Fact]
        public void TestCreateTable2()
        {

            var parser = RunParser("create TABLE t1(a, b PRIMARY KEY DEFAULT 1);");
            var expected = new
            {
                ColumnDefinitions =
                    new List<object>
                    {
                        new  {ColumnName = "a"},
                        new  {ColumnName = "b", ColumnConstraints = new List<object>
                        {
                            new PrimaryKeyConstraintNode {},
                            new DefaultConstraintNode { Value = "1"}
                        }}
                    },
                TableName = "t1",
                Temp = false
            }.ToExpectedObject().AddTreeNode();

            expected.ShouldMatch(parser);


        }
        [Fact]
        public void TestBigThing()
        {
            var parser =
                RunParser(
                    "CREATE TABLE operationbalance(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,f_balance FLOAT NOT NULL DEFAULT 0,r_operation_id INTEGER NOT NULL);");

            var expected = parser.ToExpectedObject().AddTreeNode();

            var visitor = new TreeStringOutputVisitor();
            var output = parser.Accept(visitor).ToString();
            //lets parse it again!
            var finalTree = RunParser(output);

            expected.ShouldMatch(finalTree);
        }

        [Fact]
        public void TestWithTableConstraintsRoundTrip()
        {
            var parser = RunParser(
                "CREATE TABLE PROJECTS(CLASSID int null, SEQNO int not null, LASTMODONNODEID text(50) not null, PREVMODONNODEID text(50) null, ISSUEID text(50) not null, OBJECTID text(50) not null, REVISIONNUM int not null, CONTAINERID text(50) not null, AUTHORID text(50) not null, CREATIONDATE text(25) null, LASTMODIFIEDDATE text(25) null, UPDATENUMBER int null, PREVREVISIONNUM int null, LASTCMD int null, LASTCMDACLVERSION int null, USERDEFINEDFIELD text(300) null, LASTMODIFIEDBYID text(50) null, NAME text(100) not null, ID text(100) null, constraint PK_PROJECTS primary key (ISSUEID, OBJECTID))");
            var expected = parser.ToExpectedObject().AddTreeNode();
            var visitor = new TreeStringOutputVisitor();
            var output = parser.Accept(visitor).ToString();

            var finalTree = RunParser(output);
            expected.ShouldMatch(finalTree);
        }

        [Fact]
        public void TestWithTableConstraintsTree()
        {
           var expected =  new CreateTableNode
            {
                TableName = "PROJECTS",
                ColumnDefinitions = new[]
                {
                    new ColumnDefNode
                    {
                        ColumnName = "CLASSID",
                        TypeNameNode = new TypeNameNode {TypeName = "int"},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },
                    new ColumnDefNode
                    {
                        ColumnName = "SEQNO",
                        TypeNameNode = new TypeNameNode {TypeName = "int"},
                        ColumnConstraints = new[] {new NotNullConstraintNode()}

                    },

                    new ColumnDefNode
                    {
                        ColumnName = "LASTMODONNODEID",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"50"}},
                        ColumnConstraints = new[] {new NotNullConstraintNode()}

                    }
                    ,
                    new ColumnDefNode
                    {
                        ColumnName = "PREVMODONNODEID",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"50"}},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },

                    new ColumnDefNode
                    {
                        ColumnName = "ISSUEID",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"50"}},
                        ColumnConstraints = new[] {new NotNullConstraintNode()}

                    },

                    new ColumnDefNode
                    {
                        ColumnName = "OBJECTID",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"50"}},
                        ColumnConstraints = new[] {new NotNullConstraintNode()}

                    },

                    new ColumnDefNode
                    {
                        ColumnName = "REVISIONNUM",
                        TypeNameNode = new TypeNameNode {TypeName = "int"},
                        ColumnConstraints = new[] {new NotNullConstraintNode()}

                    },

                    new ColumnDefNode
                    {
                        ColumnName = "CONTAINERID",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"50"}},
                        ColumnConstraints = new[] {new NotNullConstraintNode()}

                    },

                    new ColumnDefNode
                    {
                        ColumnName = "AUTHORID",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"50"}},
                        ColumnConstraints = new[] {new NotNullConstraintNode()}

                    },

                    new ColumnDefNode
                    {
                        ColumnName = "CREATIONDATE",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"25"}},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },

                    new ColumnDefNode
                    {
                        ColumnName = "LASTMODIFIEDDATE",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"25"}},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },
                    new ColumnDefNode
                    {
                        ColumnName = "UPDATENUMBER",
                        TypeNameNode = new TypeNameNode {TypeName = "int"},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },
                    new ColumnDefNode
                    {
                        ColumnName = "PREVREVISIONNUM",
                        TypeNameNode = new TypeNameNode {TypeName = "int"},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },
                    new ColumnDefNode
                    {
                        ColumnName = "LASTCMD",
                        TypeNameNode = new TypeNameNode {TypeName = "int"},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },
                    new ColumnDefNode
                    {
                        ColumnName = "LASTCMDACLVERSION",
                        TypeNameNode = new TypeNameNode {TypeName = "int"},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },
                    new ColumnDefNode
                    {
                        ColumnName = "USERDEFINEDFIELD",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"300"}},
                        ColumnConstraints = new[] {new DefaultConstraintNode { Value = "NULL"}}

                    },
                    new ColumnDefNode
                    {
                        ColumnName = "LASTMODIFIEDBYID",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"50"}},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },

                    new ColumnDefNode
                    {
                        ColumnName = "NAME",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"100"}},
                        ColumnConstraints = new[] {new NotNullConstraintNode()}

                    },
                    new ColumnDefNode
                    {
                        ColumnName = "ID",
                        TypeNameNode = new TypeNameNode {TypeName = "text", SignedNumbers = new[] {"100"}},
                        ColumnConstraints = new[] {new DefaultConstraintNode() { Value = "NULL"}}

                    },
                },
                TableConstraints = new[]
                {
                    new IndexClauseNode
                    {
                        ConstraintName = "PK_PROJECTS",
                        IndexColumns = new[]
                        {
                            new IndexedColumnNode {Id = "ISSUEID"},
                            new IndexedColumnNode {Id = "OBJECTID"}
                        }

                    }
                }
            }.ToExpectedObject().AddTreeNode();

            var parser = RunParser(
                "CREATE TABLE PROJECTS(CLASSID int null, SEQNO int not null, LASTMODONNODEID text(50) not null, PREVMODONNODEID text(50) null, ISSUEID text(50) not null, OBJECTID text(50) not null, REVISIONNUM int not null, CONTAINERID text(50) not null, AUTHORID text(50) not null, CREATIONDATE text(25) null, LASTMODIFIEDDATE text(25) null, UPDATENUMBER int null, PREVREVISIONNUM int null, LASTCMD int null, LASTCMDACLVERSION int null, USERDEFINEDFIELD text(300) null, LASTMODIFIEDBYID text(50) null, NAME text(100) not null, ID text(100) null, constraint PK_PROJECTS primary key (ISSUEID, OBJECTID))");
            expected.ShouldMatch(parser);

        }

        


     

        public SQLiteParseTreeNode RunParser(string parseString)
        {
            AntlrInputStream inputStream = new AntlrInputStream(parseString);
            SQLiteParserSimpleLexer sqliteLexer = new SQLiteParserSimpleLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(sqliteLexer);
            SQLiteParserSimpleParser sqliteParser = new SQLiteParserSimpleParser(commonTokenStream);
            var visitor = new SQLiteParseVisitor();
            return visitor.Visit(sqliteParser.sql_stmt());

        }
    }
}
