using ScintillaNET;
using System;
using System.Drawing;

namespace AyanixEdit
{

    public class SC
    {
        public static void Set_NoZoom(Scintilla scObj)
        {
            scObj.ZoomChanged += (TextZoomChanged);
            scObj.TextChanged += (TextChanged);
            scObj.WrapMode = WrapMode.None;
            scObj.IndentationGuides = IndentView.LookBoth;
            scObj.ScrollWidth = 1;
            scObj.ScrollWidthTracking = true;
        }


        public static void Set_Coloring_Text(Scintilla scObj, bool bLineNumbers = true)
        {
            // Reset the styles
            scObj.StyleResetDefault();
            scObj.CaretForeColor = Color.White;
            scObj.Styles[Style.Default].Font = "Consolas";
            scObj.Styles[Style.Default].Size = 10;
            scObj.Styles[Style.Default].BackColor = Helper.IntToColor(0x1E1E1E);
            scObj.Styles[Style.Default].ForeColor = Helper.IntToColor(0xF1F2F3);
            scObj.StyleClearAll();

            // Set the SQL Lexer
            scObj.Lexer = Lexer.Html;

            // Show line numbers
            if (bLineNumbers)
            {
                scObj.Margins[0].Width = 35;
                scObj.Margins[0].Type = MarginType.Number;
                scObj.Margins[0].Sensitive = true;
                scObj.Margins[0].Mask = 0;

                // Set the Styles
                scObj.Styles[Style.LineNumber].ForeColor = Helper.IntToColor(0x2B91AF);
                scObj.Styles[Style.LineNumber].BackColor = Helper.IntToColor(0x1E1E1E);
            }

          
        }



        // C++ or C# Syntax Coloring
        public static void Set_Coloring_Cpp(Scintilla scObj, bool bLineNumbers = true)
        {
            // Reset the styles
            scObj.StyleResetDefault();
            scObj.CaretForeColor = Color.White;
            scObj.Styles[Style.Default].Font = "Consolas";
            scObj.Styles[Style.Default].Size = 10;
            scObj.Styles[Style.Default].BackColor = Helper.IntToColor(0x1E1E1E);
            scObj.Styles[Style.Default].ForeColor = Helper.IntToColor(0xF1F2F3);
            scObj.StyleClearAll();

            // Set the SQL Lexer
            scObj.Lexer = Lexer.Cpp;

            // Show line numbers
            if (bLineNumbers)
            {
                scObj.Margins[0].Width = 35;
                scObj.Margins[0].Type = MarginType.Number;
                scObj.Margins[0].Sensitive = true;
                scObj.Margins[0].Mask = 0;

                // Set the Styles
                scObj.Styles[Style.LineNumber].ForeColor = Helper.IntToColor(0x2B91AF);
                scObj.Styles[Style.LineNumber].BackColor = Helper.IntToColor(0x1E1E1E);
            }

            scObj.Styles[Style.Cpp.Identifier].ForeColor = Helper.IntToColor(0xD0DAE2);

            scObj.Styles[Style.Cpp.Number].ForeColor = Helper.IntToColor(0xA6CEA8);
            scObj.Styles[Style.Cpp.String].ForeColor = Helper.IntToColor(0xD69D85);
            scObj.Styles[Style.Cpp.Character].ForeColor = Helper.IntToColor(0xD69D85);
            scObj.Styles[Style.Cpp.Preprocessor].ForeColor = Helper.IntToColor(0x8AAFEE);
            scObj.Styles[Style.Cpp.Operator].ForeColor = Helper.IntToColor(0xE0E0E0);
            scObj.Styles[Style.Cpp.Regex].ForeColor = Helper.IntToColor(0xD69D85);

            scObj.Styles[Style.Cpp.Comment].ForeColor = Helper.IntToColor(0xBD758B);
            scObj.Styles[Style.Cpp.CommentLine].ForeColor = Helper.IntToColor(0x40BF57);
            scObj.Styles[Style.Cpp.CommentLineDoc].ForeColor = Helper.IntToColor(0x77A7DB);
            scObj.Styles[Style.Cpp.CommentDoc].ForeColor = Helper.IntToColor(0x2FAE35);
            scObj.Styles[Style.Cpp.CommentDocKeyword].ForeColor = Helper.IntToColor(0xB3D991);
            scObj.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = Helper.IntToColor(0xFF0000);
            scObj.Styles[Style.Cpp.GlobalClass].ForeColor = Helper.IntToColor(0x48A8EE);

            scObj.Styles[Style.Cpp.Word].ForeColor = Helper.IntToColor(0x559CD6);
            scObj.Styles[Style.Cpp.Word2].ForeColor = Helper.IntToColor(0x55D6C2);

            // --------------------------------------------------------------------------------------------------------------------------------
            // Set keyword lists
            // --------------------------------------------------------------------------------------------------------------------------------

            string strSmallSyntax = "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally with default break continue " +
                                    "delete return each const namespace package include use is as instanceof typeof author copy deprecated eventType example exampleText exception haxe inheritDoc " +
                                    "internal link mtasc mxmlc param private see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public " +
                                    "partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool by byte case " +
                                    "char checked decimal default delegate double descending explicit event extern enum false fixed float foreach from goto group implicit " +
                                    "in int internal into lock long null object operator out override orderby params private protected public readonly ref return struct sbyte sealed " +
                                    "short sizeof stackalloc static string select this true typeof uint ulong unchecked unsafe ushort using virtual volatile void where yield datetime ";

            string strSyntax2 = "Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String " +
                                "SyntaxError Exception TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr DataTable DataRow Void Path File System Windows Forms ScintillaNET ";

            string strSyntax3 = "SqlConnection SqlCommand DataSet DataTable DataRow ListViewItem ListView GridView GridViewRow DataGridViewTextBoxColumn SQLDB";

            scObj.SetKeywords(0, strSmallSyntax);
            scObj.SetKeywords(1, strSyntax2 + strSyntax3);
        }

        // SQL Syntax Coloring
        public static void Set_Coloring_SQL(Scintilla scObj, bool bLineNumbers = true)
        {
            scObj.CharAdded += (SQLAuto_CharAdded);

            // Reset the styles
            scObj.StyleResetDefault();
            scObj.CaretForeColor = Color.White;
            scObj.Styles[Style.Default].Font = "Consolas";
            scObj.Styles[Style.Default].Size = 10;
            scObj.Styles[Style.Default].BackColor = Helper.IntToColor(0x1E1E1E);
            scObj.Styles[Style.Default].ForeColor = Helper.IntToColor(0xF1F2F3);
            scObj.StyleClearAll();

            // Set the SQL Lexer
            scObj.Lexer = Lexer.Sql;

            // Show line numbers
            if (bLineNumbers)
            {
                scObj.Margins[0].Width = 35;
                scObj.Margins[0].Type = MarginType.Number;
                scObj.Margins[0].Sensitive = true;
                scObj.Margins[0].Mask = 0;

                // Set the Styles
                scObj.Styles[Style.LineNumber].ForeColor = Helper.IntToColor(0x2B91AF);
                scObj.Styles[Style.LineNumber].BackColor = Helper.IntToColor(0x1E1E1E);
            }

            scObj.Styles[Style.Sql.Number].ForeColor = Helper.IntToColor(0xB5CEA8);
            scObj.Styles[Style.Sql.String].ForeColor = Helper.IntToColor(0xCB4141);
            scObj.Styles[Style.Sql.Character].ForeColor = Helper.IntToColor(0xCB4141);
            scObj.Styles[Style.Sql.Operator].ForeColor = Helper.IntToColor(0x818181);

            scObj.Styles[Style.Sql.Comment].ForeColor = Helper.IntToColor(0x57A64A);
            scObj.Styles[Style.Sql.CommentDoc].ForeColor = Helper.IntToColor(0x57A64A);
            scObj.Styles[Style.Sql.CommentLine].ForeColor = Helper.IntToColor(0x57A64A);
            scObj.Styles[Style.Sql.CommentLineDoc].ForeColor = Helper.IntToColor(0x57A64A);

            scObj.Styles[Style.Sql.Word].ForeColor = Helper.IntToColor(0x569CD6);
            scObj.Styles[Style.Sql.Word2].ForeColor = Helper.IntToColor(0x569CD6);
            scObj.Styles[Style.Sql.User1].ForeColor = Helper.IntToColor(0x818181);
            scObj.Styles[Style.Sql.User2].ForeColor = Helper.IntToColor(0xB5CEA8);

            // --------------------------------------------------------------------------------------------------------------------------------
            // Set keyword lists
            // --------------------------------------------------------------------------------------------------------------------------------

            // Word = 0
            scObj.SetKeywords(0, @"add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ");
            // Word2 = 1
            scObj.SetKeywords(1, @"ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ");
            
            // User1 = 4
            scObj.SetKeywords(4, @"all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ");
            // User2 = 5
            scObj.SetKeywords(5, @"sys objects sysobjects ");
        }

        // ----------------------------------------------------------------------------------------------------------------------

        private static void TextChanged(object sender, EventArgs e)
        {
            Scintilla scObj = (Scintilla)sender;

            if (scObj.Text.Length < 20)
            {
                scObj.ScrollWidth = 1;
                scObj.ScrollWidthTracking = true;
            }
        }

        private static void TextZoomChanged(object sender, EventArgs e)
        {
            Scintilla scObj = (Scintilla)sender;

            // If the Zoom level is different than the default
            if (scObj.Zoom != 0) scObj.Zoom = 0; // Set the Zoom level to default
        }


        private static void SQLAuto_CharAdded(object sender, CharAddedEventArgs e)
        {
            Scintilla scObj = (Scintilla)sender;

            // Find the word start
            var currentPos = scObj.CurrentPosition;
            var wordStartPos = scObj.WordStartPosition(currentPos, true);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0)
            {
                if (!scObj.AutoCActive)
                    scObj.AutoCShow(lenEntered, "add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go");
            }
        }

    }


}
